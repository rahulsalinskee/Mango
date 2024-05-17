using AutoMapper;
using Mango.Service.Shopping.Cart.API.DataContext;
using Mango.Service.Shopping.Cart.API.DTOs.CommonResponseDto;
using Mango.Service.Shopping.Cart.API.DTOs.Coupon;
using Mango.Service.Shopping.Cart.API.DTOs.ShoppingCart;
using Mango.Service.Shopping.Cart.API.Models;
using Mango.Service.Shopping.Cart.API.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Service.Shopping.Cart.API.Repository.Implementations
{
    public class CartServiceImplementation : ICartService
    {
        #region Private Data Members
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Application Db Context
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Response 
        /// </summary>
        private readonly ResponseDto _responseDto;

        /// <summary>
        /// Product Service
        /// </summary>
        private readonly IProductService _productService;

        /// <summary>
        /// Coupon Service
        /// </summary>
        private readonly ICouponService _couponService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="applicationDbContext"></param>
        /// <param name="responseDto"></param>
        public CartServiceImplementation(IMapper mapper, ApplicationDbContext applicationDbContext, ResponseDto responseDto, IProductService productService, ICouponService couponService)
        {
            this._mapper = mapper;
            this._applicationDbContext = applicationDbContext;
            this._responseDto = responseDto;
            this._productService = productService;
            this._couponService = couponService;
        }
        #endregion

        #region Cart Update Insert Async
        /// <summary>
        /// Update and Insert shopping cart
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto> CartUpdateInsertAsync(ShoppingCartDto shoppingCartDto)
        {
            try
            {
                /* 
                *  AsNoTracking - 
                *  Exception Message: Instance of some entity can not be tracked because another instance of the same entity is already being tracked.
                *  If we try to update an entity which is already being tracked by some other request
                *  Here: We are fetching cart head and cart details from the database.
                *  To fix this exception, we need to use AsNoTracking()
                */
                CartHeader? cartHeaderFromDb = await this._applicationDbContext.TblCartHeader.AsNoTracking().FirstOrDefaultAsync(cart => cart.UserId == shoppingCartDto.CartHeaderDto.UserId);

                /* IF - Cart Header is Null (empty), it means that there is NO entry in the cart for that user. 
                *  THEN - We need to Create a New Cart Header and Cart Details
                *  
                *  ELSE (if cart header is NOT null (empty)) - It means that there is an entry in the cart for that user (User ID).
                *  User has already added some products. It means we need to update the Cart Header.
                *  THEN - We have to check if the cart details has the same product or not.
                */
                if (cartHeaderFromDb == null)
                {
                    /* 1. Convert DTO to Model so that we can create a new Cart Header as 
                    *  creating new record in the database accepts only model (Not DTO)
                    */
                    CartHeader? cartHeaderModel = this._mapper.Map<CartHeader>(source: shoppingCartDto.CartHeaderDto);

                    /* 2. Create Cart Header and Details */
                    await this._applicationDbContext.TblCartHeader.AddAsync(entity: cartHeaderModel);

                    /* 
                    *  3. Save Changes to Database here to retrieve the Cart Header ID and 
                    *  This Cart Header ID needs to be populated in the Cart Details
                    */
                    await this._applicationDbContext.SaveChangesAsync();

                    /* Populating Cart Header ID in the Cart Details */
                    shoppingCartDto.ListOfCartDetailsDto.First().CartHeaderId = cartHeaderModel.CartHeaderId;

                    /* Convert Add and Save Cart Details into database by convert DTO to Model */
                    var message = await ConvertAddUpdateSaveCartDetailsInDatabaseAsync(shoppingCartDto: shoppingCartDto, isForUpdate: false);

                    /* Setting Response Display Message for Adding Cart Details */
                    this._responseDto.DisplayMessage = message;
                }
                else
                {
                    /* 
                    *  1st Condition: Check if the cart details is having the same product entry (Using Product ID) 
                    *  The reason [DOT]First() is being used here is because cart details will only have one entry as the only 
                    *  way to add product to cart is through the Product Details page. Hence, it is not possible to have more than one
                    *  product in the cart at the same time.
                    *  
                    *  2nd Condition: We also need to check and ensure if that product entry in the cart is 
                    *  for the same user we are working with. 
                    *  Because it is possible that different cart Header ID can have the same product ID in their shopping cart.
                    */
                    CartDetails? cartDetailsFromDb = await this._applicationDbContext.TblCartDetails.AsNoTracking().FirstOrDefaultAsync(cartDetails => cartDetails.ProductId == shoppingCartDto.ListOfCartDetailsDto.First().ProductId && cartDetails.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    /* 
                    *  IF - There is no entry of cart details in the cart AND if user is adding a new product in the cart
                    *  THEN - We need to Create a New Cart Details when cart header is already exists
                    *  
                    *  ELSE - It means there is an existing product in the cart - 
                    *  THEN - Update the count of the existing product in the cart details
                    */
                    if (cartDetailsFromDb == null)
                    {
                        /* Need to create Cart Details */
                        /* Populating Cart Header ID in the Cart Details */
                        shoppingCartDto.ListOfCartDetailsDto.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;

                        /* Convert Add and Save Cart Details into database by convert ShoppingCartDto DTO to Model */
                        var message = await ConvertAddUpdateSaveCartDetailsInDatabaseAsync(shoppingCartDto: shoppingCartDto, isForUpdate: false);

                        /* Setting Response Display Message for Adding Cart Details for add and update product in the cart */
                        this._responseDto.DisplayMessage = message;
                    }
                    else
                    {
                        /* Update the existing product count in the cart details */
                        shoppingCartDto.ListOfCartDetailsDto.First().Count += cartDetailsFromDb.Count;

                        /* Assign the shopping cart Header ID */
                        shoppingCartDto.ListOfCartDetailsDto.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;

                        /* Assign the shopping cart Details ID */
                        shoppingCartDto.ListOfCartDetailsDto.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;

                        /* Convert Add and Save Cart Details into database by convert ShoppingCartDto DTO to Model */
                        var message = await ConvertAddUpdateSaveCartDetailsInDatabaseAsync(shoppingCartDto: shoppingCartDto, isForUpdate: true);

                        /* Setting Response Display Message for Update Cart Details */
                        this._responseDto.DisplayMessage = message;
                    }
                }
                /* Setting Response */
                this._responseDto.Result = shoppingCartDto;
                this._responseDto.IsSuccess = true;
            }
            catch (Exception exception)
            {
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = exception.Message;
            }

            /* Return Response */
            return this._responseDto;
        }
        #endregion

        #region Apply Coupon Code to the shopping cart Async
        /// <summary>
        /// Apply Coupon Code to the shopping cart
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> ApplyCouponAsync(ShoppingCartDto shoppingCartDto)
        {
            var result = await ApplyRemoveCouponCodeAsync(shoppingCartDto: shoppingCartDto, isCouponApplied: true);

            if (!result.Item1)
            {
                this._responseDto.IsSuccess = false;
            }
            else
            {
                /* Setting Response */
                this._responseDto.IsSuccess = true;
                this._responseDto.Result = true;
            }
            this._responseDto.DisplayMessage = result.Item2;

            /* Return Response */
            return this._responseDto;
        }
        #endregion

        #region Remove Coupon Code from the shopping cart Async
        /// <summary>
        /// Remove Coupon Code from the shopping cart
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> RemoveCouponAsync(ShoppingCartDto shoppingCartDto)
        {
            /* Apply Remove Coupon Code */
            var result = await ApplyRemoveCouponCodeAsync(shoppingCartDto: shoppingCartDto, isCouponRemoved: true);

            if (!result.Item1)
            {
                this._responseDto.IsSuccess = false;
            }
            else
            {
                /* Setting Response */
                this._responseDto.IsSuccess = true;
                this._responseDto.Result = true;
            }
            this._responseDto.DisplayMessage = result.Item2;

            /* Return Response */
            return this._responseDto;
        }
        #endregion

        #region Delete Cart Async
        /// <summary>
        /// Delete shopping cart
        /// </summary>
        /// <param name="shoppingCartDetailId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> DeleteCartAsync(int shoppingCartDetailId)
        {
            try
            {
                /* Initialize message to set display message in the Response */
                string? message = string.Empty;

                /* 
                *  Check if the cart details is having the same cart details ID as the one we want to delete.                 
                */
                CartDetails? cartDetailsFromDb = await this._applicationDbContext.TblCartDetails.AsNoTracking().FirstOrDefaultAsync(cartDetails => cartDetails.CartDetailsId == shoppingCartDetailId);

                /* 
                * To check the total count of the cart items because we are deleting the cart items from the cart details if that exiting item is the only item in the cart 
                * for that user (UerID) then we can remove the cart header as well
                */
                int totalCartItems = await this._applicationDbContext.TblCartDetails.Where(cartDetail => cartDetail.CartHeaderId == cartDetailsFromDb.CartHeaderId).CountAsync();

                /* Delete cart details item (some item from cart - Means,  cart is still not empty) */
                this._applicationDbContext.Remove(entity: cartDetailsFromDb);

                /* Set the message when the user is removing the cart details item (some item from cart - Means,  cart is still not empty) */
                message = "Product removed from the cart";

                /* 
                * IF - Total cart item in the cart is 1 (only one item is present in the cart)
                * THEN - We need to remove the cart header that also means user is removing the last product in the cart.
                */
                if (totalCartItems is 1)
                {
                    /* Get The  cart header to remove from the database */
                    CartHeader? cartHeaderToRemoveFromDb = await this._applicationDbContext.TblCartHeader.FirstOrDefaultAsync(cartHeader => cartHeader.CartHeaderId == cartDetailsFromDb.CartHeaderId);

                    /* Remove the cart header from the database */
                    this._applicationDbContext.TblCartHeader.Remove(entity: cartHeaderToRemoveFromDb);

                    /* Set the message when the user is removing the last product in the cart */
                    message = "There is no product in the cart";
                }

                /* Save the changes in the database */
                await this._applicationDbContext.SaveChangesAsync();

                /* Setting Response */
                this._responseDto.Result = true;
                this._responseDto.IsSuccess = true;

                /* Setting Response Display Message for Adding Cart Details */
                this._responseDto.DisplayMessage = message;
            }
            catch (Exception exception)
            {
                this._responseDto.Result = false;
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = exception.Message;
            }
            return this._responseDto;
        }
        #endregion

        #region Get Cart By User  Id Async
        /// <summary>
        /// Get shopping cart information based on User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> GetCartByUserIdAsync(string userId)
        {
            try
            {
                /* Fetch Shopping Cart DTO */
                ShoppingCartDto? shoppingCartDto = new()
                {
                    /* Convert Shopping Cart Model to Shopping Cart DTO */
                    CartHeaderDto = this._mapper.Map<ShoppingCartHeaderDto>(source: await this._applicationDbContext.TblCartHeader.FirstAsync(cartHeader => cartHeader.UserId == userId))
                };

                /* 
                *  Fetch List of product items from Cart Details as source and convert List of Cart Details Model to List of Cart Details DTO
                */
                shoppingCartDto.ListOfCartDetailsDto = this._mapper.Map<IEnumerable<ShoppingCartDetailsDto>>(source: await this._applicationDbContext.TblCartDetails.Where(cartDetails => cartDetails.CartHeaderId == shoppingCartDto.CartHeaderDto.CartHeaderId).ToListAsync());

                /* Load All Products */
                var products = await this._productService.GetAllProductsAsync();

                /* 
                *  To Get the total price of the cart, each item needs to be multiplied by their unit price with the count of that item
                */
                foreach (var cartItem in shoppingCartDto.ListOfCartDetailsDto)
                {
                    /* Getting Product */
                    cartItem.Product = products.FirstOrDefault(product => product.ProductId == cartItem.ProductId)!;

                    /* Total Price is equal to (Count of product * unit Price of the product) */
                    shoppingCartDto.CartHeaderDto.CartTotal += (cartItem.Count * cartItem.Product.Price);
                }

                /* 
                *  Apply discount coupon if any discount code is valid and exists 
                *  IF - Discount code is NOT Null
                */
                if (!string.IsNullOrEmpty(shoppingCartDto.CartHeaderDto.CouponCode))
                {
                    /* Get Coupon from Coupon Service using coupon code */
                    CouponDto? couponDto = await this._couponService.GetCouponByCodeAsync(couponCode: shoppingCartDto.CartHeaderDto.CouponCode);

                    /*
                    *  IF - Coupon code is not null and total cart amount is greater than coupon minimum amount
                    *  Discount Coupon can only be applied if total cart amount is greater than coupon minimum amount
                    */
                    if (couponDto is not null && shoppingCartDto.CartHeaderDto.CartTotal > couponDto.MinimumAmount)
                    {
                        /* Apply (Reduce) discount amount from total cart amount */
                        shoppingCartDto.CartHeaderDto.CartTotal -= couponDto.DiscountAmount;

                        /* Set coupon message */
                        shoppingCartDto.CartHeaderDto.Discount = couponDto.DiscountAmount;
                    }
                }

                /* Setting Response */
                this._responseDto.Result = shoppingCartDto;
                this._responseDto.IsSuccess = true;
            }
            catch (Exception exception)
            {
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = exception.Message;
            }
            return this._responseDto;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Convert ShoppingCartDto to CartDetails model and Add, Update and Save Cart Details into database
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <param name="isForUpdate"></param>
        /// <returns name="message">message</returns>
        private async Task<string?> ConvertAddUpdateSaveCartDetailsInDatabaseAsync(ShoppingCartDto shoppingCartDto, bool isForUpdate = false)
        {
            string? operationMessage;
            try
            {
                /* 
                *  Convert Cart Details DTO to Cart Details Model so that we can create a new Cart Details as 
                *  creating new record in the database accepts only model (Not DTO) 
                */
                CartDetails? cartDetails = this._mapper.Map<CartDetails>(source: shoppingCartDto.ListOfCartDetailsDto.First());

                /*
                *  Check if it is an Update or Create operation
                */
                if (isForUpdate)
                {
                    /* Update the existing product count in the cart details */
                    this._applicationDbContext.Update(entity: cartDetails);

                    /* Setting Response Display Message for Update Cart Details */
                    operationMessage = "Cart Details Updated Successfully!";
                }
                else
                {
                    /* Create and Save Cart Details into database by convert DTO to Model */
                    await this._applicationDbContext.AddAsync(entity: cartDetails);

                    /* Setting Response Display Message for Adding Cart Details */
                    operationMessage = "Cart Details Added Successfully!";
                }

                /* Save Changes to Database */
                await this._applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                operationMessage = exception.Message;
            }

            /* Return Operation Message */
            return operationMessage;
        }

        /// <summary>
        /// Apply Or Remove Coupon Code to the shopping cart
        /// </summary>
        /// <param name="shoppingCartDto"></param>
        /// <param name="isCouponApplied"></param>
        /// <param name="isCouponRemoved"></param>
        /// <returns></returns>
        private async Task<(bool, string?)> ApplyRemoveCouponCodeAsync(ShoppingCartDto shoppingCartDto, bool isCouponApplied = false, bool isCouponRemoved = false)
        {
            /* Initialize to display message */
            string message = string.Empty;

            /* Initialize to send a status flag */
            bool isSuccess = false;

            try
            {
                /* Get the cart header from the database based on User ID */
                CartHeader? cartHeaderFromDataBase = await this._applicationDbContext.TblCartHeader.FirstAsync(cartHeader => cartHeader.UserId == shoppingCartDto.CartHeaderDto.UserId);

                /*
                *  IF - Coupon code is NOT applied to the cart AND coupon code is NOT removed from the cart
                *  THEN - This means, this method is not called from a valid way. 
                *  This method needs to be called from either for Coupon Code Applied or Coupon Code Removed
                *  In that case, execution will be stopped in this conditional block
                */
                if (!isCouponApplied && !isCouponRemoved)
                {
                    /* 
                    *  Setting message as Invalid Operation! Because this method needs to be called only when coupon code is applied or removed
                    *  If this condition is executed this means, this method is being called in by an invalid way
                    */
                    message = "Invalid Operation!";
                    isSuccess = false;
                    throw new Exception(message);
                }

                /*
                *  IF - Coupon code is applied to the cart
                *  This means, previous coupon code was not applied (left blank by default) and now it is applied
                */
                if (isCouponApplied)
                {
                    /* 
                    *  Setting Coupon Code coming from shopping cart to the cart header in the database 
                    *  This means, previous coupon code was not applied (left blank by default) and now it is applied
                    */
                    cartHeaderFromDataBase.CouponCode = shoppingCartDto.CartHeaderDto.CouponCode;

                    /* Setting Display Message for Coupon Code Applied successfully */
                    message = "Coupon code applied successfully to the cart!";
                }

                /*
                *  IF - Coupon code is removed from the cart
                *  This means, previous coupon code was applied and now it is removed
                */
                if (isCouponRemoved)
                {
                    /* 
                    *  Setting empty Coupon Code coming from shopping cart to the cart header in the database 
                    *  This means, previous coupon code was applied and now it is removed
                    */
                    cartHeaderFromDataBase.CouponCode = string.Empty;

                    /* Setting Display Message for Coupon Code Removed successfully */
                    message = "Coupon code removed successfully from the cart!";
                }

                /* Update the cart header in the database */
                this._applicationDbContext.TblCartHeader.Update(cartHeaderFromDataBase);

                /* Save the changes in the database */
                await this._applicationDbContext.SaveChangesAsync();

                /* Setting status flag to true when either of the execution is successfully completed */
                isSuccess = true;
            }
            catch (Exception exception)
            {
                /* Setting status flag to false when either of the execution fails */
                isSuccess = false;

                /* Setting Display Message accordingly */
                message = exception.Message;
            }

            /* Returning true when either of the execution is successful and return the Message accordingly */
            return (isSuccess, message);
        }
        #endregion
    }
}

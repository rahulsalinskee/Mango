using AutoMapper;
using Mango.Services.Coupon.ApplicationDataContext.ApplicationDataContext;
using Mango.Services.Coupon.BusinessLogics.Repository.Services;
using Mango.Services.Coupon.Model.DTOs.CommonResponseDtos;
using Mango.Services.Coupon.Model.DTOs.CouponDtos;
using Mango.Services.Coupon.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.Coupon.BusinessLogics.Repository.Implementations
{
    public class CouponRepository : ICouponRepositoryService
    {
        #region Private Members
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is used for dependency injection.
        /// All the parameters need to be registered in DI container (AddSingleton/AddTransient/AddScoped) inside Program.cs
        /// </summary>
        /// <param name="applicationDbContext"></param>
        /// <param name="responseDto"></param>
        public CouponRepository(ApplicationDbContext applicationDbContext, ResponseDto responseDto, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            /* As we are passing ResponseDto in constructor as parameter, we need to register in DI using AddSingleton method in Program.cs. */
            this._responseDto = responseDto;
            /* As we are passing IMapper in constructor as parameter, we need to register in DI using AddSingleton method in Program.cs. */
            this._mapper = mapper;
        }
        #endregion

        #region Get All Coupons Async
        /// <summary>
        /// This method is used to get all the coupons
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            var coupons = await this._applicationDbContext.TblCoupons.AsNoTracking().ToListAsync();
            if (coupons.Count >= 1)
            {
                this._responseDto.Result = this._mapper.Map<IEnumerable<CouponDto>>(source: coupons);
                this._responseDto.IsActive = true;
                //this._responseDto.ExpiryDate = DateTime.Now.AddDays(4);
                this._responseDto.DisplayMessage = $"{coupons.Count} Coupons Fetched Successfully!";
                this._responseDto.IsSuccess = true;
            }
            else
            {
                this._responseDto.DisplayMessage = "Coupons Not Found!";
                this._responseDto.IsSuccess = false;
                this._responseDto.Result = null;
            }
            return this._responseDto;
        }
        #endregion

        #region Get Coupon By Id Async
        /// <summary>
        /// This method is used to get coupon by coupon id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> GetCouponByIdAsync(int couponId)
        {
            try
            {
                var couponModel = await this._applicationDbContext.TblCoupons.FirstOrDefaultAsync(coupon => coupon.CouponId == couponId);

                if (couponModel is not null)
                {
                    /* This line of code is used to convert CouponModel to CouponDto using Map method */
                    this._responseDto.Result = this._mapper.Map<CouponDto>(source: couponModel);
                    this._responseDto.DisplayMessage = "Coupon Fetched Successfully!";
                    this._responseDto.IsSuccess = true;
                }
                else
                {
                    this._responseDto.DisplayMessage = "Coupon Not Found!";
                    this._responseDto.IsSuccess = false;
                }
            }
            catch (Exception exception)
            {
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = exception.Message;
            }
            return this._responseDto;
        }
        #endregion

        #region Get Coupon By Coupon Code
        /// <summary>
        /// This method is used to get coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto> GetCouponByCodeAsync(string couponCode)
        {
            try
            {
                var coupon = await this._applicationDbContext.TblCoupons.FirstOrDefaultAsync(coupon => coupon.CouponCode.ToLower() == couponCode.ToLower());

                if (coupon is not null)
                {
                    /* This line of code is used to convert CouponModel to CouponDto using Map method */
                    this._responseDto.Result = this._mapper.Map<CouponDto>(source: coupon);
                    this._responseDto.DisplayMessage = "Coupon Fetched Successfully!";
                    this._responseDto.IsSuccess = true;
                }
                else
                {
                    this._responseDto.DisplayMessage = "Coupon Not Found!";
                    this._responseDto.IsSuccess = false;
                }
            }
            catch (Exception exception)
            {
                this._responseDto.DisplayMessage = exception.Message;
                this._responseDto.IsSuccess = false;
            }
            return this._responseDto;
        }
        #endregion

        #region Create A New Coupon
        /// <summary>
        /// Creates a new coupon
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
        {
            try
            {
                /* This line of code is used to convert CouponDto to CouponModel using Map method */
                CouponModel couponModel = this._mapper.Map<CouponModel>(source: couponDto);

                if (couponModel is not null)
                {
                    var coupon = await this._applicationDbContext.TblCoupons.AddAsync(couponModel);
                    await this._applicationDbContext.SaveChangesAsync();

                    /* This line of code is used to convert CouponModel to CouponDto using Map method */
                    var couponDtoResult = this._mapper.Map<CouponDto>(source: couponModel);
                    this._responseDto.Result = couponDtoResult;
                    this._responseDto.IsSuccess = true;
                    this._responseDto.DisplayMessage = "Coupon Created Successfully!";
                }
                else
                {
                    this._responseDto.IsSuccess = false;
                    this._responseDto.DisplayMessage = "Coupon Not Created!";
                }
            }
            catch (Exception exception)
            {
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = exception.Message;
            }
            return this._responseDto;
        }
        #endregion

        #region Update Coupon By ID Async
        /// <summary>
        /// This method is used to update coupon
        /// </summary>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> UpdateCouponByIdAsync(int couponId, CouponDto couponDto)
        {
            try
            {
                /* This line of code is used to fetch coupon by coupon id */
                var existingFetchedCouponById = await this._applicationDbContext.TblCoupons.FirstOrDefaultAsync(coupon => coupon.CouponId == couponId);

                if (existingFetchedCouponById is null)
                {
                    this._responseDto.IsSuccess = false;
                    this._responseDto.DisplayMessage = "Coupon Not Updated!";
                    return this._responseDto;
                }

                /* This line of code is used to convert CouponDto to CouponModel using Map method */
                if (existingFetchedCouponById.CouponId == couponId && existingFetchedCouponById is not null)
                {
                    /* These lines of code are used to update coupon */
                    existingFetchedCouponById.CouponCode = couponDto.CouponCode;
                    existingFetchedCouponById.DiscountAmount = couponDto.DiscountAmount;
                    existingFetchedCouponById.CreatedDate = couponDto.CreatedDate;
                    existingFetchedCouponById.ExpiryDate = couponDto.ExpiryDate;
                    existingFetchedCouponById.MinimumAmount = couponDto.MinimumAmount;

                    /* This line of code is used to save the changes in the database */
                    await this._applicationDbContext.SaveChangesAsync();

                    /* This line of code is used to convert CouponModel to CouponDto using Map method */
                    var couponDtoResult = this._mapper.Map<CouponDto>(source: existingFetchedCouponById);

                    /* This line of code is used to assign coupondDtoResult to responseDto */
                    this._responseDto.Result = couponDtoResult;
                    this._responseDto.IsSuccess = true;
                    this._responseDto.DisplayMessage = "Coupon Updated Successfully!";
                }
            }
            catch (Exception exception)
            {
                this._responseDto.IsSuccess = false;
                this._responseDto.DisplayMessage = exception.Message;
            }
            return this._responseDto;
        }
        #endregion

        #region Update Coupon By Code Async
        /// <summary>
        /// This method is used to update coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <param name="couponDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto> UpdateCouponByCodeAsync(string couponCode, CouponDto couponDto)
        {
            try
            {
                var existingCouponFetchedByCouponCode = await this._applicationDbContext.TblCoupons.FirstOrDefaultAsync(coupon => coupon.CouponCode.ToLower() == couponCode.ToLower());

                if (existingCouponFetchedByCouponCode is null)
                {
                    this._responseDto.IsSuccess = false;
                    this._responseDto.DisplayMessage = "Coupon Not Updated!";
                    return this._responseDto;
                }

                else if (existingCouponFetchedByCouponCode.CouponCode == couponCode && existingCouponFetchedByCouponCode is not null)
                {
                    existingCouponFetchedByCouponCode.CouponCode = couponDto.CouponCode;
                    existingCouponFetchedByCouponCode.DiscountAmount = couponDto.DiscountAmount;
                    existingCouponFetchedByCouponCode.CreatedDate = couponDto.CreatedDate;
                    existingCouponFetchedByCouponCode.ExpiryDate = couponDto.ExpiryDate;
                    existingCouponFetchedByCouponCode.MinimumAmount = couponDto.MinimumAmount;

                    await this._applicationDbContext.SaveChangesAsync();

                    var couponDtoResult = this._mapper.Map<CouponDto>(source: existingCouponFetchedByCouponCode);
                    this._responseDto.Result = couponDtoResult;
                    this._responseDto.DisplayMessage = "Coupon Updated Successfully!";
                    this._responseDto.IsSuccess = true;
                }
            }
            catch (Exception exception)
            {
                this._responseDto.DisplayMessage = exception.Message;
                this._responseDto.IsSuccess = false;
            }
            return this._responseDto;
        }
        #endregion

        #region Delete Coupon By ID
        /// <summary>
        /// This method is used to delete coupon by coupon id
        /// </summary>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public async Task<ResponseDto> DeleteCouponByIdAsync(int couponId)
        {
            try
            {
                var existingCouponFetchedById = await this._applicationDbContext.TblCoupons.FirstOrDefaultAsync(coupon => coupon.CouponId == couponId);

                if (existingCouponFetchedById is null)
                {
                    this._responseDto.IsSuccess = false;
                    this._responseDto.DisplayMessage = "Coupon Not Deleted!";
                }

                if (existingCouponFetchedById is not null)
                {
                    this._applicationDbContext.TblCoupons.Remove(existingCouponFetchedById);
                    await this._applicationDbContext.SaveChangesAsync();
                    var couponDtoResult = this._mapper.Map<CouponDto>(source: existingCouponFetchedById);
                    this._responseDto.Result = couponDtoResult;
                    this._responseDto.IsSuccess = true;
                    this._responseDto.DisplayMessage = "Coupon Deleted Successfully!";
                }
            }
            catch (Exception)
            {
                this._responseDto.DisplayMessage = "Coupon Not Deleted!";
                this._responseDto.IsSuccess = false;
            }
            return this._responseDto;
        }
        #endregion

        #region Delete Coupon By Code
        /// <summary>
        /// This method is used to delete coupon by coupon code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public async Task<ResponseDto> DeleteCouponByCodeAsync(string couponCode)
        {
            try
            {
                var existingCouponFetchedByCode = await this._applicationDbContext.TblCoupons.FirstOrDefaultAsync(coupon => coupon.CouponCode.ToLower() == couponCode.ToLower());

                if (existingCouponFetchedByCode is null)
                {
                    this._responseDto.IsSuccess = false;
                    this._responseDto.DisplayMessage = "Coupon Not Deleted!";
                    return this._responseDto;
                }

                if (existingCouponFetchedByCode is not null)
                {
                    this._applicationDbContext.TblCoupons.Remove(existingCouponFetchedByCode);
                    await this._applicationDbContext.SaveChangesAsync();
                    var couponDtoResult = this._mapper.Map<CouponDto>(source: existingCouponFetchedByCode);
                    this._responseDto.Result = couponDtoResult;
                    this._responseDto.IsSuccess = true;
                }
            }
            catch (Exception exception)
            {
                this._responseDto.DisplayMessage = exception.Message;
                this._responseDto.IsSuccess = false;
            }
            return _responseDto;
        } 
        #endregion
    }
}

namespace Mango.Service.Shopping.Cart.API.DTOs.ShoppingCart
{
    public class ShoppingCartHeaderDto
    {
        /// <summary>
        /// Primary Key for the Cart Header table
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// User Id
        /// For one user, there will be only one record in the Cart Header ID
        /// This way we can know it is unique
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Coupon Code
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// Discount amount
        /// This will not be stored in the database
        /// We only need them for display purposes
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// Cart Total Amount
        /// This will not be stored in the database
        /// We only need them for display purposes
        /// </summary>
        public double CartTotal { get; set; }
    }
}

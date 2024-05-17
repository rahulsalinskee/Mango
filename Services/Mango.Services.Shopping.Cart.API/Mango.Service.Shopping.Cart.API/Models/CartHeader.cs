using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Service.Shopping.Cart.API.Models
{
    public class CartHeader
    {
        /// <summary>
        /// Primary Key for the Cart Header table
        /// </summary>
        [Key]
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
        [NotMapped]
        public double Discount { get; set; }

        /// <summary>
        /// Cart Total Amount
        /// This will not be stored in the database
        /// We only need them for display purposes
        /// </summary>
        [NotMapped]
        public double CartTotal { get; set; }
    }
}

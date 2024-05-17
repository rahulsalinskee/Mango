using Mango.Service.Shopping.Cart.API.DTOs.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Service.Shopping.Cart.API.Models
{
    public class CartDetails
    {
        /// <summary>
        /// Cart Details ID
        /// It is Primary Key
        /// </summary>
        [Key]
        public int CartDetailsId { get; set; }

        /// <summary>
        /// Cart Header ID
        /// It is Foreign Key for Cart Header ID
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Cart Header
        /// Navigation Property for Cart Header
        /// </summary>
        [ForeignKey(nameof(CartHeaderId))]
        public CartHeader? CartHeader { get; set; }

        /// <summary>
        /// Product ID
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product DTO is to show the details of the product
        /// </summary>
        [NotMapped]
        public ProductDto Product { get; set; }

        /// <summary>
        /// To show the count of products
        /// </summary>
        public int Count { get; set; }
    }
}

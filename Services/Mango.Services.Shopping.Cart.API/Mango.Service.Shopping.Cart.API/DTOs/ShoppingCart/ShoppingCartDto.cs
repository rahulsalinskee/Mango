namespace Mango.Service.Shopping.Cart.API.DTOs.ShoppingCart
{
    /// <summary>
    /// Shopping cart dto contains shopping cart header and shopping cart details
    /// It is to retrieve shopping cart for a user, it can have one cart header and many cart details
    /// </summary>
    public class ShoppingCartDto
    {
        /// <summary>
        /// Shopping cart header
        /// </summary>
        public ShoppingCartHeaderDto CartHeaderDto { get; set; }

        /// <summary>
        /// Shopping cart details
        /// </summary>
        public IEnumerable<ShoppingCartDetailsDto>? ListOfCartDetailsDto { get; set; }
    }
}

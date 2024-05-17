namespace Mango.Service.Shopping.Cart.API.DTOs.Product
{
    /// <summary>
    /// When we work with web API, We do not expose the root model
    /// We only expose the DTO of the model
    /// </summary>
    public class ProductDto
    {
        // <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the image url.
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}

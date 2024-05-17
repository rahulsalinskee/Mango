namespace Mango.Services.ProductAPIs.DTOs.ProductDtos
{
    public class ProductDto
    {
        /// <summary>
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

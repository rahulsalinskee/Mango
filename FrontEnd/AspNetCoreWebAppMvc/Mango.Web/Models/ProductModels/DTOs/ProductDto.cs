using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models.ProductModels.DTOs
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

        /// <summary>
        /// Count of Product in cart
        /// </summary>
        [Range(1, 100, ErrorMessage = "Count should be between 1 and 100")]
        public int Count { get; set; } = 1;
    }
}

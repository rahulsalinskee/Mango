using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPIs.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        [Range(1, 1000)]
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

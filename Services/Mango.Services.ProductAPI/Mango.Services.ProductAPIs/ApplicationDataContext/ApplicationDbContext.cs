using Mango.Services.ProductAPIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mango.Services.ProductAPIs.ApplicationDataContext
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var relationalDatabaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (relationalDatabaseCreator is not null)
                {
                    if (!relationalDatabaseCreator.Exists())
                    {
                        relationalDatabaseCreator.Create();
                    }
                    if (!relationalDatabaseCreator.HasTables())
                    {
                        relationalDatabaseCreator.Create();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error Message: {exception.Message}");
            }
        }

        /// <summary>
        /// Add DbSet
        /// </summary>
        public DbSet<ProductModel> TblProducts { get; set; }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* It will add these products in TblProducts of database */
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel()
            {
                ProductId = 1,
                Name = "Samosa",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Appetizer"
            });

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel()
            {
                ProductId = 2,
                Name = "Paneer Tikka",
                Price = 13.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/602x402",
                CategoryName = "Appetizer"
            });

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel()
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/601x401",
                CategoryName = "Dessert"
            });

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel()
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/600x400",
                CategoryName = "Entree"
            });
        }
    }
}

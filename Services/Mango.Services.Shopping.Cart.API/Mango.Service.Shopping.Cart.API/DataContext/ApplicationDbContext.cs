using Mango.Service.Shopping.Cart.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mango.Service.Shopping.Cart.API.DataContext
{
    /// <summary>
    /// Application Db Context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(options: dbContextOptions)
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
        /// Add DbSet for Cart Header
        /// </summary>
        public DbSet<CartHeader> TblCartHeader { get; set; }

        /// <summary>
        /// Add DbSet with CartDetails
        /// </summary>
        public DbSet<CartDetails> TblCartDetails { get; set; }
    }
}

using Mango.Services.Coupon.AuthAPI.Models.IdentityUserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace Mango.Services.Coupon.AuthAPI.IdentityDataContexts
{
    public class IdentityDataContext : IdentityDbContext<ApplicationIdentityUserModel>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> identityDbContextOption) : base(options: identityDbContextOption)
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

        public DbSet<ApplicationIdentityUserModel> TblApplicationIdentityUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

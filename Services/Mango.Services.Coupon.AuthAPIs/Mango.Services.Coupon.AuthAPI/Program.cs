using Mango.Services.Coupon.AuthAPI.IdentityDataContexts;
using Mango.Services.Coupon.AuthAPI.Models.IdentityUserModel;
using Mango.Services.Coupon.AuthAPI.Models.JwtModel;
using Mango.Services.Coupon.AuthAPI.Repository.Implementations;
using Mango.Services.Coupon.AuthAPI.Repository.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/* Registering Data Context */
builder.Services.AddDbContext<IdentityDataContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString(name: "IdentityConnectionString"));
});

/* Configuring JWT */
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(key: "ApiSettings:JWTOptions"));

/* Registering Identity Services */
builder.Services.AddIdentity<ApplicationIdentityUserModel, IdentityRole>(options =>
{
    /* Password settings rules for user registration */
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<IdentityDataContext>().AddDefaultTokenProviders();

/* Registering Services */
builder.Services.AddScoped<IAuthenticationService, AuthenticationServiceImplementation>();
builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorServiceImplementation>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();


#region This method will run update database migration command when any migration is pending
/// <summary>
/// This method will run update database migration command when any migration is pending
/// </summary>
void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var identityDataContext = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();
    /* To check if any migration(s) are pending */
    if (identityDataContext.Database.GetPendingMigrations().Count() > 0)
    {
        /* Apply Migrations */
        identityDataContext.Database.Migrate();
    }
}
#endregion

using AutoMapper;
using Mango.Service.Shopping.Cart.API.DataContext;
using Mango.Service.Shopping.Cart.API.DTOs.CommonResponseDto;
using Mango.Service.Shopping.Cart.API.Extensions;
using Mango.Service.Shopping.Cart.API.Mapper;
using Mango.Service.Shopping.Cart.API.Repository.Implementations;
using Mango.Service.Shopping.Cart.API.Repository.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/* Registering Data Context */
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnectionString"));
});

/* Registering AutoMapper */
IMapper mapper = MapperConfigure.RegisterMappers().CreateMapper();

builder.Services.AddScoped<IProductService, ProductServiceImplementation>();
builder.Services.AddScoped<ICartService, CartServiceImplementation>();
builder.Services.AddScoped<ICouponService, CouponServiceImplementation>();
builder.Services.AddScoped<ResponseDto>();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/* Need to HTTP client for this particular URL - "Product": "https://localhost:7003" */
builder.Services.AddHttpClient(name: "Product", configureClient: option =>
{
    option.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductApi"]);
});

/* Need to HTTP client for this particular URL - "Coupon": "https://localhost:7001" */
builder.Services.AddHttpClient(name: "Coupon", configureClient: option =>
{
    option.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponApi"]);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/* Configuring Swagger Gen and its options - These are the basic things & steps which are needed to enable Authentication & Authorization in swagger gen */
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme()
    {
        /* This is for Authorization - These are the default values */
        Name = "Authorization",

        /* Any meaningful description message */
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",

        /* This is for location of token */
        In = ParameterLocation.Header,

        /* This is for type of token */
        Type = SecuritySchemeType.ApiKey,

        /* This is for Bearer token */
        Scheme = "Bearer"
    });

    /* We also need to add security requirement in the options for swagger */
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            }, Array.Empty<string>()
        }
    });
});

/* Add WebApplication Authentication */
builder.AddWebApplicationAuthenticationExtensions();

/* Add Authorization */
builder.Services.AddAuthentication();


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

/* This method will run update database migration command when any migration is pending while launching the application */
ApplyMigration();

app.Run();

#region This method will run update database migration command when any migration is pending
/// <summary>
/// This method will run update database migration command when any migration is pending while launching the application
/// </summary>
void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    /* To check if any migration(s) are pending */
    if (applicationDbContext.Database.GetPendingMigrations().Count() > 0)
    {
        /* Apply Migrations */
        applicationDbContext.Database.Migrate();
    }
}
#endregion
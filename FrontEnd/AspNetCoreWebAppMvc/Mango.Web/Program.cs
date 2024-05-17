using Mango.Web.Services.Implementations;
using Mango.Web.Services.IService;
using Mango.Web.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/* Configure HttpContextAccessor For Storing Generated Token In Cookie */
builder.Services.AddHttpContextAccessor();

/* Configure Add HTTP Client to create HttpClient using IHttpClientFactory */
builder.Services.AddHttpClient();

/* Configure HttpClient for Services (Coupon API, Authentication API) */
#region Configure HTTP Client for Services (Coupon API, Authentication API)
builder.Services.AddHttpClient<ICouponService, CouponServiceImplementation>();
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationServiceImplementation>();
builder.Services.AddHttpClient<IProductService, ProductServiceImplementation>(); 
#endregion

/* Configure Services To Consume APIs (Coupon API, Authentication API, Product API) */
#region Configure Services To Consume APIs (Coupon API, Authentication API, Product API)
StaticDetails.CouponAPIBaseURL = builder.Configuration[key: "ServiceUrls:CouponApi"];
StaticDetails.AuthAPIBaseURL = builder.Configuration[key: "ServiceUrls:AuthApi"];
StaticDetails.ProductAPIBaseURL = builder.Configuration[key: "ServiceUrls:ProductApi"];
#endregion

/* Register Services */
#region Registering Services
builder.Services.AddScoped<ICouponService, CouponServiceImplementation>();
builder.Services.AddScoped<IBaseService, BaseServiceImplementation>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationServiceImplementation>();
builder.Services.AddScoped<ITokenProviderService, TokenProviderServiceImplementation>();
builder.Services.AddScoped<IProductService, ProductServiceImplementation>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOption =>
{
    /* Configure Cookie Options */
    cookieOption.Cookie.Name = "Mango.Cookie";
    cookieOption.LoginPath = "/Authentication/Login";
    cookieOption.LogoutPath = "/Authentication/Logout";
    cookieOption.ExpireTimeSpan = TimeSpan.FromHours(8);
    cookieOption.AccessDeniedPath = "/Authentication/AccessDenied";
}); 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

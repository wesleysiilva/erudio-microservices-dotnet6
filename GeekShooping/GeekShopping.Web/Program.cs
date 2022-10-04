using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
#region Add services to the container.
builder.Services.AddHttpClient<IProductService, ProductService>(c =>
    c.BaseAddress = new Uri(
        configuration["ServiceUrls:ProductAPI"]
    )
);
builder.Services.AddHttpClient<ICartService, CartService>(c =>
    c.BaseAddress = new Uri(
        configuration["ServiceUrls:CartAPI"]
    )
);
builder.Services.AddHttpClient<ICouponService, CouponService>(c =>
    c.BaseAddress = new Uri(
        configuration["ServiceUrls:CouponAPI"]
    )
);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
  .AddOpenIdConnect("oidc", options =>
  {
      options.Authority = configuration["ServiceUrls:IdentityServer"];
      options.GetClaimsFromUserInfoEndpoint = true;
      options.ClientId = "geek_shopping";
      options.ClientSecret = "SECRETCOMPLEXA";
      options.ResponseType = "code";
      options.ClaimActions.MapJsonKey("role", "role", "role");
      options.ClaimActions.MapJsonKey("sub", "sub", "sub");
      options.TokenValidationParameters.NameClaimType = "name";
      options.TokenValidationParameters.RoleClaimType = "role";
      options.Scope.Add("geek_shopping");
      options.SaveTokens = true;
  } 
);

#region DI


#endregion

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
#endregion

app.Run();
using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
#region Add services to the container.
builder.Services.AddHttpClient<IProductService, ProductService>(c => 
    c.BaseAddress = new Uri(
        configuration["ServiceUrls:ProductAPI"]
    )
);
builder.Services.AddControllersWithViews();

#region DI


#endregion

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
#endregion

app.Run();
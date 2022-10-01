using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController
        (
            ILogger<HomeController> logger,
            IProductService productService,
            ICartService cartService
        )
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index() => View(await _productService.FindAllProducts(""));
        [Authorize]
        public async Task<IActionResult> Details(int id) => View(await _productService.FindProductById(id, await HttpContext.GetTokenAsync("access_token")));
        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            CartViewModel cart = new ()
            {
                CartHeader = new()
                {
                    UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value
                },
                CartDetails = new List<CartDetailViewModel>()
                {
                    new CartDetailViewModel()
                    {
                        Count = model.Count,
                        ProductId = model.Id,
                        Product = await _productService.FindProductById(model.Id, token)
                    }
                }
            };

            var response = await _cartService.AddItemToCart(cart, token);
            if (response != null)
                return RedirectToAction(nameof(Index));
            return View(model);
        }


        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        [Authorize]
        public IActionResult Login() => RedirectToAction(nameof(Index));

        public IActionResult Logout() => SignOut("Cookies", "oidc");
    }
}
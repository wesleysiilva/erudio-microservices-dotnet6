using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex() => View(await _productService.FindAllProducts(""));        

        public IActionResult ProductCreate() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var response = await _productService.CreateProduct(model, await HttpContext.GetTokenAsync("access_token"));
            if (response != null) return RedirectToAction(nameof(ProductIndex));

            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _productService.FindProductById(id, await HttpContext.GetTokenAsync("access_token"));
            if (model != null) return View(model);
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var response = await _productService.UpdateProduct(model, await HttpContext.GetTokenAsync("access_token"));
            if (response != null) return RedirectToAction(nameof(ProductIndex));
            
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _productService.FindProductById(id, await HttpContext.GetTokenAsync("access_token"));
            if (model != null) return View(model);
            return NotFound();
        }
        
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            var response = await _productService.DeleteProductById(model.Id, await HttpContext.GetTokenAsync("access_token"));
            if (response) return RedirectToAction(nameof(ProductIndex));
            return View(model);
        }
    }
}

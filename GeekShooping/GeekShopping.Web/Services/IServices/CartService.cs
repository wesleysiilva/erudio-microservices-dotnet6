using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public class CartService : ICartService
    {
        public Task<CartViewModel> AddItemToCart(CartViewModel cart, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApplyCupon(CartViewModel cart, string cuponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCart(long cartId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> UpdateCart(CartViewModel cart, string token)
        {
            throw new NotImplementedException();
        }
    }
}

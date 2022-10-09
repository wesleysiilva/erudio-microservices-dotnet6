using GeekShopping.OrderAPI.Model;

namespace GeekShopping.CartAPI.Repository
{
    public interface IOrderRepository
    {        
        Task<bool> AddOrder(OrderHeader order);
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool status);
    }
}

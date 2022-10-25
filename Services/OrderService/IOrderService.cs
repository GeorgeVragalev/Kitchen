using Kitchen.Models;

namespace Kitchen.Services.OrderService;

public interface IOrderService
{
    public void SendOrder(Order order);
    public Task AddOrderToList(Order order);
    public Task<Order> GetUnservedOrder();
    public Task CleanServedOrders();
}
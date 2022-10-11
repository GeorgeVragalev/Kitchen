using Kitchen.Models;

namespace Kitchen.Services.OrderService;

public interface IOrderService
{
    public void SendOrder(Order order);
    public void AddOrderToList(Order order);
    public Order GetUnservedOrder();
    public Task CleanServedOrders();
}
using Kitchen.Models;

namespace Kitchen.Services.OrderService;

public interface IOrderService
{
    public void SendOrder(Order order);
    public void AddOrderToList(Order order);
    public Order CollectOrder();
    public void IncrementPreparedFoodCounter(int id);
    public Order GetUnservedOrder();
}
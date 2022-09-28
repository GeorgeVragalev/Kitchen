using Kitchen.Models;

namespace Kitchen.Repositories.OrderListRepository;

public interface IOrderListRepository
{
    public void AddOrderToList(Order order);
    public Order CollectOrder();
    public void IncrementPreparedFoodCounter(int id);
    public IList<Order> GetUnservedOrders();
}
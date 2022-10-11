using Kitchen.Models;

namespace Kitchen.Repositories.OrderListRepository;

public interface IOrderListRepository
{
    public void AddOrderToList(Order order);
    public IList<Order> GetUnservedOrders();
    public Task CleanServedOrders();
}
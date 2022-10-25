using Kitchen.Models;

namespace Kitchen.Repositories.OrderListRepository;

public interface IOrderListRepository
{
    public Task AddOrderToList(Order order);
    public IList<Order> GetUnservedOrders();
    public Task CleanServedOrders();
}
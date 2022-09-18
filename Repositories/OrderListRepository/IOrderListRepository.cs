using Kitchen.Models;

namespace Kitchen.Repositories.OrderListRepository;

public interface IOrderListRepository
{
    public void AddOrderToList(Order order);
    public Order? GetOrder();
    void RemoveOrder(Order order);
}
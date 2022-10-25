using System.Collections.Concurrent;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Repositories.OrderListRepository;

public class OrderListRepository : IOrderListRepository
{
    private readonly ConcurrentBag< Order> _orderList = new ConcurrentBag<Order>();

    public Task AddOrderToList(Order order)
    {
         _orderList.Add(order);
        PrintConsole.Write($"Order {order.Id} added to list", ConsoleColor.DarkBlue);
        return Task.CompletedTask;
    }

    public IList<Order> GetUnservedOrders()
    {
        var orders = _orderList.AsQueryable().Where(o => o.OrderStatusEnum == OrderStatusEnum.IsCooking).ToList();
        return orders;
    }

    public Task CleanServedOrders()
    {
        var orders = _orderList.Where(o => o.OrderStatusEnum == OrderStatusEnum.Served).ToList();
        if (orders.Count != 0)
        {
            orders.Clear();
        }

        return Task.CompletedTask;
    }
}
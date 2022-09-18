using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml.Linq;
using Kitchen.Helpers;
using Kitchen.Models;

namespace Kitchen.Repositories.OrderListRepository;

public class OrderListRepository : IOrderListRepository
{
    private readonly ConcurrentDictionary<int, Order> _orderList = new ConcurrentDictionary<int, Order>();

    public void AddOrderToList(Order order)
    {
        _orderList.TryAdd(order.Id, order);
        PrintConsole.Write("Order "+ order.Id+" added to list", ConsoleColor.DarkBlue);
    }

    public Order? GetOrder()
    {
        if (!_orderList.IsEmpty)
        {
            return FindOptimalOrder();
        }

        return null;
    }

    private Order? FindOptimalOrder()
    {
        var order = _orderList.FirstOrDefault().Value;
        return order;
    }

    public void RemoveOrder(Order order)
    {
        var order1 = _orderList.Remove(order.Id, out order);
    }
}
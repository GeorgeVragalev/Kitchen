using System.Collections.Concurrent;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Repositories.OrderListRepository;

public class OrderListRepository : IOrderListRepository
{
    private readonly ConcurrentBag< Order> _orderList = new ConcurrentBag<Order>();
    private static Mutex _mutex = new();

    public void AddOrderToList(Order order)
    {
        _orderList.Add( order);
        PrintConsole.Write("Order "+ order.Id+" added to list", ConsoleColor.DarkBlue);
    }


    private Order? FindOptimalOrder()
    {
        var order = _orderList.AsQueryable().FirstOrDefault(o => o.OrderStatus == OrderStatus.IsCooking);

        if (order != null)
        {
            order.OrderStatus = OrderStatus.IsCooking;
            return order;
        }

        return null;
    }

    public Order CollectOrder()
    {
        var order = _orderList.AsQueryable()
            .Where(o => o.OrderStatus == OrderStatus.IsCooking)
            .FirstOrDefault(o => o.FoodsPreparedCount.Equals(o.Foods.Count));
        
        if (order!=null)
        {
            order.OrderStatus = OrderStatus.Served;
            return order;
        }

        return null;
    }

    public void IncrementPreparedFoodCounter(int id)
    {
        _mutex.WaitOne();
        var order = _orderList.AsQueryable()
            .FirstOrDefault(o => o.Id == id);
        if (order != null) 
            order.FoodsPreparedCount += 1;
        _mutex.ReleaseMutex();
    }

    public IList<Order> GetUnservedOrders()
    {
        var orders = _orderList.AsQueryable().Where(o => o.OrderStatus == OrderStatus.IsCooking).ToList();
        return orders;
    }
}
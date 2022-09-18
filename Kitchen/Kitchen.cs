using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Repositories;
using Kitchen.Repositories.OrderListRepository;
using Kitchen.Services.OrderService;

namespace Kitchen.Kitchen;

public class Kitchen : IKitchen
{
    private readonly IOrderListRepository _orderListRepository;
    private readonly IOrderService _orderService;
    private static Mutex _mutex = new();

    public Kitchen(IOrderListRepository orderListRepository, IOrderService orderService)
    {
        _orderListRepository = orderListRepository;
        _orderService = orderService;
    }


    public void RunKitchen(CancellationToken cancellationToken)
    {
        //get cooks 
        //initialize threads
        //look for orders to cook
        while (!cancellationToken.IsCancellationRequested)
        {
            var order = _orderService.GetOrder();
            if (order != null)
            {
                _orderService.RemoveOrder(order);
                PrintConsole.Write("Order "+ order.Id+" being processed", ConsoleColor.DarkBlue);
                PrepareOrder(order);
            }
            Thread.Sleep(4000);
        }
    }

    public void PrepareOrder(Order order)
    {
        _orderService.PrepareOrder(order);
    }
    
}
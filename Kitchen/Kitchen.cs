using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Repositories.CookRepository;
using Kitchen.Repositories.OrderListRepository;
using Kitchen.Services.CookService;
using Kitchen.Services.FoodService;
using Kitchen.Services.OrderService;

namespace Kitchen.Kitchen;

public class Kitchen : IKitchen
{
    private readonly ICookService _cookService;
    private readonly IOrderService _orderService;
    private readonly IFoodService _foodService;
    private static Mutex _mutex = new();

    public Kitchen(IOrderService orderService, ICookService cookService, IFoodService foodService)
    {
        _orderService = orderService;
        _cookService = cookService;
        _foodService = foodService;
    }

    private void InitializeKitchen()
    {
        _cookService.TestConfiguration();
        _foodService.GenerateMenu();
    }

    public void ExecuteCode(CancellationToken cancellationToken)
    {
        InitializeKitchen();
        //get cooks 
        //initialize threads
        RunThreads(cancellationToken);
        
        //look for orders to cook
        // RunKitchen(cancellationToken);
    }

    private async void RunThreads(CancellationToken cancellationToken)
    {
        Thread t1 = new Thread(() => RunKitchen(cancellationToken));
        Thread t2 = new Thread(() => RunKitchen(cancellationToken));
        Thread t3 = new Thread(() => RunKitchen(cancellationToken));
        Thread t4 = new Thread(() => RunKitchen(cancellationToken));
        Thread t5 = new Thread(() => RunKitchen(cancellationToken));
        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
        t5.Start();
    }

    public void RunKitchen(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var cook = _cookService.GetAvailableCook();
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
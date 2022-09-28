using System.Diagnostics.CodeAnalysis;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;
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

        Thread t6 = new Thread(() => SendOrder(cancellationToken));
        t6.Start();
    }

    private void SendOrder(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var order = _orderService.GetUnservedOrder();
            if (order != null)
            {
                PrintConsole.Write($"Found order with id {order.Id} ready to be sent", ConsoleColor.Blue);

                order.OrderStatus = OrderStatus.Served;
                _orderService.SendOrder(order);
            }
            else
            {
                Thread.Sleep(2 * Settings.Settings.TimeUnit);
            }
        }
    }

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public void RunKitchen(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var cook = _cookService.GetAvailableCook();
            if (cook == null)
            {
                Thread.Sleep(4 * Settings.Settings.TimeUnit);
                continue;
            }

            var foodsToCook = _foodService.GetOptimalFoodsToCook(cook.Proficiency, cook.MaxFoodsCanCook);
            if (foodsToCook != null)
            {
                PrintConsole.Write($"Cook with id {cook.Id} is preparing foods: {foodsToCook.Count}", ConsoleColor.DarkCyan);
                _cookService.MakeCookBusy(cook, foodsToCook);
            }
            else
            {
                cook.IsBusy = false;

                Thread.Sleep(4 * Settings.Settings.TimeUnit);
            }
        }
    }
}
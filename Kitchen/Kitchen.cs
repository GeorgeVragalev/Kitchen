using System.Diagnostics.CodeAnalysis;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;
using Kitchen.Services.CookingApparatusService;
using Kitchen.Services.CookService;
using Kitchen.Services.FoodService;
using Kitchen.Services.OrderService;

namespace Kitchen.Kitchen;

public class Kitchen : IKitchen
{
    private readonly ICookService _cookService;
    private readonly IOrderService _orderService;
    private readonly IFoodService _foodService;
    private readonly ICookingApparatusService _apparatusService;

    public Kitchen(IOrderService orderService, ICookService cookService, IFoodService foodService, ICookingApparatusService apparatusService)
    {
        _orderService = orderService;
        _cookService = cookService;
        _foodService = foodService;
        _apparatusService = apparatusService;
    }

    private void InitializeKitchen()
    {
        _cookService.TestConfiguration();
        _foodService.GenerateMenu();
        _apparatusService.TestConfiguration();
    }

    public void ExecuteCode(CancellationToken cancellationToken)
    {
        InitializeKitchen();
        RunThreads(cancellationToken);
    }

    private async void RunThreads(CancellationToken cancellationToken)
    {
        Thread t1 = new Thread(() => RunKitchen(cancellationToken));
        Thread t2 = new Thread(() => RunKitchen(cancellationToken));
        Thread t3 = new Thread(() => RunKitchen(cancellationToken));
        Thread t4 = new Thread(() => RunKitchen(cancellationToken));
        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();

        Thread t5 = new Thread(() => SendOrder(cancellationToken));
        t5.Start();
    }

    private async Task SendOrder(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var order = await _orderService.GetUnservedOrder();
            if (order != null)
            {
                PrintConsole.Write($"Found order with id {order.Id} ready to be sent", ConsoleColor.Blue);

                order.OrderStatusEnum = OrderStatusEnum.Served;
                _orderService.SendOrder(order);
            }
            else
            {
                await _orderService.CleanServedOrders();
                Thread.Sleep(2 * Settings.Settings.TimeUnit);
            }
        }
    }

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public async Task RunKitchen(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (!await _foodService.IsOrderListEmpty())
            {
                var cook = await _cookService.GetAvailableCook();
                _cookService.MakeCookBusy(cook);
                break;
            }
            else
            {
                Thread.Sleep(5000);
            }
        }
    }
}
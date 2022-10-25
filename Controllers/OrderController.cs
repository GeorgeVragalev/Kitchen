using Kitchen.Helpers;
using Kitchen.Kitchen;
using Kitchen.Models;
using Kitchen.Services.FoodService;
using Kitchen.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers;

[ApiController]
[Route("/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IFoodService _foodService;

    public OrderController(IOrderService orderService, IFoodService foodService)
    {
        _orderService = orderService;
        _foodService = foodService;
    }

    [HttpPost]
    public async Task Order([FromBody] CollectedOrder collectedOrder)
    {
        PrintConsole.Write($"{collectedOrder.OrderType} Order {collectedOrder.Id} with {collectedOrder.Foods.Count} foods received in kitchen", ConsoleColor.Green);

        var order = await collectedOrder.MapFinishedOrder();
        //todo configure orders
        await _foodService.AddFoodsToList(collectedOrder.Foods, order);
        await _orderService.AddOrderToList(order);
    }

    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}
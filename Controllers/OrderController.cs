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
    public void Order([FromBody] CollectedOrder collectedOrder)
    {
        var order = collectedOrder.MapFinishedOrder();
        //todo configure orders
        _foodService.AddFoodsToList(collectedOrder.Foods, order);
        _orderService.AddOrderToList(order);
        PrintConsole.Write("Order " + order.Id + " received in kitchen", ConsoleColor.Green);
    }

    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}
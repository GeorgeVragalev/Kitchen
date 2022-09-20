using Kitchen.Helpers;
using Kitchen.Kitchen;
using Kitchen.Models;
using Kitchen.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers;

[ApiController]
[Route("/order")]
public class OrderController : ControllerBase
{
    private readonly IKitchen _kitchen;
    private readonly IOrderService _orderService;


    public OrderController(IKitchen kitchen, IOrderService orderService)
    {
        _kitchen = kitchen;
        _orderService = orderService;
    }

    [HttpPost]
    public void Order([FromBody] CollectedOrder collectedOrder)
    {
        var order = collectedOrder.MapFinishedOrder();
        PrintConsole.Write("Order "+ order.Id+" received in kitchen", ConsoleColor.Green);

        _orderService.AddOrderToList(order);
        // return new JsonResult(order);
    }
    
    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}
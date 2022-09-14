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
    private readonly ILogger<OrderController> _logger;


    public OrderController(IKitchen kitchen, IOrderService orderService, ILogger<OrderController> logger)
    {
        _kitchen = kitchen;
        _orderService = orderService;
        _logger = logger;
    }

    [HttpPost]
    public void Order([FromBody] CollectedOrder collectedOrder)
    {
        var order = collectedOrder.MapFinishedOrder();
        Console.WriteLine("Order "+ order.Id+" received in kitchen");
        
        _kitchen.RunKitchen(order);
        // return new JsonResult(order);
    }
    
    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}
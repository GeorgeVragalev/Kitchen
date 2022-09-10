using DiningHall.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controllers;

[ApiController]
[Route("api/controller/order")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public ActionResult Index()
    {
        var order = new Order()
        {
            Id = 1,
            Foods = new List<int>() {1, 2, 4},
            MaxWait = 30,
            TableId = 2,
            WaiterId = 1
        };
        
        return new JsonResult(order);
    }
    
    [HttpGet]
    public ActionResult Get()
    {
        var order = new Order()
        {
            Id = 2,
            Foods = new List<int>() {4},
            MaxWait = 3,
            TableId = 1,
            WaiterId = 1
        };
        
        return new JsonResult(order);
    }
}
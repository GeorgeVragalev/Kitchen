using System.Text;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Repositories.OrderListRepository;
using Newtonsoft.Json;

namespace Kitchen.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IOrderListRepository _orderListRepository;

    public OrderService(IOrderListRepository orderListRepository)
    {
        _orderListRepository = orderListRepository;
    }

    public async void SendOrder(Order order)
    {
        try
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Settings.Settings.DiningHallUrl;
            using var client = new HttpClient();

            await client.PostAsync(url, data);
            Console.WriteLine();
            PrintConsole.Write("Order "+ order.Id+" ready to be served", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            PrintConsole.Write("Failed to send order with id:" + order.Id, ConsoleColor.Red);
        }
    }

    public void PrepareOrder(Order order)
    {
        order.CookingDetails = new List<CookingDetails>();
        order.CookingTime = order.MaxWait - RandomGenerator.NumberGenerator(Settings.Settings.Cooks);
        foreach (var foodId in order.Foods)
        {
            var cookingDetails = new CookingDetails();
            var cookId = RandomGenerator.NumberGenerator(Settings.Settings.Cooks);
            cookingDetails.CookId = cookId;
            cookingDetails.FoodId = foodId;

            order.CookingDetails.Add(cookingDetails);
        }
        
        PrintConsole.Write("Order "+ order.Id+" prepared", ConsoleColor.DarkBlue);

        SendOrder(order);
    }

    public void AddOrderToList(Order order)
    {
        _orderListRepository.AddOrderToList(order);   
    }

    public Order GetOrder()
    {
        var order = _orderListRepository.GetOrder();
        return order;
    }

    public void RemoveOrder(Order order)
    {
        _orderListRepository.RemoveOrder(order);
    }
}
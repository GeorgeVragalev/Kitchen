﻿using System.Text;
using Kitchen.Models;
using Newtonsoft.Json;

namespace Kitchen.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;

    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }

    public async void SendOrder(Order order)
    {
        try
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Settings.Settings.DiningHallUrl;
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            _logger.LogInformation("Order "+ order.Id+" finished");

            var result = await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to send order with id:" + order.Id);
        }
    }
}
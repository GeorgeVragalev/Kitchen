﻿using System.Text;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;
using Kitchen.Repositories.OrderListRepository;
using Kitchen.Services.FoodService;
using Newtonsoft.Json;

namespace Kitchen.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IOrderListRepository _orderListRepository;
    private readonly IFoodService _foodService;

    public OrderService(IOrderListRepository orderListRepository, IFoodService foodService)
    {
        _orderListRepository = orderListRepository;
        _foodService = foodService;
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
            order.OrderStatusEnum = OrderStatusEnum.Served;
            Console.WriteLine();
            PrintConsole.Write("Order " + order.Id + " ready to be served", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            PrintConsole.Write("Failed to send order with id:" + order.Id, ConsoleColor.Red);
        }
    }

    public void AddOrderToList(Order order)
    {
        _orderListRepository.AddOrderToList(order);
    }

    public Order GetUnservedOrder()
    {
        var orders = _orderListRepository.GetUnservedOrders();

        foreach (var order in orders)
        {
            var foods = _foodService.GetFoodsByOrder(order.Id);
            bool completeOrder = foods.CheckStatus();
            if (completeOrder)
            {
                foreach (var food in foods)
                {
                    _foodService.ChangeFoodStatus(food, FoodStatusEnum.Complete);
                }
                return order;
            }
        }

        return null;
    }

    public Task CleanServedOrders()
    {
        _orderListRepository.CleanServedOrders();
        return Task.CompletedTask;
    }
}
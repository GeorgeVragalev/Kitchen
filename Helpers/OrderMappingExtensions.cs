using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Helpers;

public static class ExtensionMethods
{
    private static Mutex _mutex = new();

    public static async Task<Order> MapFinishedOrder(this CollectedOrder order)
    {
        _mutex.WaitOne();
        var finishedOrder =  new Order()
        {
            Id = order.Id,
            Priority = order.Priority,
            Foods = order.Foods,
            CookingDetails = new List<CookingDetails>(),
            OrderStatusEnum = OrderStatusEnum.IsCooking,
            MaxWait = order.MaxWait,
            CookingTime = 0,
            TableId = order.TableId,
            WaiterId = order.WaiterId,
            PickUpTime = order.PickUpTime,
            ClientId = order.ClientId,
            RestaurantId = order.RestaurantId,
            GroupOrderId = order.GroupOrderId,
            OrderType = order.OrderType
        };
        _mutex.ReleaseMutex();
        return await Task.FromResult(finishedOrder);
    }

    public static bool CheckStatus(this IList<Food> foods)
    {
        if (foods.Count == 0)
        {
            return false;
        }
        foreach (var food in foods)
        {
            if (food.FoodStatusEnum != FoodStatusEnum.Cooked)
            {
                return false;
            }
        }
        PrintConsole.Write($"{foods.Count} foods for order ID {foods[0].OrderId} ARE PREPARED", ConsoleColor.DarkCyan);

        return true;
    }
   
    public static void PrepareFood(this Food food, int waitTime)
    {
        Thread.Sleep(waitTime * Settings.Settings.TimeUnit);
    }
}
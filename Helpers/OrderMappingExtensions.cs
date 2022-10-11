using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Helpers;

public static class ExtensionMethods
{
    public static async Task<Order> MapFinishedOrder(this CollectedOrder order)
    {
        var finishedOrder =  new Order()
        {
            Id = order.Id,
            Priority = order.Priority,
            FoodsPreparedCount = 0,
            Foods = order.Foods,
            CookingDetails = new List<CookingDetails>(),
            OrderStatusEnum = OrderStatusEnum.IsCooking,
            MaxWait = order.MaxWait,
            CookingTime = 0,
            TableId = order.TableId,
            WaiterId = order.WaiterId,
            PickUpTime = order.PickUpTime
        };
        return await Task.FromResult(finishedOrder);
    }

    public static bool CheckFoodEquality(this Food food, Food foodInList)
    {
        if (food.Id == foodInList.Id &&
            food.PreparationTime == foodInList.PreparationTime &&
            food.Complexity == foodInList.Complexity &&
            food.Priority == foodInList.Priority &&
            food.OrderId == foodInList.OrderId &&
            food.Name == foodInList.Name &&
            food.CookingApparatus == foodInList.CookingApparatus &&
            food.FoodStatusEnum == foodInList.FoodStatusEnum
            )
        {
            return true;
        }

        return false;
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
    public static int Apply(this int number)
    {
        return number / 10;
    }
    
    public static void PrepareFood(this Food food, int waitTime)
    {
        Thread.Sleep(waitTime * Settings.Settings.TimeUnit);
    }
}
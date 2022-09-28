using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Helpers;

public static class OrderMappingExtension
{
    public static Order MapFinishedOrder(this CollectedOrder order)
    {
        var finishedOrder = new Order()
        {
            Id = order.Id,
            Priority = order.Priority,
            FoodsPreparedCount = 0,
            Foods = order.Foods,
            CookingDetails = new List<CookingDetails>(),
            OrderStatus = OrderStatus.IsCooking,
            MaxWait = order.MaxWait,
            CookingTime = 0,
            TableId = order.TableId,
            WaiterId = order.WaiterId,
            PickUpTime = order.PickUpTime
        };
        return finishedOrder;
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
            food.FoodStatus == foodInList.FoodStatus
            )
        {
            return true;
        }

        return false;
    }

    public static bool CheckStatus(this IList<Food> foods)
    {
        foreach (var food in foods)
        {
            if (food.FoodStatus == FoodStatus.NotPrepared)
            {
                return false;
            }
        }

        return true;
    }
    public static int Apply(this int number)
    {
        return number / 10;
    }
}
using Kitchen.Models;

namespace Kitchen.Helpers;

public static class OrderMappingExtension
{
    public static Order MapFinishedOrder(this CollectedOrder order)
    {
        var finishedOrder = new Order()
        {
            Id = order.Id,
            Priority = order.Priority,
            Foods = order.Foods,
            CookingDetails = new List<CookingDetails>(),
            MaxWait = order.MaxWait,
            CookingTime = 0,
            TableId = order.TableId,
            WaiterId = order.WaiterId,
            PickUpTime = order.PickUpTime
        };
        return finishedOrder;
    } 
}
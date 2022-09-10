namespace DiningHall.Models;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int WaiterId { get; set; }
    public int Priority { get; set; }
    public int CookingTime { get; set; }
    public int MaxWait { get; set; }
    public IList<int> Foods { get; set; }
    public IList<CookingDetails> CookingDetails { get; set; }

    public Order()
    {
    }


    public Order(int id, int tableId, int waiterId, int maxWait, IList<int> foods, int cookingTime, IList<CookingDetails> cookingDetails, int priority)
    {
        Id = id;
        TableId = tableId;
        WaiterId = waiterId;
        MaxWait = maxWait;
        Foods = foods;
        CookingTime = cookingTime;
        CookingDetails = cookingDetails;
        Priority = priority;
    }
}
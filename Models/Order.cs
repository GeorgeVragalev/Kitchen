
using Kitchen.Models.Enums;
using Newtonsoft.Json;

namespace Kitchen.Models;

public class Order : BaseEntity
{ 
    public int TableId { get; set; }
    public int WaiterId { get; set; }
    public int Priority { get; set; }
    public int CookingTime { get; set; }
    public int MaxWait { get; set; }
    [JsonIgnore]
    public int FoodsPreparedCount { get; set; }
    [JsonIgnore]
    public OrderStatusEnum OrderStatusEnum { get; set; }
    public IList<int> Foods { get; set; }
    public DateTime PickUpTime { get; set; }
    public IList<CookingDetails> CookingDetails { get; set; }

    public Order() { }
}
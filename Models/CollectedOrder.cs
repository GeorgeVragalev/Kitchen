﻿using Kitchen.Models.Enums;
using Newtonsoft.Json;

namespace Kitchen.Models;

public class CollectedOrder: BaseEntity
{
    public int? ClientId { get; set; }
    public int? GroupOrderId { get; set; }
    public int RestaurantId { get; set; }
    public int? TableId { get; set; }
    public int? WaiterId { get; set; } 
    public int Priority { get; set; }
    public int MaxWait { get; set; }
    public OrderType OrderType { get; set; }
    public DateTime PickUpTime { get; set; }
    public IList<int> Foods { get; set; }
    public OrderStatusEnum OrderStatusEnum { get; set; }
}
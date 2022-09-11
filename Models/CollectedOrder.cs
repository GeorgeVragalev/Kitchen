﻿namespace Kitchen.Models;

public class CollectedOrder: BaseEntity
{
    public int TableId { get; set; }
    public int WaiterId { get; set; } 
    public int Priority { get; set; }
    public int MaxWait { get; set; }
    public DateTime PickUpTime { get; set; }
    public IList<int> Foods { get; set; }

    public CollectedOrder() { }
}
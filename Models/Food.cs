using Kitchen.Models.Enums;

namespace Kitchen.Models;

public class Food : BaseEntity
{
    public string Name { get; set; }
    public int PreparationTime { get; set; }
    public int Complexity { get; set; }
    public int Priority { get; set; }
    public CookingApparatus CookingApparatus { get; set; }
    public FoodStatus FoodStatus { get; set; }
    public int OrderId { get; set; }

    public Food() { }
}
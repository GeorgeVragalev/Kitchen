namespace Kitchen.Models;

public class Food : BaseEntity
{
    public string Name { get; set; }

    public int PreparationTime { get; set; }

    public int Complexity { get; set; }

    public CookingApparatus CookingApparatus { get; set; }

    public Food() { }

    public Food(string name, int preparationTime, int complexity, CookingApparatus cookingApparatus)
    {
        Name = name;
        PreparationTime = preparationTime;
        Complexity = complexity;
        CookingApparatus = cookingApparatus;
    }
}
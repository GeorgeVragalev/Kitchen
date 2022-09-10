namespace DiningHall.Models;

public class Food
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int PreparationTime { get; set; }

    public int Complexity { get; set; }

    public CookingApparatus CookingApparatus { get; set; }

    public Food() { }

    public Food(int id, string name, int preparationTime, int complexity, CookingApparatus cookingApparatus)
    {
        Id = id;
        Name = name;
        PreparationTime = preparationTime;
        Complexity = complexity;
        CookingApparatus = cookingApparatus;
    }
}
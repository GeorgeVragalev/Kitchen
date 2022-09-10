namespace DiningHall.Models;

public class CookingApparatus
{
    public int Id { get; set; }
    public int Name { get; set; }
    public int EstimateTime { get; set;}
    public bool IsBusy { get; set;}

    public CookingApparatus() { }

    public CookingApparatus(int id, int name, int estimateTime, bool isBusy)
    {
        Id = id;
        Name = name;
        EstimateTime = estimateTime;
        IsBusy = isBusy;
    }
}
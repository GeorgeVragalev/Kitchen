namespace Kitchen.Models;

public class CookingApparatus : BaseEntity
{
    public int Name { get; set; }
    public int EstimateTime { get; set;}
    public bool IsBusy { get; set;}

    public CookingApparatus() { }

    public CookingApparatus(int name, int estimateTime, bool isBusy)
    {
        Name = name;
        EstimateTime = estimateTime;
        IsBusy = isBusy;
    }
}
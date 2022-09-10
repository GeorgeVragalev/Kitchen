namespace DiningHall.Models;

public class Cook
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rank { get; set; }
    public int Proficiency { get; set; }   
    public string CatchPhrase { get; set; }
    public bool IsBusy { get; set; }

    public Cook(){}

    public Cook(int id, string name, int rank, int proficiency, string catchPhrase, bool isBusy)
    {
        Id = id;
        Name = name;
        Rank = rank;
        Proficiency = proficiency;
        CatchPhrase = catchPhrase;
        IsBusy = isBusy;
    }
}  
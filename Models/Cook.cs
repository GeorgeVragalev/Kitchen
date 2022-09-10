namespace Kitchen.Models;

public class Cook : BaseEntity
{
    public string Name { get; set; }
    public int Rank { get; set; }
    public int Proficiency { get; set; }   
    public string CatchPhrase { get; set; }
    public bool IsBusy { get; set; }

    public Cook(){}

    public Cook( string name, int rank, int proficiency, string catchPhrase, bool isBusy)
    {
        Name = name;
        Rank = rank;
        Proficiency = proficiency;
        CatchPhrase = catchPhrase;
        IsBusy = isBusy;
    }
}  
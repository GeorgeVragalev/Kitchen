namespace Kitchen.Models;

public class Cook : BaseEntity
{
    public string Name { get; set; }
    public int Rank { get; set; }
    public int Proficiency { get; set; }   
    public string CatchPhrase { get; set; }
    public bool IsBusy { get; set; }
    public int MaxFoodsCanCook { get; set; }

    public Cook(){}
}  
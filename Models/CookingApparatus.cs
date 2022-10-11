using Kitchen.Models.Enums;

namespace Kitchen.Models;

public class CookingApparatus : BaseEntity
{
    public CookingApparatusEnum Name { get; set; }
    public bool IsBusy { get; set;}

    public CookingApparatus() { }
}
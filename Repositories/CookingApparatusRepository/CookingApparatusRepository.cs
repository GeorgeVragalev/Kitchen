using System.Collections.Concurrent;
using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Repositories.CookingApparatusRepository;

public class CookingApparatusRepository : ICookingApparatusRepository
{
    private readonly ConcurrentBag<CookingApparatus?> _cookingApparatus = new ConcurrentBag<CookingApparatus?>();
    
    public void TestConfiguration()
    {
        var oven1 = new CookingApparatus()
        {
            Id = 1,
            Name = CookingApparatusEnum.Oven,
            IsBusy = false
        };
        
        var oven2 = new CookingApparatus()
        {
            Id = 2,
            Name = CookingApparatusEnum.Oven,
            IsBusy = false
        };

        var stove = new CookingApparatus()
        {
            Id = 2,
            Name = CookingApparatusEnum.Stove,
            IsBusy = false
        };

        _cookingApparatus.Add(oven1);
        _cookingApparatus.Add(oven2);
        _cookingApparatus.Add(stove);
    }

    public async Task<CookingApparatus?> GetFreeApparatusByName(CookingApparatusEnum name)
    {
        var apparatus =  await Task.FromResult(_cookingApparatus.Where(a=>a.IsBusy == false).FirstOrDefault(apparatus => apparatus!.Name.Equals(name)));
        return apparatus;
    }
}
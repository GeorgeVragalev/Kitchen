using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Services.CookingApparatusService;

public interface ICookingApparatusService
{
    public Task<CookingApparatus?> GetApparatusByName(CookingApparatusEnum name);
    public Task<bool> PrepareFood(Food food);
    public void TestConfiguration();
}
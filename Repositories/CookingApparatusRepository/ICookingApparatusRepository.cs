using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Repositories.CookingApparatusRepository;

public interface ICookingApparatusRepository
{
    public void TestConfiguration();
    public Task<CookingApparatus?> GetFreeApparatusByName(CookingApparatusEnum name);
}
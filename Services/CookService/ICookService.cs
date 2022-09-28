using Kitchen.Models;

namespace Kitchen.Services.CookService;

public interface ICookService
{
    public void GenerateCooks();
    public void TestConfiguration();
    public Cook GetAvailableCook();
    public void MakeCookBusy(Cook cook, IList<Food> foodsToCook);
}
using Kitchen.Models;

namespace Kitchen.Services.CookService;

public interface ICookService
{
    public void GenerateCooks();
    public void TestConfiguration();
    public Task<Cook?> GetAvailableCook();
    public void MakeCookBusy(Cook cook);
}
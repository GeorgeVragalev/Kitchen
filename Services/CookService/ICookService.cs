using Kitchen.Models;

namespace Kitchen.Services.CookService;

public interface ICookService
{
    public void GenerateCooks();
    public Task<Cook> GetAvailableCook();
    public void TestConfiguration();
}
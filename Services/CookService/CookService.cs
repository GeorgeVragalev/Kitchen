using Kitchen.Models;
using Kitchen.Repositories.CookRepository;

namespace Kitchen.Services.CookService;

public class CookService : ICookService
{
    private readonly ICookRepository _cookRepository;

    public CookService(ICookRepository cookRepository)
    {
        _cookRepository = cookRepository;
    }

    public void GenerateCooks()
    {
        _cookRepository.GenerateCooks();
    }
    
    public void TestConfiguration()
    {
        _cookRepository.TestConfiguration();
    }

    public async Task<Cook> GetAvailableCook()
    {
        var cook = await _cookRepository.GetAvailableCook();
        if (cook!=null)
        {
            return cook;
        }

        return null;
    }
}
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
}
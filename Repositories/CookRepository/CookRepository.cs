using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Repositories.CookRepository;

public class CookRepository : ICookRepository
{
    private readonly ConcurrentBag<Cook> _cooks = new ConcurrentBag<Cook>();

    public ConcurrentBag<Cook> GenerateCooks()
    {
        for (int i = 1; i <= Settings.Settings.Cooks; i++)
        {
            var cook = new Cook()
            {
                Id = i,
                Proficiency = (i>3 ? 3 : i),
                Name = "Gordon Ramsey:" + i,
                CatchPhrase = "Hey panini head, are you listening to me?"
            };
            _cooks.Add(cook);
        }

        return _cooks;
    }

    public Task<Cook> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public ConcurrentBag<Cook> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Cook> GetAvailableCook()
    {
        throw new NotImplementedException();
    }
}
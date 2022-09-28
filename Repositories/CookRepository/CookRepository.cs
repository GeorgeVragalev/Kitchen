using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Repositories.CookRepository;

public class CookRepository : ICookRepository
{
    private readonly ConcurrentBag<Cook> _cooks = new ConcurrentBag<Cook>();
    private static Mutex _mutex = new();

    public void GenerateCooks()
    {
        for (int i = 1; i <= Settings.Settings.Cooks; i++)
        {
            var cook = new Cook()
            {
                Id = i,
                Rank = (i > 3 ? 3 : i),
                Proficiency = (i > 4 ? 4 : i + 1),
                IsBusy = false,
                MaxFoodsCanCook = (i > 4 ? 4 : i + 1),
                Name = "Gordon Ramsey:" + i,
                CatchPhrase = "Hey panini head, are you listening to me?"
            };
            _cooks.Add(cook);
        }
    }

    public void TestConfiguration()
    {
        var cook1 = new Cook()
        {
            Id = 1,
            Rank = 1,
            Proficiency = 2,
            IsBusy = false,
            MaxFoodsCanCook = 2,
            Name = "Gordon Ramsey nr 1",
            CatchPhrase = "Hey panini head, are you listening to me?"
        };
        var cook2 = new Cook()
        {
            Id = 2,
            Rank = 2,
            Proficiency = 2,
            IsBusy = false,
            MaxFoodsCanCook = 2,
            Name = "Gordon Ramsey nr 2",
            CatchPhrase = "Hey panini head, are you listening to me?"
        };
        var cook3 = new Cook()
        {
            Id = 3,
            Rank = 2,
            Proficiency = 3,
            IsBusy = false,
            MaxFoodsCanCook = 3,
            Name = "Gordon Ramsey nr 3",
            CatchPhrase = "Hey panini head, are you listening to me?"
        };
        var cook4 = new Cook()
        {
            Id = 4,
            Rank = 3,
            Proficiency = 4,
            IsBusy = false,
            MaxFoodsCanCook = 4,
            Name = "Gordon Ramsey nr 4",
            CatchPhrase = "Hey panini head, are you listening to me?"
        };
        
        _cooks.Add(cook1);
        _cooks.Add(cook2);
        _cooks.Add(cook3);
        _cooks.Add(cook4);
    }

    public Task<Cook> GetById(int id)
    {
        foreach (var cook in _cooks)
        {
            if (cook.Id == id)
            {
                return Task.FromResult(cook);
            }
        }

        return Task.FromResult<Cook>(null!);
    }

    public ConcurrentBag<Cook> GetAll()
    {
        return _cooks;
    }

    public Cook GetAvailableCook()
    {
        _mutex.WaitOne();
        foreach (var cook in _cooks)
        {
            if (cook.MaxFoodsCanCook > 0 && !cook.IsBusy)
            {
                cook.IsBusy = true;
                _mutex.ReleaseMutex();
                return cook;
            }
        }
        
        _mutex.ReleaseMutex();
        return null;
    }
}
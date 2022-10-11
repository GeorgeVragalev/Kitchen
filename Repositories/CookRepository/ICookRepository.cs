using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Repositories.CookRepository;

public interface ICookRepository
{
    public void GenerateCooks();
    public void TestConfiguration();
    public Task<Cook?> GetById(int id);
    public ConcurrentBag<Cook?> GetAll();
    public Task<Cook?> GetAvailableCook();
}
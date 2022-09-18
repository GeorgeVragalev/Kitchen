using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Repositories.CookRepository;

public interface ICookRepository
{
    public ConcurrentBag<Cook> GenerateCooks();
    public Task<Cook> GetById(int id);
    public ConcurrentBag<Cook> GetAll();
    public Task<Cook> GetAvailableCook();
}
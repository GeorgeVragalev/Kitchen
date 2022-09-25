using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Repositories.FoodRepository;

public interface IFoodRepository
{
    public ConcurrentBag<Food> GenerateFood();
    public Task<Food> GetFoodById(int id);
}
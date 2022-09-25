using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Services.FoodService;

public interface IFoodService
{
    public ConcurrentBag<Food> GenerateMenu();
    public Task<Food> GetFoodById(int id);
}
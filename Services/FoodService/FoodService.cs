using System.Collections.Concurrent;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Repositories.FoodRepository;

namespace Kitchen.Services.FoodService;

public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public  ConcurrentBag<Food> GenerateMenu()
    {
        return _foodRepository.GenerateFood();
    }

    public Task<Food> GetFoodById(int id)
    {
        return _foodRepository.GetFoodById(id);
    }
}
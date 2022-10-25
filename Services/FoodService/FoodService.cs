using System.Collections.Concurrent;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;
using Kitchen.Repositories.FoodRepository;
using Kitchen.Services.OrderService;

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

    public async Task<Food> GetFoodById(int id)
    {
        return await _foodRepository.GetFoodById(id);
    }

    public async Task<IList<Food>> GetFoodsByIds(IList<int> foods)
    {
        return await _foodRepository.GetFoodsByIds(foods);
    }
    
    public async Task AddFoodsToList(IList<int> foods, Order order)
    {
        var foodsList = await _foodRepository.GetFoodsByIds(foods);
        _foodRepository.AddFoodsToList(foodsList, order);
    }

    public async Task<Food?> GetOptimalFoodToCook(int cookProficiency, bool isClientFood)
    {
        var food = await _foodRepository.GetOptimalFoodToCook(cookProficiency, isClientFood);
        return await Task.FromResult(food);
    }

    public async Task ChangeFoodStatus(Food food, FoodStatusEnum foodStatus)
    {
        await _foodRepository.ChangeFoodStatus(food, FoodStatusEnum.Cooked);
    }

    public async Task<IList<Food>> GetFoodsByOrder(int orderId)
    {
        return await Task.FromResult(_foodRepository.GetFoodsByOrder(orderId));
    }

    public void PrintFoods()
    {
        _foodRepository.PrintFoods();
    }
    
    public async Task<bool> IsOrderListEmpty()
    {
        return await Task.FromResult(await _foodRepository.IsOrderListEmpty());
    }
}
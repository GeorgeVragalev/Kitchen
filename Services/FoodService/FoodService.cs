using System.Collections.Concurrent;
using Kitchen.Helpers;
using Kitchen.Models;
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

    public Task<Food> GetFoodById(int id)
    {
        return _foodRepository.GetFoodById(id);
    }

    public IList<Food> GetFoodsByIds(IList<int> foods)
    {
        return _foodRepository.GetFoodsByIds(foods);
    }
    
    public void AddFoodsToList(IList<int> foods, Order order)
    {
        var foodsList = GetFoodsByIds(foods);
        _foodRepository.AddFoodsToList(foodsList, order);
    }

    public IList<Food> GetOptimalFoodsToCook(int cookProficiency, int maxFoodsCanCook)
    {
        var foods = _foodRepository.GetOptimalFoodsToCook(cookProficiency, maxFoodsCanCook);
        return foods;
    }

    public void MarkFoodAsCooked(Food food)
    {
        _foodRepository.MarkFoodAsCooked(food);
        //todo make another way to check if an order is ready
        // _orderService.IncrementPreparedFoodCounter(food.OrderId);
    }

    public IList<Food> GetFoodsByOrder(int orderId)
    {
        return _foodRepository.GetFoodsByOrder(orderId);
    }
}
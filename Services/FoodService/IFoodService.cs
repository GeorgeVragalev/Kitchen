using System.Collections.Concurrent;
using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Services.FoodService;

public interface IFoodService
{
    public ConcurrentBag<Food> GenerateMenu();
    public Task<Food> GetFoodById(int id);
    public Task<IList<Food>> GetFoodsByIds(IList<int> foods);
    public Task AddFoodsToList(IList<int> foods, Order order);
    public Task<Food?> GetOptimalFoodToCook(int cookProficiency);
    public IList<Food> GetFoodsByOrder(int orderId);
    public void ChangeFoodStatus(Food food, FoodStatusEnum foodStatus);
    public void PrintFoods();
}
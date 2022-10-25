using System.Collections.Concurrent;
using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Repositories.FoodRepository;

public interface IFoodRepository
{
    public ConcurrentBag<Food> GenerateFood();
    public Task<Food> GetFoodById(int id);
    public Task<IList<Food>> GetFoodsByIds(IList<int> foods);
    public void AddFoodsToList(IList<Food> foods,Order order);
    public Task<Food?> GetOptimalFoodToCook(int cookProficiency, bool isClientFood);
    public Task ChangeFoodStatus(Food food, FoodStatusEnum foodStatus);
    public IList<Food> GetFoodsByOrder(int orderId);
    public void PrintFoods();
    public Task<bool> IsOrderListEmpty();
}
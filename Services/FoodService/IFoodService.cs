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
    public Task<Food?> GetOptimalFoodToCook(int cookProficiency, bool isClientFood);
    public Task<IList<Food>> GetFoodsByOrder(int orderId);
    public Task ChangeFoodStatus(Food food, FoodStatusEnum foodStatus);
    public void PrintFoods();
    public Task<bool> IsOrderListEmpty();
}
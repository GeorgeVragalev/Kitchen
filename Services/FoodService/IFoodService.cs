using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Services.FoodService;

public interface IFoodService
{
    public ConcurrentBag<Food> GenerateMenu();
    public Task<Food> GetFoodById(int id);
    public IList<Food> GetFoodsByIds(IList<int> foods);
    public void AddFoodsToList(IList<int> foods, Order order);
    public IList<Food> GetOptimalFoodsToCook(int cookProficiency, int maxFoodsCanCook);
    public IList<Food> GetFoodsByOrder(int orderId);
    public void MarkFoodAsCooked(Food food);
}
using System.Collections.Concurrent;
using Kitchen.Models;

namespace Kitchen.Repositories.FoodRepository;

public interface IFoodRepository
{
    public ConcurrentBag<Food> GenerateFood();
    public Task<Food> GetFoodById(int id);
    public IList<Food> GetFoodsByIds(IList<int> foods);
    public void AddFoodsToList(IList<Food> foods,Order order);
    public IList<Food> GetOptimalFoodsToCook(int cookProficiency, int maxFoodsCanCook);
    public void MarkFoodAsCooked(Food food);
    public IList<Food> GetFoodsByOrder(int orderId);
}
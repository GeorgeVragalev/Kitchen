using System.Collections.Concurrent;
using System.Linq;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;

namespace Kitchen.Repositories.FoodRepository;

public class FoodRepository : IFoodRepository
{
    private readonly ConcurrentBag<Food> _menu = new ConcurrentBag<Food>();
    private readonly ConcurrentBag<Food> _foodsList = new ConcurrentBag<Food>();
    private static Mutex _mutex = new();

    public ConcurrentBag<Food> GenerateFood()
    {
        _menu.Add(new Food
        {
            Id = 1,
            Name = "Pizza",
            PreparationTime = 20,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 2,
            Name = "Salad",
            PreparationTime = 10,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 3,
            Name = " Zeama",
            PreparationTime = 7,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 4,
            Name = "Scallop Sashimi with Meyer Lemon Confit",
            PreparationTime = 32,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 5,
            Name = "Island Duck with Mulberry Mustard",
            PreparationTime = 35,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 6,
            Name = "Waffles",
            PreparationTime = 10,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 7,
            Name = "Aubergine",
            PreparationTime = 20,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 8,
            Name = "Lasagna",
            PreparationTime = 30,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 9,
            Name = "Burger",
            PreparationTime = 15,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 10,
            Name = "Gyros",
            PreparationTime = 15,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });

        _menu.Add(new Food
        {
            Id = 11,
            Name = "Kebab",
            PreparationTime = 15,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 12,
            Name = "UnagiMaki",
            PreparationTime = 20,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });
        _menu.Add(new Food
        {
            Id = 13,
            Name = "TobaccoChicken",
            PreparationTime = 30,
            OrderId = 0,
            FoodStatus = FoodStatus.NotPrepared,
            Complexity = 0,
            Priority = 0,
            CookingApparatus = null
        });

        return _menu;
    }

    public async Task<Food> GetFoodById(int id)
    {
        return _menu.AsQueryable().FirstOrDefault(f => f.Id == id)!;
    }

    public IList<Food> GetFoodsByIds(IList<int> foods)
    {
        var foodsList = new List<Food>();
        foreach (var foodId in foods)
        {
            var food = GetFoodById(foodId).Result;
            foodsList.Add(food);
        }

        return foodsList;
    }

    public void AddFoodsToList(IList<Food> foods, Order order)
    {
        foreach (var food in foods)
        {
            food.Priority = order.Priority;
            food.OrderId = order.Id;
            food.FoodStatus = FoodStatus.NotPrepared;
            _foodsList.Add(food);
        }
    }

    public void MarkFoodAsCooked(Food food)
    {
        var foodInList = _foodsList.AsQueryable().FirstOrDefault(f => f.CheckFoodEquality(food));
        foodInList.FoodStatus = FoodStatus.IsPrepared;
    }

    public IList<Food> GetOptimalFoodsToCook(int cookProficiency, int maxFoodsCanCook)
    {
        _mutex.WaitOne();
        if (_foodsList.Count != 0)
        {
            var foods = _foodsList.AsQueryable()
                .Where(f => f.FoodStatus == FoodStatus.NotPrepared)
                .Where(f => f.Complexity <= cookProficiency)
                .OrderBy(f => f.OrderId)
                .ThenBy(f => f.Priority)
                .ThenBy(f => f.PreparationTime)
                .Take(maxFoodsCanCook).ToList();

            if (foods.Count != 0)
            {
                // //todo remove foods from list
                // foreach (var food in foods)
                // {
                //     food.FoodStatus = FoodStatus.IsCooking;
                // }

                _mutex.ReleaseMutex();
                return foods;
            }
        }

        _mutex.ReleaseMutex();
        return null;
    }

    public IList<Food> GetFoodsByOrder(int orderId)
    {
        var foods =  _foodsList.Where(f => f.OrderId == orderId).ToList();
        return foods;
    }
}
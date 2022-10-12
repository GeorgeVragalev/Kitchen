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
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 2,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Oven
        });
        _menu.Add(new Food
        {
            Id = 2,
            Name = "Salad",
            PreparationTime = 10,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 1,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.None
        });
        _menu.Add(new Food
        {
            Id = 3,
            Name = " Zeama",
            PreparationTime = 7,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 1,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Stove
        });
        _menu.Add(new Food
        {
            Id = 4,
            Name = "Scallop Sashimi with Meyer Lemon Confit",
            PreparationTime = 32,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 3,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.None
        });
        _menu.Add(new Food
        {
            Id = 5,
            Name = "Island Duck with Mulberry Mustard",
            PreparationTime = 35,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 3,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Oven
        });
        _menu.Add(new Food
        {
            Id = 6,
            Name = "Waffles",
            PreparationTime = 10,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 1,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Stove
        });
        _menu.Add(new Food
        {
            Id = 7,
            Name = "Aubergine",
            PreparationTime = 20,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 2,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Oven
        });
        _menu.Add(new Food
        {
            Id = 8,
            Name = "Lasagna",
            PreparationTime = 30,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 2,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Oven
        });
        _menu.Add(new Food
        {
            Id = 9,
            Name = "Burger",
            PreparationTime = 15,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 1,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Stove
        });
        _menu.Add(new Food
        {
            Id = 10,
            Name = "Gyros",
            PreparationTime = 15,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 1,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.None
        });

        _menu.Add(new Food
        {
            Id = 11,
            Name = "Kebab",
            PreparationTime = 15,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 1,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.None
        });
        _menu.Add(new Food
        {
            Id = 12,
            Name = "UnagiMaki",
            PreparationTime = 20,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 2,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.None
        });
        _menu.Add(new Food
        {
            Id = 13,
            Name = "TobaccoChicken",
            PreparationTime = 30,
            OrderId = 0,
            FoodStatusEnum = FoodStatusEnum.NotPrepared,
            Complexity = 2,
            Priority = 0,
            CookingApparatus = CookingApparatusEnum.Oven
        });

        return _menu;
    }

    public async Task<Food> GetFoodById(int id)
    {
        var foodFromList = await Task.FromResult(_menu.AsQueryable().FirstOrDefault(f => f.Id == id)!);
        var food = new Food()
        {
            Id = foodFromList.Id,
            Complexity = foodFromList.Complexity,
            Name = foodFromList.Name,
            Priority = foodFromList.Priority,
            CookingApparatus = foodFromList.CookingApparatus,
            OrderId = foodFromList.OrderId,
            PreparationTime = foodFromList.PreparationTime,
            FoodStatusEnum = foodFromList.FoodStatusEnum,
        };
        return food;
    }

    public async Task<IList<Food>> GetFoodsByIds(IList<int> foods)
    {
        var foodsList = new List<Food>();
        foreach (var foodId in foods)
        {
            var food = await GetFoodById(foodId);
            foodsList.Add(food);
        }

        return await Task.FromResult<IList<Food>>(foodsList);
    }

    public void AddFoodsToList(IList<Food> foods, Order order)
    {
        foreach (var food in foods)
        {
            food.Priority = order.Priority;
            food.OrderId = order.Id;
            food.FoodStatusEnum = FoodStatusEnum.NotPrepared;
            _foodsList.Add(food);
        }
    }

    public void ChangeFoodStatus(Food food, FoodStatusEnum foodStatus)
    {
        _mutex.WaitOne();
        var foodInList = _foodsList.AsQueryable().FirstOrDefault(f => f.CheckFoodEquality(food));
        if (foodInList != null)
        {
            foodInList.FoodStatusEnum = foodStatus;
        }
        _mutex.ReleaseMutex();
    }

    public async Task<Food?> GetOptimalFoodToCook(int cookProficiency)
    {
        _mutex.WaitOne();
        if (_foodsList.Count != 0)
        {
            var food = _foodsList.AsQueryable()
                .Where(f => f.FoodStatusEnum == FoodStatusEnum.NotPrepared)
                .Where(f => f.Complexity == cookProficiency)
                .OrderBy(f => f.OrderId)
                .ThenBy(f => f.Priority)
                .ThenBy(f => f.PreparationTime)
                .FirstOrDefault();
            if (food == null && cookProficiency > 1)
            {
                food = _foodsList.AsQueryable()
                    .Where(f => f.FoodStatusEnum == FoodStatusEnum.NotPrepared)
                    .Where(f => f.Complexity == cookProficiency - 1)
                    .OrderBy(f => f.OrderId)
                    .ThenBy(f => f.Priority)
                    .ThenBy(f => f.PreparationTime)
                    .FirstOrDefault();
            }

            if (food != null)
            {
                food.FoodStatusEnum = FoodStatusEnum.IsCooking;
            }
            
            _mutex.ReleaseMutex();
            return await Task.FromResult(food);
        }

        _mutex.ReleaseMutex();
        return null;
    }

    public IList<Food> GetFoodsByOrder(int orderId)
    {
        var foods = _foodsList.Where(f => f.OrderId == orderId).ToList();
        return foods;
    }

    public void PrintFoods()
    {
        var foods = _foodsList.ToList();
        PrintConsole.Write($"-----------------------------------------------------------------------------", ConsoleColor.Magenta);
        foreach (var food in foods)     
        {
            PrintConsole.WriteSpace($"Food {food.Id} | Order: {food.OrderId} | Status: {food.FoodStatusEnum.ToString()} \n", ConsoleColor.Magenta);
        }
    }
}
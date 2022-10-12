using System.Diagnostics.CodeAnalysis;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;
using Kitchen.Repositories.CookRepository;
using Kitchen.Services.CookingApparatusService;
using Kitchen.Services.FoodService;
using Kitchen.Services.OrderService;

namespace Kitchen.Services.CookService;

public class CookService : ICookService
{
    private readonly ICookRepository _cookRepository;
    private readonly IFoodService _foodService;
    private readonly ICookingApparatusService _apparatusService;

    public CookService(ICookRepository cookRepository, IFoodService foodService, ICookingApparatusService apparatusService)
    {
        _cookRepository = cookRepository;
        _foodService = foodService;
        _apparatusService = apparatusService;
    }

    public void GenerateCooks()
    {
        _cookRepository.GenerateCooks();
    }

    public void TestConfiguration()
    {
        _cookRepository.TestConfiguration();
    }

    public async Task<Cook?> GetAvailableCook()
    {
        var cook = await _cookRepository.GetAvailableCook();
        if (cook != null)
        {
            return await Task.FromResult(cook);
        }

        return null;
    }

    public void MakeCookBusy(Cook cook)
    {
        var threads = cook.Proficiency;
        for (int i = 0; i < threads; i++)
        {
            Thread t = new Thread(() => PrepareFoodParallel(cook));
            t.Start();
        }

        cook.IsBusy = false;
    }

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    private async Task PrepareFoodParallel(Cook cook)
    {
        while (true)
        {
            var food = await _foodService.GetOptimalFoodToCook(cook.Proficiency);

            if (food != null)
            {
                PrintConsole.Write($"Cook {cook.Id} found food with id {food.Id} orderId: {food.OrderId} time {food.PreparationTime} foodStatus: {food.FoodStatusEnum}", ConsoleColor.DarkGreen);
                
                var cookedFood = await _apparatusService.PrepareFood(food);

                if (cookedFood)
                {
                    _foodService.ChangeFoodStatus(food, FoodStatusEnum.Cooked);
                    PrintConsole.Write($"Cook {cook.Id} prepared food {food.Id} orderId: {food.OrderId} foodStatus: {food.FoodStatusEnum}", ConsoleColor.Green);
                }
            }
            else
            {
                Thread.Sleep(1*Settings.Settings.TimeUnit);
            }
        }
    }
}
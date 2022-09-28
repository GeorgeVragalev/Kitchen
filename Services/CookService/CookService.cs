using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;
using Kitchen.Repositories.CookRepository;
using Kitchen.Services.FoodService;
using Kitchen.Services.OrderService;

namespace Kitchen.Services.CookService;

public class CookService : ICookService
{
    private readonly ICookRepository _cookRepository;
    private readonly IOrderService _orderService;
    private readonly IFoodService _foodService;

    public CookService(ICookRepository cookRepository, IOrderService orderService, IFoodService foodService)
    {
        _cookRepository = cookRepository;
        _orderService = orderService;
        _foodService = foodService;
    }

    public void GenerateCooks()
    {
        _cookRepository.GenerateCooks();
    }

    public void TestConfiguration()
    {
        _cookRepository.TestConfiguration();
    }

    public Cook GetAvailableCook()
    {
        var cook = _cookRepository.GetAvailableCook();
        if (cook != null)
        {
            return cook;
        }

        return null;
    }

    public void MakeCookBusy(Cook cook, IList<Food> foodsToCook)
    {
        if (foodsToCook != null && cook.MaxFoodsCanCook > 0)
        {
            var foodsCanCook = cook.MaxFoodsCanCook;

            cook.MaxFoodsCanCook = 0;

            var maxWait = foodsToCook.AsQueryable().OrderByDescending(f => f.PreparationTime)
                .Select(f => f.PreparationTime).FirstOrDefault().Apply();

            Thread.Sleep(maxWait * Settings.Settings.TimeUnit);

            foreach (var food in foodsToCook)
            {
                if (foodsCanCook > 0)
                {
                    //todo check that inceremennt works properly
                    _foodService.MarkFoodAsCooked(food);
                    // _orderService.IncrementPreparedFoodCounter(food.OrderId);
                    foodsCanCook--;
                }
            }

            cook.MaxFoodsCanCook += foodsToCook.Count;
            cook.IsBusy = false;
        }
    }
}
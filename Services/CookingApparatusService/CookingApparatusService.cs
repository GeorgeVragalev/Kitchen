using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.Enums;
using Kitchen.Repositories.CookingApparatusRepository;

namespace Kitchen.Services.CookingApparatusService;

public class CookingApparatusService : ICookingApparatusService
{
    private readonly ICookingApparatusRepository _apparatusRepository;
    private static Semaphore _semaphore = new Semaphore(1,1);
    public CookingApparatusService(ICookingApparatusRepository apparatusRepository)
    {
        _apparatusRepository = apparatusRepository;
    }

    public async Task<CookingApparatus?> GetApparatusByName(CookingApparatusEnum name)
    {
        return await _apparatusRepository.GetFreeApparatusByName(name);
    }

    public async Task<bool> PrepareFood(Food food)
    {
        CookingApparatus? apparatus = new CookingApparatus();
        switch (food.CookingApparatus)
        {
            case CookingApparatusEnum.Stove:
                apparatus = await _apparatusRepository.GetFreeApparatusByName(CookingApparatusEnum.Stove);
                break;
            case CookingApparatusEnum.Oven:
                apparatus = await _apparatusRepository.GetFreeApparatusByName(CookingApparatusEnum.Oven);
                break;
            case CookingApparatusEnum.None:
                food.PrepareFood(food.PreparationTime);
                return true;
            default:
                return false;
        }

        if (apparatus != null || !apparatus.IsBusy)
        {
            _semaphore.WaitOne();
            apparatus.IsBusy = true;
            PrintConsole.Write($"Cooking {food.Name} in {apparatus.Name} ID: {apparatus.Id} for order id {food.OrderId}", ConsoleColor.DarkMagenta);
            food.PrepareFood(food.PreparationTime);
            apparatus.IsBusy = false;
            PrintConsole.Write($"Apparatus with id {apparatus.Id} {apparatus.Name} is Released", ConsoleColor.Magenta);
            _semaphore.Release();
            return true;
        }

        PrintConsole.Write($"Apparatus with id {apparatus.Id} {apparatus.Name} is BUSY Status: {apparatus.IsBusy}", ConsoleColor.DarkMagenta);

        return false;
    }

    public void TestConfiguration()
    {
        _apparatusRepository.TestConfiguration();
    }
}
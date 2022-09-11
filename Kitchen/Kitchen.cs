using Kitchen.Repositories;
using Kitchen.Services.OrderService;

namespace Kitchen.Kitchen;

public class Kitchen : IKitchen
{
    private readonly IOrderListRepository _orderListRepository;
    private readonly IOrderService _orderService;

    public Kitchen(IOrderListRepository orderListRepository, IOrderService orderService)
    {
        _orderListRepository = orderListRepository;
        _orderService = orderService;
    }


    public void RunKitchen()
    {
        // _orderService.SendOrder(o);
    }
}
using Kitchen.Repositories;

namespace Kitchen.Kitchen;

public class Kitchen
{
    private readonly IOrderListRepository _orderListRepository;

    public Kitchen(IOrderListRepository orderListRepository)
    {
        _orderListRepository = orderListRepository;
    }
}
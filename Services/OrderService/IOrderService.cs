using Kitchen.Models;

namespace Kitchen.Services.OrderService;

public interface IOrderService
{
    public void SendOrder(Order order);
    public void PrepareOrder(Order order);
}
using Kitchen.Models;

namespace Kitchen.Kitchen;

public interface IKitchen
{
    public void RunKitchen(CancellationToken cancellationToken);
}
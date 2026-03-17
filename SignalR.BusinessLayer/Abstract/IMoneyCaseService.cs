using SignalR.EntitytLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IMoneyCaseService : IGenericService<MoneyCase>
    {
        decimal TTotalMoneyCaseAmout();
    }
}

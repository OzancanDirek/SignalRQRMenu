using SignalR.EntitytLayer.Entities;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IMoneyCaseDal :IGenericDal<MoneyCase>
    {
        public decimal TotalMoneyCaseAmout();
    }
}

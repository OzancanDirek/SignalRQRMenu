using SignalR.EntitytLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IMenuTableService : IGenericService<MenuTable>
    {
        public int TTableCount();

    }
}

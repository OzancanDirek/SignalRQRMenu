using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntitytLayer.Entities;

namespace SignalR.BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderdal;

        public OrderManager(IOrderDal orderdal)
        {
            _orderdal = orderdal;
        }

        public int TActiveOrderCount()
        {
            return _orderdal.ActiveOrderCount();
        }

        public void TAdd(Order entity)
        {
            _orderdal.Add(entity);
        }

        public void TDelete(Order entity)
        {
            _orderdal.Delete(entity);
        }

        public Order TGetById(int id)
        {
            return _orderdal.GetById(id);
        }

        public List<Order> TGetListAll()
        {
            return _orderdal.GetListAll();
        }

        public decimal TLastOrderPrice()
        {
            return _orderdal.LastOrderPrice();
        }

        public int TTotalOrderCount()
        {
            return _orderdal.TotalOrderCount();
        }

        public void TUpdate(Order entity)
        {
            _orderdal.Update(entity);
        }
    }
}

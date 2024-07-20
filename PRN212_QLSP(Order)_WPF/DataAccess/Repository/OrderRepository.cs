using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository, IBase
    {
        public void DeleteById(int id) => OrderDAO.Instance.DeleteById(id);

        public IEnumerable<Order> GetList() => OrderDAO.Instance.GetList();
        public void Add(Order order) => OrderDAO.Instance.Add(order);
        public void Update(Order order) => OrderDAO.Instance.Update(order);
    }
}

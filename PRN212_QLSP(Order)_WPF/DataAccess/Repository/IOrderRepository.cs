using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetList();
        void Add(Order order);
        void Update(Order order);
    }
}

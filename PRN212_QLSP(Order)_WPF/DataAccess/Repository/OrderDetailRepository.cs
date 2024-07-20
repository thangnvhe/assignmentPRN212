using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public IEnumerable<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetList();
    }
}

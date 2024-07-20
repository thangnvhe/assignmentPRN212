using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new OrderDetailDAO();
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetList()
        {
            List<OrderDetail> orderDetails;
            try
            {
                var db = new SaleManagmentContext();
                orderDetails = db.OrderDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orderDetails;
        }
    }
}

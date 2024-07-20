using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new OrderDAO();
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetList()
        {
            List<Order> orders;
            try
            {
                var db = new SaleManagmentContext();
                orders = db.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orders;
        }

        public void DeleteById(int Id)
        {
            try
            {
                var db = new SaleManagmentContext();
                Order order = new() { OrderId = Id };

                List<OrderDetail> lst = (from rows in db.OrderDetails where rows.OrderId == Id select rows).ToList();

                foreach (var item in lst)
                {
                    db.OrderDetails.Remove(item);
                }

                db.Orders.Remove(order);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Order order)
        {
            try
            {
                var db = new SaleManagmentContext();

                db.Members.Attach(order.Member);
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                var db = new SaleManagmentContext();

                db.Members.Attach(order.Member);
                db.Orders.Update(order);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

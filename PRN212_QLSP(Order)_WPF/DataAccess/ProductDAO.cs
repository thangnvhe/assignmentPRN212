using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new ProductDAO();
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetList()
        {
            List<Product> products;
            try
            {
                var db = new SaleManagmentContext();
                products = db.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return products;
        }

        public void DeleteById(int Id)
        {
            try
            {
                var db = new SaleManagmentContext();
                Product product = new() { ProductId = Id };
                db.Products.Remove(product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public void Add(Product product)
        {
            try
            {
                CategoryDAO.Instance.IsExisted(product.CategoryId);
                var db = new SaleManagmentContext();

                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                var db = new SaleManagmentContext();

                db.Products.Update(product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetById(int id)
        {
            try
            {
                var db = new SaleManagmentContext();

                return db.Products.First(m => m.ProductId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> GetByUnitPrice(decimal price)
        {
            try
            {
                var db = new SaleManagmentContext();

                return db.Products.Where(product => product.UnitPrice == price).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> GetByUnitInStock(int unit)
        {
            try
            {
                var db = new SaleManagmentContext();

                return db.Products.Where(product => product.UnitInStock == unit).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> GetByName(string name)
        {
            try
            {
                var db = new SaleManagmentContext();

                return db.Products.Where(product => product.ProductName.ToLower().Contains(name.ToLower())).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

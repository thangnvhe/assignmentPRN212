using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new CategoryDAO();
                    return instance;
                }
            }
        }

        public IEnumerable<Category> GetList()
        {
            List<Category> orders;
            try
            {
                var db = new SaleManagmentContext();
                orders = db.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orders;
        }

        public void IsExisted(int? id)
        {
            try
            {
                var db = new SaleManagmentContext();
                Category c = db.Categories.FirstOrDefault(x=>x.CategoryId==id);
                if(c == null)
                {
                    throw new Exception("Category did not already existed, try again");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

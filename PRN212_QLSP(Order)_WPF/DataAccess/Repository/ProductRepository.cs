using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository, IBase
    {
        public void DeleteById(int id) => ProductDAO.Instance.DeleteById(id);

        public IEnumerable<Product> GetList() => ProductDAO.Instance.GetList();
        public void Update(Product product) => ProductDAO.Instance.Update(product);
        public void Add(Product product) => ProductDAO.Instance.Add(product);
        public Product GetById(int id) => ProductDAO.Instance.GetById(id);
        public IEnumerable<Product> GetByName(string name) => ProductDAO.Instance.GetByName(name);
        public IEnumerable<Product> GetByUnitPrice(decimal price) => ProductDAO.Instance.GetByUnitPrice(price);
        public IEnumerable<Product> GetByUnitInStock(int unit) => ProductDAO.Instance.GetByUnitInStock(unit);
    }
}

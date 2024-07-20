using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetList();
        void Update(Product product);
        void Add(Product product);
        Product GetById(int id);
        IEnumerable<Product> GetByName(string name);
        IEnumerable<Product> GetByUnitPrice(decimal price);
        IEnumerable<Product> GetByUnitInStock(int unit);

    }
}

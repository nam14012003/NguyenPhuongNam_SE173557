using ConvenienceStore_BusinessObject;
using ConvenienceStore_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_Repository
{
    public class ProductRepo : IProductRepo
    {
        public bool AddNewProduct(Product product) => ProductDAO.Instance.AddNewProduct(product);

        public bool DeleteProductProfile(int productId) => ProductDAO.Instance.DeleteProductProfile(productId);

        public List<Product> GetAllProduct() => ProductDAO.Instance.GetAllProduct();

        public Product GetProductProfileById(int id) => ProductDAO.Instance.GetProductProfileById(id);

        public List<Product> SearchProducts(string productName, string description) => ProductDAO.Instance.SearchProducts(productName, description);

        public bool UpdateProductProfile(Product product) => ProductDAO.Instance.UpdateProductProfile(product);
    }
}

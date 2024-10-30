using ConvenienceStore_BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_Repository
{
    public interface IProductRepo
    {
        public List<Product> GetAllProduct();

        public Product GetProductProfileById(int id);

        public bool AddNewProduct(Product product);

        public bool UpdateProductProfile(Product product);

        public bool DeleteProductProfile(int productId);

        public List<Product> SearchProducts(string productName, string description);
    }
}

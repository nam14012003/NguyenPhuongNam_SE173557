using ConvenienceStore_BusinessObject;
using ConvenienceStoreDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_DAO
{
    public class ProductDAO
    {
        private ConvenienceStoreDbContext dbContext;
        private static ProductDAO instance = null;

        public ProductDAO()
        {
            dbContext = new ConvenienceStoreDbContext();
        }

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
        }

        public List<Product> GetAllProduct()
        {
            return dbContext.Products
                .Include(p => p.Vendor) 
                .ToList();
        }
        public Product GetProductProfileById(int id)
        {
            return dbContext.Products.SingleOrDefault(m => m.ProductId.Equals(id));
        }


        public bool AddNewProduct(Product product)
        {
            bool isSuccess = false;
            Product productProdile = this.GetProductProfileById(product.ProductId);
            try
            {
                if (productProdile == null)
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }

            }
            catch (Exception e)
            {
                throw new Exception();
            }
            return isSuccess;
        }




        public bool UpdateProductProfile(Product product)
        {
            bool isSuccess = false;
            var existingProduct = dbContext.Products.Local.FirstOrDefault(p => p.ProductId == product.ProductId);
            try
            {
                if (existingProduct != null)
                {
                    dbContext.Products.Update(product);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating candidate profile: " + ex.Message);
            }
            return isSuccess;
        }


        public bool DeleteProductProfile(int productId)
        {
            bool isSuccess = false;
            Product productProfile = this.GetProductProfileById(productId);
            try
            {
                if (productId != null)
                {
                    dbContext.Products.Remove(productProfile);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            return isSuccess;
        }

        public List<Product> SearchProducts(string productName, string description)
        {
            if (string.IsNullOrWhiteSpace(productName) && string.IsNullOrWhiteSpace(description))
            {
                return new List<Product>();
            }
            return dbContext.Products
                .Where(p =>
                    (string.IsNullOrEmpty(productName) || p.ProductName.ToLower().Contains(productName.ToLower())) &&
                    (string.IsNullOrEmpty(description) || p.Description.ToLower().Contains(description.ToLower())))
                .ToList();
        }


    }
}



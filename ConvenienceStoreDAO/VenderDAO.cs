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
    public class VenderDAO
    {
        private ConvenienceStoreDbContext dbContext;
        private static VenderDAO instance = null;

        public VenderDAO()
        {
            dbContext = new ConvenienceStoreDbContext();
        }

        public static VenderDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VenderDAO();
                }
                return instance;
            }
        }
        public Vendor GetVendorById(string id)
        {
            return dbContext.Vendors.SingleOrDefault(m => m.VendorId.Equals(id));
        }

        public List<Vendor> GetVendor()
        {
            return dbContext.Vendors.ToList();
        }
    }
}

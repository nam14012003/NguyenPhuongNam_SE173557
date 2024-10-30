using ConvenienceStore_BusinessObject;
using ConvenienceStore_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_Repository
{
    public class VendorRepo : IVendorRepo
    {
        public List<Vendor> GetVendor() => VenderDAO.Instance.GetVendor();

        public Vendor GetVendorById(string id) => VenderDAO.Instance.GetVendorById(id);
    }
}

using ConvenienceStore_BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_Repository
{
    public interface IVendorRepo
    {
        public Vendor GetVendorById(string id);


        public List<Vendor> GetVendor();

    }
}

using ConvenienceStore_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_Repository
{
    public interface IStoreAccountRepo
    {
        public StoreAccount GetStoreAccount(string email, string password);
    }
}

using ConvenienceStore_BusinessObject;
using ConvenienceStore_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_Repository
{
    public class StoreAccountRepo : IStoreAccountRepo
    {
        public StoreAccount GetStoreAccount(string email, string password) => StoreAccountDAO.Instance.GetStoreAccount(email, password);
    }
}

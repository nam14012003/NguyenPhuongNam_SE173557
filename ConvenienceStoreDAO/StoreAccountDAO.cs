using ConvenienceStore_BusinessObject;
using ConvenienceStoreDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenienceStore_DAO
{
    public class StoreAccountDAO
    {
        private ConvenienceStoreDbContext dbcontext;

        private static StoreAccountDAO instance = null;

        public static StoreAccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StoreAccountDAO();
                }
                return instance;
            }
        }
        public StoreAccountDAO()
        {
            dbcontext = new ConvenienceStoreDbContext();
        }

        public StoreAccount GetStoreAccount(string email, string password)
        {
            return dbcontext.StoreAccounts.SingleOrDefault(m => m.Email.Equals(email) && m.Password.Equals(password));
        }
    }
}

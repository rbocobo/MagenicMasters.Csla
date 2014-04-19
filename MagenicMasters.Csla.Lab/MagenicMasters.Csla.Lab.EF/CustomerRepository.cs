using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.Csla.Lab.DataAccess;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.EF;

namespace MagenicMasters.CslaLab.EF
{
    public class CustomerRepository : ICustomerRepository
    {
        private IMagenicMastersContext context = MMContext.context;

        public CslaLab.DataAccess.DataContracts.ICustomerData GetCustomer(int id)
        {
            return context.Customers.Find(id);
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }
}

using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.RepositoryContracts
{
    public interface ICustomerRepository : IRepository
    {
        ICustomerData GetCustomer(int id);
    }
}

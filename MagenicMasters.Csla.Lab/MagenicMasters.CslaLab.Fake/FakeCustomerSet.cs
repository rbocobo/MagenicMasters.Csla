using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Fake
{
    public class FakeCustomerSet : FakeDbSet<ICustomerData>
    {
        public override ICustomerData Find(params object[] keyValues)
        {
            return this.SingleOrDefault(_ => _.Id == (int)keyValues.Single());
        }
    }
}

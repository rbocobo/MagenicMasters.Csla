using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.CslaLab.Fake
{
    public class FakeCustomerSet : FakeDbSet<Customer>
    {
        public FakeCustomerSet()
        {
            this.Add(new Customer() { Id=1, Name="Edmure Tully"});
            this.Add(new Customer() { Id=2, Name="Margaery Tyrell"});
            this.Add(new Customer() { Id = 3, Name = "Theon Greyjoy" });
        }
        public override Customer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(_ => _.Id == (int)keyValues.Single());
        }
    }
}

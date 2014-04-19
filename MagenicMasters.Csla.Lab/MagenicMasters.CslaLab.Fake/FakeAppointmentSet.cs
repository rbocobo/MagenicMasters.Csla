using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.CslaLab.Fake
{
    public class FakeAppointmentSet : FakeDbSet<Appointment>
    {
        public override Appointment Find(params object[] keyValues)
        {
            return this.SingleOrDefault(_ => _.Id == (int)keyValues.Single());
        }
    }
}

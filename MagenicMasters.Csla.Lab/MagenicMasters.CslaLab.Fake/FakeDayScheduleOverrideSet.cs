using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.CslaLab.Fake
{
    public class FakeDayScheduleOverrideSet : FakeDbSet<DayScheduleOverride>
    {
        public override DayScheduleOverride Find(params object[] keyValues)
        {
            return this.Single(_ => _.Id == (int) keyValues.Single());
        }
    }
}

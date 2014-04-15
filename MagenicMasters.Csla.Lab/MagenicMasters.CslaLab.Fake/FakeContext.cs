using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
namespace MagenicMasters.CslaLab.Fake
{
    public class FakeContext :  IMagenicMastersContext
    {

        public FakeContext()
        {

        }

        public IDbSet<IAppointmentData> Appointments { get; set; }
        public IDbSet<ICustomerData> Customers { get; set; }
        public IDbSet<IDayScheduleOverrideData> DayScheduleOverrides { get; set; }
        public IDbSet<IDesignerData> Designers { get; set; }
        public IDbSet<IDesignerSpecialtyData> DesignerSpecialties { get; set; }
        public IDbSet<ISpecialtyData> Specialties { get; set; }
        public IDbSet<IWeekScheduleData> WeekSchedules { get; set; }
    }
}

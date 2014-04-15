using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess
{
    public interface IMagenicMastersContext
    {

        IDbSet<IAppointmentData> Appointments { get; set; }
        IDbSet<ICustomerData> Customers { get; set; }
        IDbSet<IDayScheduleOverrideData> DayScheduleOverrides { get; set; }
        IDbSet<IDesignerData> Designers { get; set; }
        IDbSet<IDesignerSpecialtyData> DesignerSpecialties { get; set; }
        IDbSet<ISpecialtyData> Specialties { get; set; }
        IDbSet<IWeekScheduleData> WeekSchedules { get; set; }
    }
}

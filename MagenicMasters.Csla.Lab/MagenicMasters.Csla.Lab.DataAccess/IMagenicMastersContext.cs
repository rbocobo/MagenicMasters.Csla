using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.CslaLab.DataAccess
{
    public interface IMagenicMastersContext
    {
        IDbSet<Appointment> Appointments { get; set; }
        IDbSet<Customer> Customers { get; set; }
        IDbSet<DayScheduleOverride> DayScheduleOverrides { get; set; }
        IDbSet<Designer> Designers { get; set; }
        IDbSet<DesignerSpecialty> DesignerSpecialties { get; set; }
        IDbSet<Specialty> Specialties { get; set; }
        IDbSet<WeekSchedule> WeekSchedules { get; set; }
        IDbSet<DesignerRate> DesignerRates { get; set; }
        IDbSet<Cancellation> Cancellations { get; set; }

        int SaveChanges();
    }
}

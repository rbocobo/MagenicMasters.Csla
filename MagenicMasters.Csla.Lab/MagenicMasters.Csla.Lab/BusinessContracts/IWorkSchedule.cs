using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.BusinessContracts
{
    public interface IWorkSchedule : IBusinessBase
    {
         int Id { get; set; }
         DateTime StartDate { get; set; }
         int MaxHours { get; set; }
         int AppointmentInterval { get; set; }
         string WorkingDays { get; set; }
         DateTime StartTime { get; set; }
         DateTime EndTime { get; set; }

    }
}

using Csla;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts
{
    public interface IWorkSchedule : IBusinessBase
    {
         int Id { get; set; }
         int DesignerId { get; set; }
         DateTime StartDate { get; set; }
         int MaxHours { get; set; }
         int AppointmentInterval { get; set; }
         string WorkingDays { get; set; }
         DateTime StartTime { get; set; }
         DateTime EndTime { get; set; }
         IScheduleRepository ScheduleRepository { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface IDayScheduleOverrideData
    {
        int Id { get; set; }
        int WeekScheduleId { get; set; }
        DateTime Date { get; set; }
        DateTime EndTime { get; set; }
        DateTime StartTime { get; set; }
        //IWeekScheduleData WeekSchedule { get; set; }
    }
}

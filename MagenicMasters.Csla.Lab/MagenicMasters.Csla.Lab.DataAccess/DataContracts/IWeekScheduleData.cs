using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface IWeekScheduleData
    {
        int Id { get; set; }
        int DesignerId { get; set; }
        DateTime StartDate { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        int IntervalsInMinutes { get; set; }
        int MaxHours { get; set; }
        bool IncludeHolidays { get; set; }
    }
}

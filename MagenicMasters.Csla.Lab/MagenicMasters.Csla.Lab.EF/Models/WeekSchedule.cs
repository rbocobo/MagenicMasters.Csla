using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.Csla.Lab.EF.Models
{
    public partial class WeekSchedule : IWeekScheduleData
    {
        public WeekSchedule()
        {
            this.DayScheduleOverrides = new List<DayScheduleOverride>();
        }

        public int Id { get; set; }
        public int DesignerId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public int IntervalsInMinutes { get; set; }
        public int MaxHours { get; set; }
        public bool IncludeHolidays { get; set; }
        public virtual ICollection<DayScheduleOverride> DayScheduleOverrides { get; set; }
        public virtual Designer Designer { get; set; }
    }
}

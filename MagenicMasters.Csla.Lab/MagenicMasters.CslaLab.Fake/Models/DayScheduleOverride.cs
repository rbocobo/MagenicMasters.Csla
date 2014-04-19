using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.CslaLab.Fake.Models
{
    public partial class DayScheduleOverride : IDayScheduleOverrideData
    {
        public int Id { get; set; }
        public int WeekScheduleId { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime EndTime { get; set; }
        public System.DateTime StartTime { get; set; }
        public  WeekSchedule WeekSchedule { get; set; }
    }
}

using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.CslaLab.Fake.Models
{
    public partial class Designer : IDesignerData
    {
        public Designer()
        {
            this.Appointments = new List<Appointment>();
            this.DesignerSpecialties = new List<DesignerSpecialty>();
            this.WeekSchedules = new List<WeekSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFull { get; set; }
        public  ICollection<Appointment> Appointments { get; set; }
        public  ICollection<DesignerSpecialty> DesignerSpecialties { get; set; }
        public  ICollection<WeekSchedule> WeekSchedules { get; set; }
        public  ICollection<DesignerRate> DesignerRates { get; set; }
    }
}

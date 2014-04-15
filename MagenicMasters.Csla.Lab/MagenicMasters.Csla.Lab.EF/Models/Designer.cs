using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.Csla.Lab.EF.Models
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
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<DesignerSpecialty> DesignerSpecialties { get; set; }
        public virtual ICollection<WeekSchedule> WeekSchedules { get; set; }
    }
}

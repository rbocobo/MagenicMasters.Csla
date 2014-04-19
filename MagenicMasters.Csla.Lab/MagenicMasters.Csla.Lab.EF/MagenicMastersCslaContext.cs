using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MagenicMasters.Csla.Lab.EF.Models.Mapping;
using MagenicMasters.Csla.Lab.EF.Models;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MagenicMasters.CslaLab.EF
{
    public partial class MagenicMastersCslaContext : DbContext, IMagenicMastersContext
    {
        static MagenicMastersCslaContext()
        {
            Database.SetInitializer<MagenicMastersCslaContext>(null);
        }

        public MagenicMastersCslaContext()
            : base("Name=MagenicMastersCslaContext")
        {
        }

        public IDbSet<Appointment> Appointments { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<DayScheduleOverride> DayScheduleOverrides { get; set; }
        public IDbSet<Designer> Designers { get; set; }
        public IDbSet<DesignerSpecialty> DesignerSpecialties { get; set; }
        public IDbSet<Specialty> Specialties { get; set; }
        public IDbSet<WeekSchedule> WeekSchedules { get; set; }
        public IDbSet<DesignerRate> DesignerRates { get; set; }
        public IDbSet<Cancellation> Cancellations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppointmentMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DayScheduleOverrideMap());
            modelBuilder.Configurations.Add(new DesignerMap());
            modelBuilder.Configurations.Add(new DesignerSpecialtyMap());
            modelBuilder.Configurations.Add(new SpecialtyMap());
            modelBuilder.Configurations.Add(new WeekScheduleMap());
            modelBuilder.Configurations.Add(new DesignerRateMap());
            modelBuilder.Configurations.Add(new CancellationMap());
        }
    }
}

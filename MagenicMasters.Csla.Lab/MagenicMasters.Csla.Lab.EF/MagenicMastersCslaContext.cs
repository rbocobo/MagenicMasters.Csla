using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MagenicMasters.Csla.Lab.EF.Models.Mapping;
using MagenicMasters.Csla.Lab.EF.Models;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.DataContracts;

namespace MagenicMasters.CslaLab.EF
{
    public partial class MagenicMastersCslaContext : DbContext
    {
        static MagenicMastersCslaContext()
        {
            Database.SetInitializer<MagenicMastersCslaContext>(null);
        }

        public MagenicMastersCslaContext()
            : base("Name=MagenicMastersCslaContext")
        {
        }

        public DbSet<IAppointmentData> Appointments { get; set; }
        public DbSet<ICustomerData> Customers { get; set; }
        public DbSet<IDayScheduleOverrideData> DayScheduleOverrides { get; set; }
        public DbSet<IDesignerData> Designers { get; set; }
        public DbSet<IDesignerSpecialtyData> DesignerSpecialties { get; set; }
        public DbSet<ISpecialtyData> Specialties { get; set; }
        public DbSet<IWeekScheduleData> WeekSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppointmentMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DayScheduleOverrideMap());
            modelBuilder.Configurations.Add(new DesignerMap());
            modelBuilder.Configurations.Add(new DesignerSpecialtyMap());
            modelBuilder.Configurations.Add(new SpecialtyMap());
            modelBuilder.Configurations.Add(new WeekScheduleMap());
        }
    }
}

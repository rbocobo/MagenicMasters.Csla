using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.Models;
namespace MagenicMasters.CslaLab.Fake
{
    public class FakeContext :  IMagenicMastersContext
    {

        public FakeContext()
        {
            Customers = new FakeCustomerSet();

            Designers = new FakeDesignerSet();


            Specialties = new FakeDbSet<Specialty>()
            {
                new Specialty(){ Id=1, Name="Kitchen Design" },
                new Specialty(){ Id=2, Name="Bathroom Design" },
                new Specialty(){ Id=3, Name="Bedroom Design" },
                new Specialty(){ Id=4, Name="Landscape Design" },
                new Specialty(){ Id=5, Name="Living Area Design" },
                new Specialty(){ Id=6, Name="Pool and pool area design" },
                new Specialty(){ Id=7, Name="Home Architecture" }
            };
            DesignerRates = new FakeDbSet<DesignerRate>()
            {
                new DesignerRate() { Id=1, DesignerId=1, Rate=300},
                new DesignerRate() { Id=2, DesignerId=2, Rate=400},
                new DesignerRate() { Id=3, DesignerId=3, Rate=340},
                new DesignerRate() { Id=4, DesignerId=4, Rate=360}
            };

            DesignerSpecialties = new FakeDbSet<DesignerSpecialty>()
            {
                new DesignerSpecialty(){ Id=1, DesignerId=1, SpecialtyId=1},
                new DesignerSpecialty(){ Id=2, DesignerId=2, SpecialtyId=1},
                new DesignerSpecialty(){ Id=3, DesignerId=3, SpecialtyId=1},
                new DesignerSpecialty(){ Id=4, DesignerId=4, SpecialtyId=1},
                new DesignerSpecialty(){ Id=5, DesignerId=1, SpecialtyId=2},
                new DesignerSpecialty(){ Id=6, DesignerId=2, SpecialtyId=2},
                new DesignerSpecialty(){ Id=7, DesignerId=3, SpecialtyId=2},
                new DesignerSpecialty(){ Id=8, DesignerId=4, SpecialtyId=2},
                new DesignerSpecialty(){ Id=9, DesignerId=1, SpecialtyId=3},
                new DesignerSpecialty(){ Id=10, DesignerId=2, SpecialtyId=3},
                new DesignerSpecialty(){ Id=11, DesignerId=3, SpecialtyId=3},
                new DesignerSpecialty(){ Id=12, DesignerId=4, SpecialtyId=3}
            };
            Cancellations = new FakeDbSet<Cancellation>() { 
                new Cancellation(){Id=1, Fee=100, Window=5}
            };

            WeekSchedules = new FakeWeekScheduleSet();
            Appointments = new FakeAppointmentSet();
            DayScheduleOverrides = new FakeDayScheduleOverrideSet();

        }
        public IDbSet<Appointment> Appointments
        { get; set; }

        public IDbSet<Customer> Customers
        { get; set; }

        public IDbSet<DayScheduleOverride> DayScheduleOverrides
        { get; set; }

        public IDbSet<Designer> Designers
        { get; set; }

        public IDbSet<DesignerSpecialty> DesignerSpecialties
        { get; set; }

        public IDbSet<Specialty> Specialties
        { get; set; }

        public IDbSet<WeekSchedule> WeekSchedules
        { get; set; }

        public IDbSet<DesignerRate> DesignerRates
        { get; set; }

        public IDbSet<Cancellation> Cancellations
        { get; set; }

        public  int SaveChanges()
        {
            
            foreach (var item in WeekSchedules.Local)
            {
                var i = item as WeekSchedule;
                i.Id = 1;
            }

            foreach (var item in Appointments.Local)
            {
                var i = item as Appointment;
                i.Id = 1;
            }

            foreach (var item in DayScheduleOverrides.Local)
            {
                var i = item as DayScheduleOverride;
                i.Id = 1;
            }
            return 1;
        }
    }
}

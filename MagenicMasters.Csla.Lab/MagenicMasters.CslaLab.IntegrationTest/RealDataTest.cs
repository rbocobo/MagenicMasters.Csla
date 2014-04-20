using System;
using Autofac;
using Csla.Server;
using MagenicMasters.CslaLab.Contracts.Designer;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.Designer;
using MagenicMasters.CslaLab.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spackle;
using CS = Csla.Server;
using C = Csla;
using MagenicMasters.CslaLab.Criteria;
using MagenicMasters.CslaLab.Customer;
using MagenicMasters.CslaLab.Contracts.Customer;
using System.Linq;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.Csla.Lab.DataAccess;
using MagenicMasters.CslaLab.Common;
namespace MaagenicMasters.CslaLab.IntegrationTest
{
    [TestClass]
    public class RealDataTest
    {
        ContainerBuilder builder;
        RandomObjectGenerator generator;
        public RealDataTest()
        {
            generator = new RandomObjectGenerator();

        }
        [TestInitialize]
        public void TestInitializer()
        {
            MagenicMastersCslaContext context = new MagenicMastersCslaContext();

            foreach (var item in (context.Appointments.Select(_ => _)))
            {
                context.Appointments.Remove(item);
            }
            foreach (var item in (context.WeekSchedules.Select(_ => _)))
            {
                context.WeekSchedules.Remove(item);
            }
            foreach (var item in (context.DayScheduleOverrides.Select(_ => _)))
            {
                context.DayScheduleOverrides.Remove(item);
            }
            context.SaveChanges();
        }

        [TestMethod]
        public void CancelAppointmentInCancelWindowPassed()
        {
            //arrange
            this.builder = new ContainerBuilder();
            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();
            builder.RegisterType<AppointmentRequest>().As<IAppointmentRequest>();
            builder.RegisterType<RequestAppointmentCommand>().As<IRequestAppointmentCommand>();
            builder.RegisterType<AppointmentResultView>().As<IAppointmentResultView>();
            builder.RegisterType<MagenicMasters.CslaLab.Customer.AppointmentView>().As<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentView>();
            builder.RegisterType<TimeEntry>().As<ITimeEntry>();
            builder.RegisterType<TimeEntries>().As<ITimeEntries>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = DateTime.Now.AddDays(3).Date;
            workSchedule.StartTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 09:00 AM");
            workSchedule.EndTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 01:00 PM");
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortal.Update(workSchedule);

            var arPortal = IoC.Container.Resolve<IObjectPortal<IAppointmentRequest>>();
            var tePortal = IoC.Container.Resolve<IObjectPortal<ITimeEntries>>();
            //var tecPortal = IoC.Container.Resolve<IChildObjectPortal>();

            var aReq = arPortal.Create();
            var timeEntries = tePortal.Create();
            var timeEntry = timeEntries.ChildObjectPortal.CreateChild<ITimeEntry>(); //tecPortal.CreateChild<ITimeEntry>();
            timeEntry.StartDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 10:00 AM");
            timeEntry.EndDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 11:00 AM");

            timeEntries.Add(timeEntry);
            aReq.CustomerId = 1;
            aReq.SpecialtyId = 1;
            aReq.TimeEntries = timeEntries;
            

            var rcPortal = IoC.Container.Resolve<IObjectPortal<IRequestAppointmentCommand>>();
            var cmd = rcPortal.Create(aReq);
            var result = rcPortal.Execute(cmd);
            var appt = result.AppointmentRequestResult;

            var appointmentIdToCancel = appt.Id;
            var cPortal = IoC.Container.Resolve<IObjectPortal<ICancelAppointment>>();
            var cancelCmd = cPortal.Create(appointmentIdToCancel);
            cancelCmd = cPortal.Execute(cancelCmd);

            //assert

            Assert.IsNotNull(appt);
            Assert.AreEqual(appt.StartDateTime, timeEntry.StartDateTime);
            Assert.AreEqual(appt.Fee, 300);
            Assert.AreEqual(appt.DesignerName, "Ned Stark");

            Assert.AreEqual(cancelCmd.Charges, 100);
    
        }

        [TestMethod]
        public void CancelAppointmentOutsideCancelWindowPassed()
        {
            //arrange
            this.builder = new ContainerBuilder();
            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();
            builder.RegisterType<AppointmentRequest>().As<IAppointmentRequest>();
            builder.RegisterType<RequestAppointmentCommand>().As<IRequestAppointmentCommand>();
            builder.RegisterType<AppointmentResultView>().As<IAppointmentResultView>();
            builder.RegisterType<MagenicMasters.CslaLab.Customer.AppointmentView>().As<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentView>();
            builder.RegisterType<TimeEntry>().As<ITimeEntry>();
            builder.RegisterType<TimeEntries>().As<ITimeEntries>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = DateTime.Now.AddDays(8).Date;
            workSchedule.StartTime = DateTime.Parse(DateTime.Now.AddDays(8).Date.ToShortDateString() + " 09:00 AM");
            workSchedule.EndTime = DateTime.Parse(DateTime.Now.AddDays(8).Date.ToShortDateString() + " 01:00 PM");
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortal.Update(workSchedule);

            var arPortal = IoC.Container.Resolve<IObjectPortal<IAppointmentRequest>>();
            var tePortal = IoC.Container.Resolve<IObjectPortal<ITimeEntries>>();
            //var tecPortal = IoC.Container.Resolve<IChildObjectPortal>();

            var aReq = arPortal.Create();
            var timeEntries = tePortal.Create();
            var timeEntry = timeEntries.ChildObjectPortal.CreateChild<ITimeEntry>(); //tecPortal.CreateChild<ITimeEntry>();
            timeEntry.StartDateTime = DateTime.Parse(DateTime.Now.AddDays(8).ToShortDateString() + " 10:00 AM");
            timeEntry.EndDateTime = DateTime.Parse(DateTime.Now.AddDays(8).ToShortDateString() + " 11:00 AM");

            timeEntries.Add(timeEntry);
            aReq.CustomerId = 1;
            aReq.SpecialtyId = 1;
            aReq.TimeEntries = timeEntries;


            var rcPortal = IoC.Container.Resolve<IObjectPortal<IRequestAppointmentCommand>>();
            var cmd = rcPortal.Create(aReq);
            var result = rcPortal.Execute(cmd);
            var appt = result.AppointmentRequestResult;

            var appointmentIdToCancel = appt.Id;
            var cPortal = IoC.Container.Resolve<IObjectPortal<ICancelAppointment>>();
            var cancelCmd = cPortal.Create(appointmentIdToCancel);
            cancelCmd = cPortal.Execute(cancelCmd);

            //assert

            Assert.IsNotNull(appt);
            Assert.AreEqual(appt.StartDateTime, timeEntry.StartDateTime);
            Assert.AreEqual(appt.Fee, 300);
            Assert.AreEqual(appt.DesignerName, "Ned Stark");

            Assert.AreEqual(cancelCmd.Charges, 50);

        }

        [TestMethod]
        public void BuildRequestAppointmentPassed()
        {
            //arrange
            this.builder = new ContainerBuilder();
            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();
            builder.RegisterType<AppointmentRequest>().As<IAppointmentRequest>();
            builder.RegisterType<RequestAppointmentCommand>().As<IRequestAppointmentCommand>();
            builder.RegisterType<AppointmentResultView>().As<IAppointmentResultView>();
            builder.RegisterType<MagenicMasters.CslaLab.Customer.AppointmentView>().As<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentView>();
            builder.RegisterType<TimeEntry>().As<ITimeEntry>();
            builder.RegisterType<TimeEntries>().As<ITimeEntries>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = DateTime.Now.AddDays(3).Date;
            workSchedule.StartTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 09:00 AM");
            workSchedule.EndTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 01:00 PM");
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortal.Update(workSchedule);

            var arPortal = IoC.Container.Resolve<IObjectPortal<IAppointmentRequest>>();
            var tePortal = IoC.Container.Resolve<IObjectPortal<ITimeEntries>>();
            //var tecPortal = IoC.Container.Resolve<IChildObjectPortal>();

            var aReq = arPortal.Create();
            var timeEntries = tePortal.Create();
            var timeEntry = timeEntries.ChildObjectPortal.CreateChild<ITimeEntry>(); //tecPortal.CreateChild<ITimeEntry>();
            timeEntry.StartDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 10:00 AM");
            timeEntry.EndDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 11:00 AM");

            timeEntries.Add(timeEntry);
            aReq.CustomerId = 1;
            aReq.SpecialtyId = 1;
            aReq.TimeEntries = timeEntries;


            var rcPortal = IoC.Container.Resolve<IObjectPortal<IRequestAppointmentCommand>>();
            var cmd = rcPortal.Create(aReq);
            var result = rcPortal.Execute(cmd);
            var appt = result.AppointmentRequestResult;
            //assert

            Assert.IsNotNull(appt);
            Assert.AreEqual(appt.StartDateTime, timeEntry.StartDateTime);
            Assert.AreEqual(appt.Fee, 300);
            Assert.AreEqual(appt.DesignerName, "Ned Stark");

        }

        [TestMethod]
        public void CreateWeekSchedulePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();
            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            
            //assert
            Assert.IsNotNull(workSchedule);
        }

        [TestMethod]
        public void AddWeekSchedulePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();
            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = DateTime.Now.AddDays(4).Date;
            workSchedule.StartTime = DateTime.Parse(DateTime.Now.AddDays(4).Date.ToShortDateString() + " 09:00 AM");
            workSchedule.EndTime = DateTime.Parse(DateTime.Now.AddDays(4).Date.ToShortDateString() + " 12:00 PM");
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortal.Update(workSchedule);
            objectPortal.Delete(workSchedule.Id);
            //assert
            Assert.IsTrue(workSchedule.Id > 0);
            
        }

        [TestMethod]
        public void UpdateWeekSchedulePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();
            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            var startDate = DateTime.Now.AddDays(5).Date;
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 2;
            workSchedule.StartDate = startDate;
            workSchedule.StartTime = DateTime.Parse(startDate.ToShortDateString() + " 09:00 AM");
            workSchedule.EndTime = DateTime.Parse(startDate.Date.ToShortDateString() + " 10:00 AM");
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortal.Update(workSchedule);

            var wsId = workSchedule.Id;

            workSchedule = objectPortal.Fetch(new GetWeekScheduleCriteria(2, startDate));
            workSchedule.DesignerId = 3;
            workSchedule = objectPortal.Update(workSchedule);

            //assert
            Assert.IsTrue(workSchedule.DesignerId != 2);
        }

        [TestMethod]
        public void CreateDayScheduleOverridePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();
            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            var dsOverride = objectPortal.Create();

            //assert
            Assert.IsNotNull(dsOverride);
        }

        [TestMethod]
        public void AddDayScheduleOverridePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            var expectedWSDate = DateTime.Now.AddDays(3).Date;
            var expectedWSDateStartTime = DateTime.Parse(expectedWSDate.ToShortDateString() + " 9:00 AM");
            var expectedWSDateEndTime = expectedWSDateStartTime.AddHours(1);


            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();
            //act

            var objectPortalWS = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortalWS.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = expectedWSDate;
            workSchedule.StartTime = expectedWSDateStartTime;
            workSchedule.EndTime = expectedWSDateEndTime;
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortalWS.Update(workSchedule);


            var objectPortalDSO = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            var dsOverride = objectPortalDSO.Create();
            dsOverride.WeekScheduleId = workSchedule.Id;
            dsOverride.Date = expectedWSDate;
            dsOverride.StartTime = expectedWSDateStartTime;
            dsOverride.EndTime = expectedWSDateEndTime;
            dsOverride  = objectPortalDSO.Update(dsOverride);

            //assert
            Assert.IsNotNull(dsOverride);
            Assert.IsTrue(dsOverride.Id > 0);
        }

        [TestMethod]
        public void GetDayScheduleOverridePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            var expectedWSDate = DateTime.Now.AddDays(3).Date;
            var expectedWSDateStartTime = DateTime.Parse(expectedWSDate.ToShortDateString() + " 9:00 AM");
            var expectedWSDateEndTime = expectedWSDateStartTime.AddHours(1);


            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();
            //act

            var objectPortalWS = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortalWS.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = expectedWSDate;
            workSchedule.StartTime = expectedWSDateStartTime;
            workSchedule.EndTime = expectedWSDateEndTime;
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortalWS.Update(workSchedule);


            var objectPortalDSO = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            var dsOverride = objectPortalDSO.Create();
            dsOverride.WeekScheduleId = workSchedule.Id;
            dsOverride.Date = expectedWSDate;
            dsOverride.StartTime = expectedWSDateStartTime;
            dsOverride.EndTime = expectedWSDateEndTime;
            dsOverride = objectPortalDSO.Update(dsOverride);

            var desId = 1;

            var editDSOVerride = objectPortalDSO.Fetch(new GetDayScheduleOverrideCriteria(desId, expectedWSDate));


            //assert
            Assert.IsNotNull(editDSOVerride);
            Assert.AreEqual(editDSOVerride.Date, expectedWSDate);
            Assert.AreEqual(editDSOVerride.StartTime, expectedWSDateStartTime);
            Assert.AreEqual(editDSOVerride.EndTime, expectedWSDateEndTime);
        }

        [TestMethod]
        public void UpdateDayScheduleOverridePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            var expectedWSDate = DateTime.Now.AddDays(3).Date;
            var expectedWSDateStartTime = DateTime.Parse(expectedWSDate.ToShortDateString() + " 9:00 AM");
            var expectedWSDateEndTime = expectedWSDateStartTime.AddHours(1);


            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();
            //act

            var objectPortalWS = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortalWS.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = expectedWSDate;
            workSchedule.StartTime = expectedWSDateStartTime;
            workSchedule.EndTime = expectedWSDateEndTime;
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortalWS.Update(workSchedule);


            var objectPortalDSO = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            var dsOverride = objectPortalDSO.Create();
            dsOverride.WeekScheduleId = workSchedule.Id;
            dsOverride.Date = expectedWSDate;
            dsOverride.StartTime = expectedWSDateStartTime;
            dsOverride.EndTime = expectedWSDateEndTime;
            dsOverride = objectPortalDSO.Update(dsOverride);

            var desId = 1;

            var editDSOVerride = objectPortalDSO.Fetch(new GetDayScheduleOverrideCriteria(desId, expectedWSDate));
            var editDate = expectedWSDate.AddDays(1);
            var editStartTime = expectedWSDateStartTime.AddDays(1);
            var editEndTime = expectedWSDateEndTime.AddDays(1);

            editDSOVerride.Date = editDate;
            editDSOVerride.StartTime = editStartTime;
            editDSOVerride.EndTime = editEndTime;
            editDSOVerride = objectPortalDSO.Update(editDSOVerride);

            //assert
            Assert.IsNotNull(editDSOVerride);
            Assert.AreEqual(editDSOVerride.Date, editDate);
            Assert.AreEqual(editDSOVerride.StartTime, editStartTime);
            Assert.AreEqual(editDSOVerride.EndTime, editEndTime);
        }

        [TestMethod]
        public void GetDesignerActiveAppointmentsPassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();
            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();
            builder.RegisterType<AppointmentRequest>().As<IAppointmentRequest>();
            builder.RegisterType<RequestAppointmentCommand>().As<IRequestAppointmentCommand>();
            builder.RegisterType<AppointmentResultView>().As<IAppointmentResultView>();
            builder.RegisterType<MagenicMasters.CslaLab.Designer.AppointmentView>().As<MagenicMasters.CslaLab.Contracts.Designer.IAppointmentView>();
            builder.RegisterType<MagenicMasters.CslaLab.Designer.AppointmentViewCollection>().As<MagenicMasters.CslaLab.Contracts.Designer.IAppointmentViewCollection>();
            builder.RegisterType<TimeEntry>().As<ITimeEntry>();
            builder.RegisterType<TimeEntries>().As<ITimeEntries>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = DateTime.Now.AddDays(3).Date;
            workSchedule.StartTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 09:00 AM");
            workSchedule.EndTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 01:00 PM");
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortal.Update(workSchedule);

            var arPortal = IoC.Container.Resolve<IObjectPortal<IAppointmentRequest>>();
            var tePortal = IoC.Container.Resolve<IObjectPortal<ITimeEntries>>();
            //var tecPortal = IoC.Container.Resolve<IChildObjectPortal>();

            var aReq = arPortal.Create();
            var timeEntries = tePortal.Create();
            var timeEntry = timeEntries.ChildObjectPortal.CreateChild<ITimeEntry>(); //tecPortal.CreateChild<ITimeEntry>();
            timeEntry.StartDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 10:00 AM");
            timeEntry.EndDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 11:00 AM");

            timeEntries.Add(timeEntry);
            aReq.CustomerId = 1;
            aReq.SpecialtyId = 1;
            aReq.TimeEntries = timeEntries;


            var rcPortal = IoC.Container.Resolve<IObjectPortal<IRequestAppointmentCommand>>();
            var cmd = rcPortal.Create(aReq);
            var result = rcPortal.Execute(cmd);

            var daPortal = IoC.Container.Resolve<IObjectPortal<MagenicMasters.CslaLab.Contracts.Designer.IAppointmentViewCollection>>();
            var desApptViewColl = daPortal.Fetch(1);
            //assert

            Assert.IsNotNull(desApptViewColl);
            Assert.IsTrue(desApptViewColl.Count() > 0);

        }

        [TestMethod]
        public void GetCustomerActiveAppointmentsPassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();
            new RealDataTestBuilderComposition().Compose(builder);

            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();
            builder.RegisterType<AppointmentRequest>().As<IAppointmentRequest>();
            builder.RegisterType<RequestAppointmentCommand>().As<IRequestAppointmentCommand>();
            builder.RegisterType<AppointmentResultView>().As<IAppointmentResultView>();
            builder.RegisterType<MagenicMasters.CslaLab.Customer.AppointmentView>().As<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentView>();
            builder.RegisterType<MagenicMasters.CslaLab.Customer.AppointmentViewCollection>().As<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentViewCollection>();
            builder.RegisterType<TimeEntry>().As<ITimeEntry>();
            builder.RegisterType<TimeEntries>().As<ITimeEntries>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            MMContext.context = IoC.Container.Resolve<IMagenicMastersContext>();

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            workSchedule.AppointmentInterval = 3;
            workSchedule.DesignerId = 1;
            workSchedule.StartDate = DateTime.Now.AddDays(3).Date;
            workSchedule.StartTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 09:00 AM");
            workSchedule.EndTime = DateTime.Parse(DateTime.Now.AddDays(3).Date.ToShortDateString() + " 01:00 PM");
            workSchedule.WorkingDays = "M";
            workSchedule = objectPortal.Update(workSchedule);

            var arPortal = IoC.Container.Resolve<IObjectPortal<IAppointmentRequest>>();
            var tePortal = IoC.Container.Resolve<IObjectPortal<ITimeEntries>>();
            //var tecPortal = IoC.Container.Resolve<IChildObjectPortal>();

            var aReq = arPortal.Create();
            var timeEntries = tePortal.Create();
            var timeEntry = timeEntries.ChildObjectPortal.CreateChild<ITimeEntry>(); //tecPortal.CreateChild<ITimeEntry>();
            timeEntry.StartDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 10:00 AM");
            timeEntry.EndDateTime = DateTime.Parse(DateTime.Now.AddDays(3).ToShortDateString() + " 11:00 AM");

            timeEntries.Add(timeEntry);
            aReq.CustomerId = 1;
            aReq.SpecialtyId = 1;
            aReq.TimeEntries = timeEntries;


            var rcPortal = IoC.Container.Resolve<IObjectPortal<IRequestAppointmentCommand>>();
            var cmd = rcPortal.Create(aReq);
            var result = rcPortal.Execute(cmd);

            var caPortal = IoC.Container.Resolve<IObjectPortal<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentViewCollection>>();
            var custApptViewColl = caPortal.Fetch(1);
            //assert

            Assert.IsNotNull(custApptViewColl);
            Assert.IsTrue(custApptViewColl.Count() > 0);

        }
    }

    public class RealDataTestBuilderComposition : IContainerBuilderComposition
    {

        public void Compose(ContainerBuilder builder)
        {
            builder.RegisterType<ObjectActivator>().As<IDataPortalActivator>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ObjectPortal<>))
                .As(typeof(IObjectPortal<>))
                .InstancePerLifetimeScope();
            builder.RegisterType<ChildObjectPortal>()
                .As<IChildObjectPortal>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppointmentRepository>().As<IAppointmentRepository>();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<DesignerRepository>().As<IDesignerRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<MagenicMastersCslaContext>().As<IMagenicMastersContext>();
        }
    }

}

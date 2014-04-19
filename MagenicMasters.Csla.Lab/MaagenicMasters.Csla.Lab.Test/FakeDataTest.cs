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
using MagenicMasters.CslaLab.Fake;
using MagenicMasters.CslaLab.DataAccess.Models;

namespace MaagenicMasters.Csla.Lab.Test
{
    [TestClass]
    public class FakeDataTest
    {
        ContainerBuilder builder;
        RandomObjectGenerator generator;
        public FakeDataTest()
        {
            generator = new RandomObjectGenerator();

        }

        [TestInitialize]
        public void TestInitializer()
        {
           
        }

        [TestMethod]
        public void BuildRequestAppointmentPassedFake()
        {
            //arrange
            this.builder = new ContainerBuilder();
            new FakeDataTestBuilderComposition().Compose(builder);

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
            aReq.SpecialtyId = 2;
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
        public void AddWeekSchedulePassedFake()
        {
            //arrange
            this.builder = new ContainerBuilder();

            new FakeDataTestBuilderComposition().Compose(builder);

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
        public void UpdateWeekSchedulePassedFake()
        {
            //arrange
            this.builder = new ContainerBuilder();

            new FakeDataTestBuilderComposition().Compose(builder);

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
    }

    public class FakeDataTestBuilderComposition : IContainerBuilderComposition
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
            builder.RegisterType<FakeContext>().As<IMagenicMastersContext>();
        }
    }
}

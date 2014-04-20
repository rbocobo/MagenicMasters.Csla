using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using CS = Csla.Server;
using C = Csla;
using MagenicMasters.CslaLab.Common;
using MagenicMasters.CslaLab.Customer;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Designer;
using Moq;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.DataAccess;
using System.Linq;
using Csla.Server;
using MagenicMasters.CslaLab.Contracts.Customer;
using MagenicMasters.CslaLab.Contracts.Designer;
using Spackle;
using MagenicMasters.CslaLab.Criteria;

namespace MaagenicMasters.Csla.Lab.Test
{
    /// <summary>
    /// Summary description for MockDataTest
    /// </summary>
    [TestClass]
    public class MockDataTest
    {
        private static IContainer Container { get; set; }
        ContainerBuilder builder;
        RandomObjectGenerator generator;
        public MockDataTest()
        {
            //
            // TODO: Add constructor logic here
            //
            generator = new RandomObjectGenerator();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {


        }

        #endregion

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(ObjectPortal<>))
                .As(typeof(IObjectPortal<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ChildObjectPortal>()
                .As<IChildObjectPortal>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppointmentRequest>()
                .As<IAppointmentRequest>()
                .InstancePerLifetimeScope();

            return builder.Build();
        }

        [TestMethod]
        public void TestBuildAppointment()
        {
            //arrange

            builder = new ContainerBuilder();

            var appointmentData = new Mock<IAppointmentData>(MockBehavior.Strict);
            var appointmentDate = DateTime.Now.AddDays(3);
            appointmentData.SetupAllProperties();
            appointmentData.SetupGet(_ => _.DateTime).Returns(appointmentDate);
            appointmentData.SetupGet(_ => _.Id).Returns(1);

            var designerName = "Designer Name";
            var designerData = new Mock<IDesignerData>(MockBehavior.Strict);
            designerData.SetupAllProperties();
            designerData.SetupGet(_ => _.Id).Returns(1);
            designerData.SetupGet(_ => _.IsFull).Returns(true);
            designerData.SetupGet(_ => _.Name).Returns(designerName);

            var repository = new Mock<IAppointmentRepository>(MockBehavior.Strict);
            repository.SetupAllProperties();
            repository.Setup(_ => _.BuildAppointment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<List<DateTimeRange>>()))
                      .Returns(appointmentData.Object);
            repository.Setup(_ => _.Dispose()).Callback(() => { });
            

            var desRepository = new Mock<IDesignerRepository>(MockBehavior.Strict);
            desRepository.SetupAllProperties();
            desRepository.Setup(_ => _.GetDesigner(It.IsAny<int>()))
                        .Returns(designerData.Object);
            desRepository.Setup(_ => _.Dispose()).Callback(()=>{});


            new MockTestBuilderComposition().Compose(builder);

            builder.RegisterType<AppointmentRequest>().As<IAppointmentRequest>().InstancePerLifetimeScope();
            builder.RegisterType<RequestAppointmentCommand>().As<IRequestAppointmentCommand>().InstancePerLifetimeScope();
            builder.RegisterType<AppointmentResultView>().As<IAppointmentResultView>().InstancePerLifetimeScope();
            builder.RegisterInstance(repository.Object).As<IAppointmentRepository>();
            builder.RegisterInstance(desRepository.Object).As<IDesignerRepository>();
            

            IoC.Container = builder.Build();

            var activator = IoC.Container.Resolve<IDataPortalActivator>();


            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            var aR = IoC.Container.Resolve<IObjectPortal<IAppointmentRequest>>();
            var rC = IoC.Container.Resolve<IObjectPortal<IRequestAppointmentCommand>>();


            var appRequest = aR.Create();  //C.DataPortal.Create<AppointmentRequest>();
            var cmd = rC.Create(appRequest); //C.DataPortal.Create<RequestAppoinmentCommand>(appRequest);


            var d = rC.Execute(cmd);//C.DataPortal.Execute(cmd);

            Assert.AreEqual(designerName, d.AppointmentRequestResult.DesignerName);
            Assert.AreEqual(appointmentDate, d.AppointmentRequestResult.StartDateTime);
        }

        [TestMethod]
        public void TestCancelAppointment()
        {
            //arrange
            builder = new ContainerBuilder();

            decimal charges = 15.0M;

            var apptRepository = new Mock<IAppointmentRepository>(MockBehavior.Strict);
            apptRepository.Setup(_ => _.CancelAppointment(It.IsAny<int>()))
                          .Returns(charges);


            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterType<CancelAppointment>().As<ICancelAppointment>();
            builder.RegisterInstance(apptRepository.Object).As<IAppointmentRepository>();
            IoC.Container = builder.Build();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<ICancelAppointment>>();
            var command = objectPortal.Create(1);  // C.DataPortal.Create<CancelAppointment>(1);
            var result = objectPortal.Execute(command); //C.DataPortal.Execute(command);

            //assert
            Assert.AreEqual(charges, result.Charges);
        }

        [TestMethod]
        public void TestGetDesignerActiveAppointments()
        {
            //arrange
            builder = new ContainerBuilder();

            List<IAppointmentData> appointmentList = new List<IAppointmentData>();
            for(int i=0;i<3;i++)
            {
                var appointmentData = new Mock<IAppointmentData>(MockBehavior.Strict);
                appointmentData.SetupAllProperties();
                appointmentData.SetupGet(_ => _.Id).Returns(i + 1);
                appointmentData.SetupGet(_ => _.DateTime).Returns(generator.Generate<DateTime>());
                appointmentList.Add(appointmentData.Object);
            }

            var custName = generator.Generate<string>();
            var custData = new Mock<ICustomerData>();
            custData.SetupAllProperties();
            custData.SetupGet(_ => _.Name).Returns(custName);

            
            var custRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
            custRepository.Setup(_ => _.GetCustomer(It.IsAny<int>()))
                .Returns(custData.Object);

            var apptRepository = new Mock<IAppointmentRepository>(MockBehavior.Strict);
            apptRepository.Setup(_ => _.GetDesignerActiveAppointments(It.IsAny<int>()))
                .Returns(appointmentList);

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(custRepository.Object).As<ICustomerRepository>();
            builder.RegisterInstance(apptRepository.Object).As<IAppointmentRepository>();
            builder.RegisterType<MagenicMasters.CslaLab.Designer.AppointmentViewCollection>().As<MagenicMasters.CslaLab.Contracts.Designer.IAppointmentViewCollection>();

            IoC.Container = builder.Build();

            var activator = IoC.Container.Resolve<IDataPortalActivator>();

            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();


            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<MagenicMasters.CslaLab.Contracts.Designer.IAppointmentViewCollection>>();
            var collectionObject = objectPortal.Fetch(1); //C.DataPortal.Fetch<MagenicMasters.CslaLab.Designer.AppointmentViewCollection>(1);

            //assert

            for (int i = 0; i < collectionObject.Count;i++ )
            {
                Assert.AreEqual(collectionObject[i].CustomerName, custName);
                Assert.AreEqual(collectionObject[i].Date, appointmentList[i].DateTime);
            }

            custRepository.VerifyAll();
            apptRepository.VerifyAll();
        }

        [TestMethod]
        public void TestGetCustomerActiveAppointments()
        {
            //arrange
            builder = new ContainerBuilder();

            List<IAppointmentData> appointmentList = new List<IAppointmentData>();
            for (int i = 0; i < 3; i++)
            {
                var appointmentData = new Mock<IAppointmentData>(MockBehavior.Strict);
                appointmentData.SetupAllProperties();
                appointmentData.SetupGet(_ => _.Id).Returns(i + 1);
                appointmentData.SetupGet(_ => _.Fee).Returns(generator.Generate<decimal>());
                appointmentList.Add(appointmentData.Object);
            }

            var designerName = generator.Generate<string>();
            var designerId = generator.Generate<int>();
            var designerData = new Mock<IDesignerData>(MockBehavior.Strict);
            designerData.SetupAllProperties();
            designerData.SetupGet(_ => _.Id).Returns(designerId);
            designerData.SetupGet(_ => _.IsFull).Returns(true);
            designerData.SetupGet(_ => _.Name).Returns(designerName);

            var apptRepository = new Mock<IAppointmentRepository>(MockBehavior.Strict);
            apptRepository.Setup(_ => _.GetCustomerActiveAppointments(It.IsAny<int>()))
                .Returns(appointmentList);

            var desRepository = new Mock<IDesignerRepository>(MockBehavior.Strict);
            desRepository.SetupAllProperties();
            desRepository.Setup(_ => _.GetDesigner(It.IsAny<int>()))
                        .Returns(designerData.Object);
            

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(apptRepository.Object).As<IAppointmentRepository>();
            builder.RegisterInstance(desRepository.Object).As<IDesignerRepository>();
            builder.RegisterType<MagenicMasters.CslaLab.Customer.AppointmentViewCollection>().As<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentViewCollection>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            //act

            var objectPortal = IoC.Container.Resolve<IObjectPortal<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentViewCollection>>();
            var appointmentListResult = objectPortal.Fetch(1);
            //assert
            for (int i = 0 ; i < appointmentList.Count; i++)
            {
                Assert.AreEqual(appointmentList[i].Fee, appointmentListResult[i].Fee);
                Assert.AreEqual(designerName, appointmentListResult[i].DesignerName);
            }
            apptRepository.VerifyAll();
            desRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateWeekSchedulePassed()
        {
            //arrange
            this.builder = new ContainerBuilder();

            var designerId = generator.Generate<int>();
            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(designerId);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.CreateWeekSchedule())
                .Returns(weekScheduleData.Object).Verifiable();
            scheduleRepository.Setup(_ => _.AddWeekSchedule(It.IsAny<IWeekScheduleData>()));
            scheduleRepository.Setup(_ => _.SaveChanges());


            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            //act

            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            objectPortal.Update(workSchedule);

            //assert

            scheduleRepository.VerifyAll();
        }

        [TestMethod]
        public void AddWeekSchedulePassed()
        {
            //arrange
            this.builder = new ContainerBuilder();

            List<IWeekScheduleData> weekScheduleList = new List<IWeekScheduleData>();

            var designerId = generator.Generate<int>();
            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(designerId);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.CreateWeekSchedule())
                .Returns(weekScheduleData.Object).Verifiable();
            scheduleRepository.Setup(_ => _.AddWeekSchedule(It.IsAny<IWeekScheduleData>()))
                .Callback((IWeekScheduleData data) => {
                    weekScheduleList.Add(data);
                }).Verifiable();
            scheduleRepository.Setup(_ => _.SaveChanges());


            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            //act

            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            objectPortal.Update(workSchedule);

            //assert
            Assert.IsTrue(weekScheduleList.Contains(weekScheduleData.Object));
            scheduleRepository.VerifyAll();
        }

        [TestMethod]
        public void AddWeekSchedulePersistedPassed()
        {
            //arrange
            this.builder = new ContainerBuilder();

            List<IWeekScheduleData> weekScheduleList = new List<IWeekScheduleData>();
            List<IWeekScheduleData> weekScheduleTable = new List<IWeekScheduleData>();

            var designerId = generator.Generate<int>();
            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(designerId);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.CreateWeekSchedule())
                .Returns(weekScheduleData.Object).Verifiable();
            scheduleRepository.Setup(_ => _.AddWeekSchedule(It.IsAny<IWeekScheduleData>()))
                .Callback((IWeekScheduleData data) =>
                {
                    weekScheduleList.Add(data);
                }).Verifiable();
            scheduleRepository.Setup(_ => _.SaveChanges())
                .Callback(() => {

                    foreach (var item in weekScheduleList.ToArray())
                    {
                        weekScheduleTable.Add(item);
                    }
                    weekScheduleList.Clear();
                });


            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            //act

            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            objectPortal.Update(workSchedule);

            //assert
            Assert.IsTrue(weekScheduleList.Count == 0);
            Assert.IsTrue(weekScheduleTable.Contains(weekScheduleData.Object));
            scheduleRepository.VerifyAll();
        }

        [TestMethod]
        public void GetWeekSchedulePassed()
        {
            //arrange
            this.builder = new ContainerBuilder();

            List<IWeekScheduleData> weekScheduleTable = new List<IWeekScheduleData>();
            var designerId = generator.Generate<int>();
            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(designerId);
            weekScheduleTable.Add(weekScheduleData.Object);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);

            scheduleRepository.Setup(_ => _.GetWeekSchedule(It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(weekScheduleTable.First());

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            //act

            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Fetch(new GetWeekScheduleCriteria(designerId, DateTime.Now));

            //assert

            Assert.AreEqual(workSchedule.DesignerId, designerId);
            scheduleRepository.VerifyAll();


        }

        [TestMethod]
        public void UpdateWeekSchedulePassed()
        {
            //arrange
            this.builder = new ContainerBuilder();
            List<IWeekScheduleData> weekScheduleChangeSet = new List<IWeekScheduleData>();
            List<IWeekScheduleData> weekScheduleTable = new List<IWeekScheduleData>();

            var id = generator.Generate<int>();
            var designerId = generator.Generate<int>();
            var intervalInMins = generator.Generate<int>();
            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.Id).Returns(id);
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(designerId);
            weekScheduleData.SetupGet(_ => _.IntervalsInMinutes).Returns(intervalInMins);
            weekScheduleTable.Add(weekScheduleData.Object);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.CreateWeekSchedule())
                .Returns(new Mock<IWeekScheduleData>().SetupAllProperties().Object).Verifiable();
            scheduleRepository.Setup(_ => _.GetWeekSchedule(It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(weekScheduleTable.First());
            scheduleRepository.Setup(_ => _.UpdateWeekSchedule(It.IsAny<IWeekScheduleData>()))
                .Callback((IWeekScheduleData data) => {
                    weekScheduleChangeSet.Add(data);
                });
            scheduleRepository.Setup(_ => _.SaveChanges())
                .Callback(() => {
                    foreach (var item in weekScheduleChangeSet)
                    {
                        var oldItem = weekScheduleTable.Where(_ => _.Id == item.Id).Single();
                        weekScheduleTable.Remove(oldItem);
                        oldItem.IntervalsInMinutes = item.IntervalsInMinutes;
                        weekScheduleTable.Add(oldItem);
                        
                        //oldItem.IntervalsInMinutes = item.IntervalsInMinutes;
                    }
                    weekScheduleChangeSet.Clear();
                });

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            //act

            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Fetch(new GetWeekScheduleCriteria(designerId, DateTime.Now));
            workSchedule.AppointmentInterval = generator.Generate<int>();
            objectPortal.Update(workSchedule);


            //assert

            Assert.AreEqual(workSchedule.DesignerId, designerId);
            scheduleRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateDayScheduleOverridePassed()
        {
            //arrange

            builder = new ContainerBuilder();


            var changeSet = new List<IDayScheduleOverrideData>();
            var savedSet = new List<IDayScheduleOverrideData>();
            var expectedDate = DateTime.Now.AddDays(3).Date;
            var expectedDateStartTime = DateTime.Parse(expectedDate.ToShortDateString() + " 9:00 AM");
            var expectedDateEndTime = expectedDateStartTime.AddHours(1);
            var expectedDesId = 1;
            var expectedWSId = 1;
            var schedOverrideData = new Mock<IDayScheduleOverrideData>(MockBehavior.Strict);
            schedOverrideData.SetupAllProperties();
            
            //schedOverrideData.SetupGet(_ => _.Date).Returns(expectedDate);
            //schedOverrideData.SetupGet(_ => _.StartTime).Returns(expectedDateStartTime);
            //schedOverrideData.SetupGet(_ => _.EndTime).Returns(expectedDateEndTime);
            //schedOverrideData.SetupGet(_ => _.WeekScheduleId).Returns(1);

            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(expectedDesId);
            weekScheduleData.SetupGet(_ => _.Id).Returns(expectedWSId);
            weekScheduleData.SetupGet(_ => _.StartDate).Returns(expectedDate);
            weekScheduleData.SetupGet(_ => _.StartTime).Returns(expectedDateStartTime);
            weekScheduleData.SetupGet(_ => _.EndTime).Returns(expectedDateEndTime);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.AddDayScheduleOverride(It.IsAny<IDayScheduleOverrideData>()))
                .Callback((IDayScheduleOverrideData data) => {
                    changeSet.Add(data);
                });
            scheduleRepository.Setup(_ => _.CreateDayScheduleOverride())
                .Returns(schedOverrideData.Object);
            scheduleRepository.Setup(_ => _.SaveChanges())
                .Callback(() => {
                    var id = savedSet.Count();
                    foreach (var item in changeSet)
                    {
                        item.Id = ++id;
                        savedSet.Add(item);
                    }
                    changeSet.Clear();
                });

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<DayScheduleOverride>().As<IDayScheduleOverride>();
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            
            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            
            var dayScheduleOverride = objectPortal.Create();
            
            //assert
            Assert.IsNotNull(dayScheduleOverride);
        }

        [TestMethod]
        public void AddDayScheduleOverridePassed()
        {
            //arrange

            builder = new ContainerBuilder();


            var changeSet = new List<IDayScheduleOverrideData>();
            var savedSet = new List<IDayScheduleOverrideData>();
            var expectedDate = DateTime.Now.AddDays(3).Date;
            var expectedDateStartTime = DateTime.Parse(expectedDate.ToShortDateString() + " 9:00 AM");
            var expectedDateEndTime = expectedDateStartTime.AddHours(1);
            var expectedDesId = 1;
            var expectedWSId = 1;
            var schedOverrideData = new Mock<IDayScheduleOverrideData>(MockBehavior.Strict);
            schedOverrideData.SetupAllProperties();

            //schedOverrideData.SetupGet(_ => _.Date).Returns(expectedDate);
            //schedOverrideData.SetupGet(_ => _.StartTime).Returns(expectedDateStartTime);
            //schedOverrideData.SetupGet(_ => _.EndTime).Returns(expectedDateEndTime);
            //schedOverrideData.SetupGet(_ => _.WeekScheduleId).Returns(1);

            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(expectedDesId);
            weekScheduleData.SetupGet(_ => _.Id).Returns(expectedWSId);
            weekScheduleData.SetupGet(_ => _.StartDate).Returns(expectedDate);
            weekScheduleData.SetupGet(_ => _.StartTime).Returns(expectedDateStartTime);
            weekScheduleData.SetupGet(_ => _.EndTime).Returns(expectedDateEndTime);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.AddDayScheduleOverride(It.IsAny<IDayScheduleOverrideData>()))
                .Callback((IDayScheduleOverrideData data) =>
                {
                    changeSet.Add(data);
                });
            scheduleRepository.Setup(_ => _.CreateDayScheduleOverride())
                .Returns(schedOverrideData.Object);
            scheduleRepository.Setup(_ => _.SaveChanges())
                .Callback(() =>
                {
                    var id = savedSet.Count();
                    foreach (var item in changeSet)
                    {
                        item.Id = ++id;
                        savedSet.Add(item);
                    }
                    changeSet.Clear();
                });

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<DayScheduleOverride>().As<IDayScheduleOverride>();
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            var dayScheduleOverride = objectPortal.Create();
            dayScheduleOverride.WeekScheduleId = expectedWSId;
            dayScheduleOverride.Date = expectedDate;
            dayScheduleOverride.StartTime  =expectedDateStartTime;
            dayScheduleOverride.EndTime = expectedDateEndTime;

            dayScheduleOverride = objectPortal.Update(dayScheduleOverride);
            //assert
            Assert.IsNotNull(dayScheduleOverride);
            Assert.IsTrue(dayScheduleOverride.Id > 0);
            
        }

        [TestMethod]
        public void GetDayScheduleOverridePassed()
        {
            //arrange

            builder = new ContainerBuilder();



            var savedSet = new List<IDayScheduleOverrideData>();
            var expectedDate = DateTime.Now.AddDays(3).Date;
            var expectedDateStartTime = DateTime.Parse(expectedDate.ToShortDateString() + " 9:00 AM");
            var expectedDateEndTime = expectedDateStartTime.AddHours(1);
            var expectedDesId = 1;
            var expectedWSId = 1;

            var schedOverrideData = new Mock<IDayScheduleOverrideData>(MockBehavior.Strict);
            schedOverrideData.SetupAllProperties();
            schedOverrideData.SetupGet(_ => _.Id).Returns(1);
            schedOverrideData.SetupGet(_ => _.Date).Returns(expectedDate);
            schedOverrideData.SetupGet(_ => _.StartTime).Returns(expectedDateStartTime);
            schedOverrideData.SetupGet(_ => _.EndTime).Returns(expectedDateEndTime);
            schedOverrideData.SetupGet(_ => _.WeekScheduleId).Returns(1);

            savedSet.Add(schedOverrideData.Object);

            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(expectedDesId);
            weekScheduleData.SetupGet(_ => _.Id).Returns(expectedWSId);
            weekScheduleData.SetupGet(_ => _.StartDate).Returns(expectedDate);
            weekScheduleData.SetupGet(_ => _.StartTime).Returns(expectedDateStartTime);
            weekScheduleData.SetupGet(_ => _.EndTime).Returns(expectedDateEndTime);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.GetDayScheduleOverride(It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(savedSet.FirstOrDefault());

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<DayScheduleOverride>().As<IDayScheduleOverride>();
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });
            
            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            var schedOvrd = objectPortal.Fetch(new GetDayScheduleOverrideCriteria(1, DateTime.Now.AddDays(3)));

            //Assert
            Assert.IsNotNull(schedOvrd);
            Assert.AreEqual(schedOvrd.Id,1);
            Assert.AreEqual(schedOvrd.WeekScheduleId, expectedWSId);
            Assert.AreEqual(schedOvrd.Date, expectedDate);
            Assert.AreEqual(schedOvrd.StartTime, expectedDateStartTime);
            Assert.AreEqual(schedOvrd.EndTime, expectedDateEndTime);
        }

        [TestMethod]
        public void UpdateDayScheduleOverridePassed()
        {
            //arrange

            builder = new ContainerBuilder();


            var changeSet = new List<IDayScheduleOverrideData>();
            var savedSet = new List<IDayScheduleOverrideData>();

            var oldDate = DateTime.Now.AddDays(4).Date;
            var oldStartTime = DateTime.Parse(oldDate.ToShortDateString() + " 9:00 AM");
            var oldEndTime = oldStartTime.AddHours(1);
            var oldDesId = 2;
            var oldWSId = 2;

            var expectedDate = DateTime.Now.AddDays(3).Date;
            var expectedDateStartTime = DateTime.Parse(expectedDate.ToShortDateString() + " 9:00 AM");
            var expectedDateEndTime = expectedDateStartTime.AddHours(1);
            var expectedDesId = 1;
            var expectedWSId = 1;

            var newData = new Mock<IDayScheduleOverrideData>(MockBehavior.Strict);
            newData.SetupAllProperties();

            var schedOverrideData = new Mock<IDayScheduleOverrideData>(MockBehavior.Strict);
            schedOverrideData.SetupAllProperties();
            schedOverrideData.SetupGet(_ => _.Id).Returns(1);
            schedOverrideData.SetupGet(_ => _.Date).Returns(oldDate);
            schedOverrideData.SetupGet(_ => _.StartTime).Returns(oldStartTime);
            schedOverrideData.SetupGet(_ => _.EndTime).Returns(oldEndTime);
            schedOverrideData.SetupGet(_ => _.WeekScheduleId).Returns(oldWSId);

            savedSet.Add(schedOverrideData.Object);

            var weekScheduleData = new Mock<IWeekScheduleData>(MockBehavior.Strict);
            weekScheduleData.SetupAllProperties();
            weekScheduleData.SetupGet(_ => _.DesignerId).Returns(expectedDesId);
            weekScheduleData.SetupGet(_ => _.Id).Returns(expectedWSId);
            weekScheduleData.SetupGet(_ => _.StartDate).Returns(expectedDate);
            weekScheduleData.SetupGet(_ => _.StartTime).Returns(expectedDateStartTime);
            weekScheduleData.SetupGet(_ => _.EndTime).Returns(expectedDateEndTime);

            var scheduleRepository = new Mock<IScheduleRepository>(MockBehavior.Strict);
            scheduleRepository.Setup(_ => _.CreateDayScheduleOverride())
                .Returns(newData.Object);
            scheduleRepository.Setup(_ => _.GetDayScheduleOverride(It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(savedSet.FirstOrDefault());
            scheduleRepository.Setup(_ => _.UpdateDayScheduleOverride(It.IsAny<IDayScheduleOverrideData>()))
                .Callback((IDayScheduleOverrideData data) => {
                    changeSet.Add(data);
                });
            
            scheduleRepository.Setup(_ => _.SaveChanges())
                .Callback(() =>
                {
                    foreach (var item in changeSet)
                    {
                        var s = savedSet.Single(_ => _.Id == item.Id);
                        savedSet.Remove(s);
                        savedSet.Add(item);
                        
                    }
                    changeSet.Clear();
                });



            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();
            builder.RegisterType<DayScheduleOverride>().As<IDayScheduleOverride>();
            builder.RegisterInstance(scheduleRepository.Object).As<IScheduleRepository>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.User = new System.Security.Principal.GenericPrincipal(C.ApplicationContext.User.Identity, new string[] { UserRole.Designers });

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IDayScheduleOverride>>();
            var schedOvrd = objectPortal.Fetch(new GetDayScheduleOverrideCriteria(1, DateTime.Now.AddDays(3)));
            schedOvrd.Date = expectedDate;
            schedOvrd.StartTime = expectedDateStartTime;
            schedOvrd.EndTime = expectedDateEndTime;
            schedOvrd.WeekScheduleId = expectedWSId;
            objectPortal.Update(schedOvrd);
            //Assert
            Assert.IsNotNull(schedOvrd);
            Assert.AreEqual(savedSet.Single().Id, 1);
            Assert.AreEqual(savedSet.Single().WeekScheduleId, expectedWSId);
            Assert.AreEqual(savedSet.Single().Date, expectedDate);
            Assert.AreEqual(savedSet.Single().StartTime, expectedDateStartTime);
            Assert.AreEqual(savedSet.Single().EndTime, expectedDateEndTime);
        }

        [TestMethod]
        public void GetDesignerActiveAppointmentsPassed()
        {
            //arrange

            builder = new ContainerBuilder();

            var eCancelWindow = generator.Generate<int>();
            var eCustomerId = generator.Generate<int>();
            var eDateTime = generator.Generate<DateTime>();
            var eDesigner = generator.Generate<int>();
            var eFee = generator.Generate<decimal>();
            var eId = generator.Generate<int>();
            var eSpcId = generator.Generate<int>();

            List<IAppointmentData> lstAppts = new List<IAppointmentData>();
            var appointmentData = new Mock<IAppointmentData>(MockBehavior.Strict);
            appointmentData.SetupAllProperties();
            appointmentData.SetupGet(_ => _.CancelWindow).Returns(eCancelWindow);
            appointmentData.SetupGet(_ => _.CustomerId).Returns(eCustomerId);
            appointmentData.SetupGet(_ => _.DateTime).Returns(eDateTime); ;
            appointmentData.SetupGet(_ => _.DesignerId).Returns(eDesigner);
            appointmentData.SetupGet(_ => _.Fee).Returns(eFee);
            appointmentData.SetupGet(_ => _.Id).Returns(eId);
            appointmentData.SetupGet(_ => _.SpecialtyId).Returns(eSpcId);
            lstAppts.Add(appointmentData.Object);

            var custData = new Mock<ICustomerData>(MockBehavior.Strict);
            custData.SetupAllProperties();
            custData.SetupGet(_ => _.Id).Returns(generator.Generate<int>());
            custData.SetupGet(_ => _.Name).Returns(generator.Generate<string>());

            var apptRepository = new Mock<IAppointmentRepository>(MockBehavior.Strict);
            apptRepository.Setup(_ => _.GetDesignerActiveAppointments(It.IsAny<int>()))
                .Returns(lstAppts.AsEnumerable());
            var custRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
            custRepository.Setup(_ => _.GetCustomer(It.IsAny<int>())).Returns(custData.Object);

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(apptRepository.Object).As<IAppointmentRepository>();
            builder.RegisterInstance(custRepository.Object).As<ICustomerRepository>();
            builder.RegisterType<MagenicMasters.CslaLab.Designer.AppointmentViewCollection>().As<MagenicMasters.CslaLab.Contracts.Designer.IAppointmentViewCollection>();
            

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            //act
            var objPortal = IoC.Container.Resolve<IObjectPortal<MagenicMasters.CslaLab.Contracts.Designer.IAppointmentViewCollection>>();
            var apptViewColl = objPortal.Fetch(generator.Generate<int>());

            //assert
            Assert.IsNotNull(apptViewColl);
            Assert.AreEqual(apptViewColl.First().Date, eDateTime);
            //Assert.AreEqual(apptViewColl.First().Specialty 
            Assert.AreEqual(apptViewColl.First().CustomerName, custData.Object.Name);

        }

        [TestMethod]
        public void GetCustomerActiveAppointmentsPassed()
        {
            //arrange

            builder = new ContainerBuilder();

            var eCancelWindow = generator.Generate<int>();
            var eCustomerId = generator.Generate<int>();
            var eDateTime = generator.Generate<DateTime>();
            var eDesigner = generator.Generate<int>();
            var eFee = generator.Generate<decimal>();
            var eId = generator.Generate<int>();
            var eSpcId = generator.Generate<int>();

            List<IAppointmentData> lstAppts = new List<IAppointmentData>();
            var appointmentData = new Mock<IAppointmentData>(MockBehavior.Strict);
            appointmentData.SetupAllProperties();
            appointmentData.SetupGet(_ => _.CancelWindow).Returns(eCancelWindow);
            appointmentData.SetupGet(_ => _.CustomerId).Returns(eCustomerId);
            appointmentData.SetupGet(_ => _.DateTime).Returns(eDateTime); ;
            appointmentData.SetupGet(_ => _.DesignerId).Returns(eDesigner);
            appointmentData.SetupGet(_ => _.Fee).Returns(eFee);
            appointmentData.SetupGet(_ => _.Id).Returns(eId);
            appointmentData.SetupGet(_ => _.SpecialtyId).Returns(eSpcId);
            lstAppts.Add(appointmentData.Object);

            var desgData = new Mock<IDesignerData>(MockBehavior.Strict);
            desgData.SetupAllProperties();
            desgData.SetupGet(_ => _.Id).Returns(generator.Generate<int>());
            desgData.SetupGet(_ => _.Name).Returns(generator.Generate<string>());

            var apptRepository = new Mock<IAppointmentRepository>(MockBehavior.Strict);
            apptRepository.Setup(_ => _.GetCustomerActiveAppointments(It.IsAny<int>()))
                .Returns(lstAppts.AsEnumerable());
            var desgRepository = new Mock<IDesignerRepository>(MockBehavior.Strict);
            desgRepository.Setup(_ => _.GetDesigner(It.IsAny<int>())).Returns(desgData.Object);

            new MockTestBuilderComposition().Compose(builder);
            builder.RegisterInstance(apptRepository.Object).As<IAppointmentRepository>();
            builder.RegisterInstance(desgRepository.Object).As<IDesignerRepository>();
            builder.RegisterType<MagenicMasters.CslaLab.Customer.AppointmentViewCollection>().As<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentViewCollection>();


            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            //act
            var objPortal = IoC.Container.Resolve<IObjectPortal<MagenicMasters.CslaLab.Contracts.Customer.IAppointmentViewCollection>>();
            var apptViewColl = objPortal.Fetch(generator.Generate<int>());

            //assert
            Assert.IsNotNull(apptViewColl);
            Assert.AreEqual(apptViewColl.First().StartDateTime, eDateTime);
            Assert.AreEqual(apptViewColl.First().DesignerName, desgData.Object.Name);

        }
    }

    public class MockTestBuilderComposition: IContainerBuilderComposition
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
        }
    }
}

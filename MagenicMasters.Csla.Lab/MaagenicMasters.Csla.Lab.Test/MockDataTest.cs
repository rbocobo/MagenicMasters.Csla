using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using CS = Csla.Server;
using C = Csla;
using MagenicMasters.CslaLab.Customer;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Designer;
using Moq;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.DataAccess;

using Csla.Server;
using MagenicMasters.CslaLab.Contracts.Customer;
using MagenicMasters.CslaLab.Contracts.Designer;
using Spackle;

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
                .Returns(weekScheduleData.Object);


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
        }

        [TestMethod]
        public void AddWeekSchedulePassed()
        {

        }

        [TestMethod]
        public void GetWeekSchedulePassed()
        {

        }

        [TestMethod]
        public void UpdateWeekSchedulePassed()
        {

        }

        [TestMethod]
        public void CreateDayScheduleOverridePassed()
        {

        }

        [TestMethod]
        public void AddDayScheduleOverridePassed()
        {

        }

        [TestMethod]
        public void GetDayScheduleOverridePassed()
        {

        }

        [TestMethod]
        public void UpdateDayScheduleOverridePassed()
        {

        }

        [TestMethod]
        public void SaveChangesPassed()
        {

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

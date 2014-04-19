using Autofac;
using Csla;
using Csla.Server;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.Customer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MagenicMasters.CslaLab.Designer;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.EF;
using MagenicMasters.CslaLab.Contracts.Customer;
using MagenicMasters.CslaLab.Contracts.Designer;

namespace MaagenicMasters.CslaLab.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PortalTest
    {

        private static IContainer Container { get; set; }

        public PortalTest()
        {
            //
            // TODO: Add constructor logic here
            //
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


        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(ObjectPortal<>))
                .As(typeof(IObjectPortal<>))
                .InstancePerLifetimeScope();
            builder.RegisterType<ChildObjectPortal>()
                .As<IChildObjectPortal>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ScheduleRepository>()
                .As<IScheduleRepository>()
                .InstancePerLifetimeScope();

            return builder.Build();
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<ObjectActivator>()
                .As<IDataPortalActivator>()
                .WithParameter("container", GetContainer())
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ObjectPortal<>))
                .As(typeof(IObjectPortal<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ChildObjectPortal>()
                .As<IChildObjectPortal>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TimeEntry>()
                .As<ITimeEntry>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TimeEntries>()
                .As<ITimeEntries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestAppointmentCommand>()
                .As<IRequestAppointmentCommand>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WorkSchedule>()
                .As<IWorkSchedule>()
                .InstancePerLifetimeScope();

            

            Container = builder.Build();

            using(var scope = Container.BeginLifetimeScope())
            {
                ApplicationContext.DataPortalActivator = scope.Resolve<IDataPortalActivator>();
            }

            
            
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void TestCommandResolve()
        {
            using(var scope = Container.BeginLifetimeScope())
            {
                var cmd = scope.Resolve<IRequestAppointmentCommand>() as RequestAppointmentCommand;
                Assert.IsNotNull(cmd);
                
            }
        }

        [TestMethod]
        public void PassWhen_DataPortalActivatorIsEqualToConcreteType()
        {

            Assert.IsTrue(ApplicationContext.DataPortalActivator.GetType().Equals(typeof(ObjectActivator)));

        }

        [TestMethod]
        public void PassWhen_IChildObjectPortalIsResolved()
        { 
            using(var scope = Container.BeginLifetimeScope())
            {
                var childObjectPortal = scope.Resolve<IChildObjectPortal>();
                Assert.IsNotNull(childObjectPortal);
                Assert.IsInstanceOfType(childObjectPortal, typeof(ChildObjectPortal));
            }
        }

        [TestMethod]
        public void PassWhen_IChildObjectInvokeCreateChild()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var childObjectPortal = scope.Resolve<IChildObjectPortal>();
                var itimeentry = childObjectPortal.CreateChild<ITimeEntry>();
                Assert.IsNotNull(itimeentry);
                Assert.IsInstanceOfType(itimeentry, typeof(TimeEntry));
            }
        }

        [TestMethod]
        public void PassWhen_IObjectPortalIsResolved()
        {
            using(var scope = Container.BeginLifetimeScope())
            {
                var objectPortal = scope.Resolve<IObjectPortal<IWorkSchedule>>();
                Assert.IsNotNull(objectPortal);
            }
        }

        [TestMethod]
        public void PassWhen_IObjectPortalCreate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var objectPortal = scope.Resolve<IObjectPortal<IWorkSchedule>>();
                Assert.IsNotNull(objectPortal);

                var appointmentReq = objectPortal.Create();
                Assert.IsNotNull(appointmentReq);
                Assert.IsInstanceOfType(appointmentReq, typeof(WorkSchedule));
            }
        }
    }
}

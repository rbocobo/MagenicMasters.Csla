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

namespace MaagenicMasters.Csla.Lab.Test
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

        [TestMethod]
        public void CreateWeekSchedulePassedEF()
        {
            //arrange
            this.builder = new ContainerBuilder();

            new RealDataTestBuilderComposition().Compose(builder);
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

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
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<WorkSchedule>().As<IWorkSchedule>();

            IoC.Container = builder.Build();
            var activator = IoC.Container.Resolve<IDataPortalActivator>();
            C.ApplicationContext.DataPortalActivator = IoC.Container.Resolve<IDataPortalActivator>();

            //act
            var objectPortal = IoC.Container.Resolve<IObjectPortal<IWorkSchedule>>();
            var workSchedule = objectPortal.Create();
            //assert
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
        }
    }

}

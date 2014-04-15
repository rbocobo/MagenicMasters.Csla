using Autofac;
using Csla;
using Csla.Server;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            UseObjectFactoryDirectly();
        }

        private static void UseObjectFactoryDirectly()
        {
            ApplicationContext.DataPortalActivator = new ObjectActivator(CreateContainer());

            var command = new ObjectPortal<IAppointmentRequest>().Create();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            

            //builder.RegisterType<ObjectActivator>()
            //    .As<IDataPortalActivator>()
            //    .WithParameter("container", GetContainer())
            //    .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ObjectPortal<>))
                .As(typeof(IObjectPortal<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ChildObjectPortal>()
                .As<IChildObjectPortal>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TimeEntry>()
                .As<ITimeEntry>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TimeEntryCollection>()
                .As<ITimeEntries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestAppointmentCommand>()
                .As<IRequestAppointmentCommand>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppointmentRequest>()
                .As<IAppointmentRequest>()
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}

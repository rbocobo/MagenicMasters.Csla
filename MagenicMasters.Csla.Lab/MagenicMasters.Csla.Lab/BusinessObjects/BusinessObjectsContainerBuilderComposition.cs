using Autofac;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.BusinessObjects
{
    public sealed class BusinessObjectsContainerBuilderComposition : IContainerBuilderComposition
    {
        public void Compose(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ObjectPortal<>)).As(typeof(IObjectPortal<>));
            builder.RegisterType(typeof(ChildObjectPortal)).As(typeof(IChildObjectPortal));
        }
    }
}

using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    public interface IContainerBuilderComposition
    {
        void Compose(ContainerBuilder builder);
    }
}

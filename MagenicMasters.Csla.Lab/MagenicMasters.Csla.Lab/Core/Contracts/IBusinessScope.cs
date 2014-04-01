using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core.Contracts
{
    internal interface IBusinessScope
    {
        ILifetimeScope Scope { get; set; }
    }
}

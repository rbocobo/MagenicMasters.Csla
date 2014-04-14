using Autofac;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    [Serializable]
    public abstract class ReadOnlyBaseScopeCore<T>
        :ReadOnlyBaseCore<T>, IBusinessScope
        where T : ReadOnlyBaseScopeCore<T>
    {
        protected ReadOnlyBaseScopeCore()
            : base() { }

        [NonSerialized]
        private ILifetimeScope scope;
        ILifetimeScope IBusinessScope.Scope
        { 
            get { return this.scope; }
            set { this.scope = value; }
        }

    }
}

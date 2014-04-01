using Autofac;
using MagenicMasters.Csla.Lab.Core.Contracts;
using MagenicMasters.Csla.Lab.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.Core
{
    [Serializable]
    public abstract class BusinessBaseScopeCore<T>
        :BusinessBaseCore<T>, IBusinessScope
        where T : BusinessBaseScopeCore<T>
    {
        protected BusinessBaseScopeCore()
            : base() { }
        [NonSerialized]
        private ILifetimeScope scope;
        [InjectedObjectPortal]
        ILifetimeScope IBusinessScope.Scope
        {
            get { return this.scope; }
            set { this.scope = value; }
        }
    }
}

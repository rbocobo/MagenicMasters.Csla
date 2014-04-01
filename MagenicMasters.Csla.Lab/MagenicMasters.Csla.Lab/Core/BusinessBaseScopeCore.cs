using Autofac;
using MagenicMasters.CslaLab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
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
        ILifetimeScope IBusinessScope.Scope
        {
            get { return this.scope; }
            set { this.scope = value; }
        }
    }
}

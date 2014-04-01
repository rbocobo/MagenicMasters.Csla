using Autofac;
using Csla.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    [Serializable]
    public abstract class ReadOnlyListBaseScopeCore<T,C>
        :ReadOnlyListBaseCore<T,C>, IBusinessScope
        where T : ReadOnlyListBaseScopeCore<T, C>
        where C : IReadOnlyObject
    {
        protected ReadOnlyListBaseScopeCore():base(){}
        [NonSerialized]
        private ILifetimeScope scope;
        ILifetimeScope IBusinessScope.Scope
        {
            get { return this.scope; }
            set { this.scope = value; }
        }
    }
}

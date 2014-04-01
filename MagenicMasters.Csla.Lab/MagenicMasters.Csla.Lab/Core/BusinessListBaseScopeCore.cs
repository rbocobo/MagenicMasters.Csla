using Autofac;
using Csla.Core;
using MagenicMasters.Csla.Lab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.Core
{
    [Serializable]
    public class BusinessListBaseScopeCore<T,C>
        : BusinessListBaseCore<T,C>, IBusinessScope
        where T: BusinessListBaseScopeCore<T,C>
        where C : IEditableBusinessObject
    {
        protected BusinessListBaseScopeCore(): base()   
        {
            
        }
        [NonSerialized]
        private ILifetimeScope scope;
        ILifetimeScope IBusinessScope.Scope
        {
            get { return this.scope; }
            set { this.scope = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Contracts.Customer;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class TimeEntries :
      BusinessListBaseScopeCore<TimeEntries, ITimeEntry>, ITimeEntries
    {
        [NonSerialized]
        private IChildObjectPortal childObjectPortal;
        [Dependency]
        public IChildObjectPortal ChildObjectPortal
        {
            get { return childObjectPortal; }
            set { childObjectPortal = value; }
        }

        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
        }

        protected override void Child_Create()
        {
            base.Child_Create();
        }
    }
}

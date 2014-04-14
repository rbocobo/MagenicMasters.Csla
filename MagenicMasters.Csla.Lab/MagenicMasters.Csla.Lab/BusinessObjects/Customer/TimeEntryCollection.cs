using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Core;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class TimeEntryCollection :
      BusinessListBaseScopeCore<TimeEntryCollection, ITimeEntry>, ITimeEntries
    {

        [Dependency]
        public IChildObjectPortal ChildObjectPortal { get; set; }

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

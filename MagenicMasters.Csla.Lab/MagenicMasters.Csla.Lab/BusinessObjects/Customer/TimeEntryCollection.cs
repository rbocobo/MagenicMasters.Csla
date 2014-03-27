using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.Csla.Lab.BusinessContracts;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class TimeEntryCollection :
      BusinessListBase<TimeEntryCollection, ITimeEntry>, ITimeEntries
    {
        #region Factory Methods

        internal static TimeEntryCollection NewEditableChildList()
        {
            return DataPortal.CreateChild<TimeEntryCollection>();
        }

        internal static TimeEntryCollection GetEditableChildList(
          object childData)
        {
            return DataPortal.FetchChild<TimeEntryCollection>(childData);
        }

        private TimeEntryCollection()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            foreach (var child in (IList<object>)childData)
                this.Add(TimeEntry.GetEditableChild(child));
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}

using System;
using Csla;

namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class DayType : NameValueListBase<int, string>
    {

        #region Data Access

        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            // TODO: load values
            //object listData = null;
            //foreach (var item in listData)
            //  Add(new NameValueListBase<int, string>.
            //    NameValuePair(item.Key, item.Value));
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}

using System;
using Csla;

namespace MagenicMasters.Csla.Lab.Customer
{
    [Serializable]
    public class SpecialtyCollection : NameValueListBase<int, string>
    {
        #region Factory Methods

        private static SpecialtyCollection _list;

        public static SpecialtyCollection GetNameValueList()
        {
            if (_list == null)
                _list = DataPortal.Fetch<SpecialtyCollection>();
            return _list;
        }

        public static void InvalidateCache()
        {
            _list = null;
        }

        private SpecialtyCollection()
        { /* require use of factory methods */ }

        #endregion

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

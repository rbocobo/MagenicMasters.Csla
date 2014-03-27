using System;
using System.Collections.Generic;
using Csla;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentViewCollection :
      ReadOnlyListBase<AppointmentViewCollection, AppointmentView>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(AppointmentViewCollection), "Role");
        }

        #endregion

        #region Factory Methods

        public static AppointmentViewCollection GetReadOnlyList(string filter)
        {
            return DataPortal.Fetch<AppointmentViewCollection>(filter);
        }

        private AppointmentViewCollection()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        //private void DataPortal_Fetch(string criteria)
        //{
        //    RaiseListChangedEvents = false;
        //    IsReadOnly = false;
        //    // TODO: load values
        //    object objectData = null;
        //    foreach (var child in (List<object>)objectData)
        //        Add(ReadOnlyChild.GetReadOnlyChild(child));
        //    IsReadOnly = true;
        //    RaiseListChangedEvents = true;
        //}

        #endregion
    }
}

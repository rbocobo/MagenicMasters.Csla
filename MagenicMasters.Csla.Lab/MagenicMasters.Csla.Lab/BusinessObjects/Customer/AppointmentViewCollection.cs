using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.BusinessObjects.Contracts;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentViewCollection :
      ReadOnlyListBaseScopeCore<AppointmentViewCollection, AppointmentView>, IAppointmentViewCollection
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(AppointmentViewCollection), "Role");
        }

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

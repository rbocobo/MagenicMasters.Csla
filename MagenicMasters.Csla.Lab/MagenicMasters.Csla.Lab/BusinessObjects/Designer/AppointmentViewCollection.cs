using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.Csla.Lab.Core.Contracts;
using MagenicMasters.Csla.Lab.CustomAttributes;

namespace MagenicMasters.CslaLab.Designer
{
    [Serializable]
    public class AppointmentViewCollection :
      ReadOnlyListBase<AppointmentViewCollection, AppointmentView>
    {
        [InjectedObjectPortal]
        public IChildObjectPortal ChildObjectPortal { get; set; }

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
        //        Add(AppointmentView.GetReadOnlyChild(child));
        //    IsReadOnly = true;
        //    RaiseListChangedEvents = true;
        //}

        #endregion
    }
}

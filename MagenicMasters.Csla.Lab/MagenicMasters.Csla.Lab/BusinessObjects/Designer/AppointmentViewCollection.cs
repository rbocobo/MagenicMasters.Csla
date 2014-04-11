using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.BusinessObjects.Contracts;

namespace MagenicMasters.CslaLab.Designer
{
    [Serializable]
    public class AppointmentViewCollection :
      ReadOnlyListBaseScopeCore<AppointmentViewCollection, AppointmentView>, IAppointmentViewCollection
    {
        [Dependency]
        public IChildObjectPortal ChildObjectPortal { get; set; }

        [Dependency]
        public IAppointmentRepository AppointmentRepository { get; set; }

        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(AppointmentViewCollection), "Role");
        }

        #endregion


        #region Data Access

        protected  void DataPortal_Fetch(int criteria)
        {
            IEnumerable<IAppointmentData> data = this.AppointmentRepository.GetDesignerActiveAppointments(criteria);
            foreach(var item in data )
            {
                this.ChildObjectPortal.FetchChild<AppointmentView>(item);
            }
        }

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

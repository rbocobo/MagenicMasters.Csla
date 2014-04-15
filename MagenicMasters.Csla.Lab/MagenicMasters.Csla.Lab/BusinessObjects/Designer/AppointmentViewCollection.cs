using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Contracts.Designer;
using MagenicMasters.CslaLab.Criteria;

namespace MagenicMasters.CslaLab.Designer
{
    [Serializable]
    public class AppointmentViewCollection :
      ReadOnlyListBaseScopeCore<AppointmentViewCollection, IAppointmentView>, IAppointmentViewCollection
    {
        [NonSerialized]
        private IChildObjectPortal childObjectPortal;
        [Dependency]
        public IChildObjectPortal ChildObjectPortal
        {
            get { return childObjectPortal; }
            set { childObjectPortal = value; }
        }

        [NonSerialized]
        private IAppointmentRepository appointmentRepository;
        [Dependency]
        public IAppointmentRepository AppointmentRepository
        {
            get { return appointmentRepository; }
            set { appointmentRepository = value; }
        }

        [NonSerialized]
        private ICustomerRepository customerRepository;
        [Dependency]
        public ICustomerRepository CustomerRepository
        {
            get { return customerRepository; }
            set { customerRepository = value; }
        }

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
            IsReadOnly = false;
            foreach(var item in data )
            {
                var custData = this.CustomerRepository.GetCustomer(item.CustomerId);
                var child = this.ChildObjectPortal.FetchChild<IAppointmentView>(new CustomerAppointmentViewCriteria(custData.Name, "test", item.DateTime));
                Add(child);
            }
            IsReadOnly = true;
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

using System;
using System.Collections.Generic;
using Csla;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System.Linq;
using MagenicMasters.CslaLab.Criteria;
namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentViewCollection :
      ReadOnlyListBaseScopeCore<AppointmentViewCollection, ICustomerAppointmentView>, ICustomerAppointmentViewCollection
    {

        [Dependency]
        public IChildObjectPortal ChildObjectPortal { get; set; }

        [Dependency]
        public IAppointmentRepository AppointmentRepository { get; set; }

        [Dependency]
        public ICustomerRepository CustomerRepository { get; set; }

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
            IEnumerable<IAppointmentData> data = this.AppointmentRepository.GetCustomerActiveAppointments(criteria);
            
            foreach (var item in data)
            {
                ICustomerData custData = this.CustomerRepository.GetCustomer(item.CustomerId);
                AppointmentViewCriteria childcriteria = new AppointmentViewCriteria(item.Id, item.DateTime, item.DateTime, custData.Name, item.Fee);
                
                this.ChildObjectPortal.FetchChild<AppointmentView>(item);
            }
        }


        #endregion
    }
}

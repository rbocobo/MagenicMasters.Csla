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
using MagenicMasters.CslaLab.Contracts.Customer;
namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentViewCollection :
      ReadOnlyListBaseScopeCore<AppointmentViewCollection, IAppointmentView>, IAppointmentViewCollection
    {

        [Dependency]
        public IChildObjectPortal ChildObjectPortal { get; set; }

        [Dependency]
        public IAppointmentRepository AppointmentRepository { get; set; }

        [Dependency]
        public IDesignerRepository DesignerRepository { get; set; }

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
            IsReadOnly = false;
            foreach (var item in data)
            {
                IDesignerData desData = this.DesignerRepository.GetDesigner(item.DesignerId);
                AppointmentViewCriteria childcriteria = new AppointmentViewCriteria(item.Id, item.DateTime, item.DateTime, desData.Name, item.Fee);

                Add(this.ChildObjectPortal.FetchChild<AppointmentView>(childcriteria));
            }
            IsReadOnly = true;
        }


        #endregion
    }
}

using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Contracts.Customer;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Criteria;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentView : ReadOnlyBaseCore<AppointmentView>, IAppointmentView
    {
        #region Business Methods

        public static readonly PropertyInfo<int> AppointmentIdProperty =
    PropertyInfoRegistration.Register<AppointmentView, int>(_ => _.AppointmentId);
        public int AppointmentId
        {
            get { return this.ReadProperty(AppointmentView.AppointmentIdProperty); }
            private set { this.LoadProperty(AppointmentView.AppointmentIdProperty, value); }
        }


        public static readonly PropertyInfo<DateTime> StartDateTimeProperty =
    PropertyInfoRegistration.Register<AppointmentView, DateTime>(_ => _.StartDateTime);
        public DateTime StartDateTime
        {
            get { return this.ReadProperty(AppointmentView.StartDateTimeProperty); }
            private set { this.LoadProperty(AppointmentView.StartDateTimeProperty, value); }
        }


        public static readonly PropertyInfo<DateTime> EndDateTimeProperty =
    PropertyInfoRegistration.Register<AppointmentView, DateTime>(_ => _.EndDateTime);
        public DateTime EndDateTime
        {
            get { return this.ReadProperty(AppointmentView.EndDateTimeProperty); }
            private set { this.LoadProperty(AppointmentView.EndDateTimeProperty, value); }
        }


        public static readonly PropertyInfo<string> DesignerNameProperty =
    PropertyInfoRegistration.Register<AppointmentView, string>(_ => _.DesignerName);
        [MaxLength(200)]
        public string DesignerName
        {
            get { return this.ReadProperty(AppointmentView.DesignerNameProperty); }
            private set { this.LoadProperty(AppointmentView.DesignerNameProperty, value); }
        }


        public static readonly PropertyInfo<decimal> FeeProperty =
    PropertyInfoRegistration.Register<AppointmentView, decimal>(_ => _.Fee);
        public decimal Fee
        {
            get { return this.ReadProperty(AppointmentView.FeeProperty); }
            private set { this.LoadProperty(AppointmentView.FeeProperty, value); }
        }


        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion



        #region Data Access

        private void Child_Fetch(AppointmentViewCriteria childData)
        {
            // TODO: load values from childData
            
            LoadProperty(AppointmentIdProperty, childData.AppointmentId);
            LoadProperty(StartDateTimeProperty, childData.StartDateTime);
            LoadProperty(EndDateTimeProperty, childData.EndDateTime);
            LoadProperty(DesignerNameProperty, childData.Name);
            LoadProperty(FeeProperty, childData.Fee);
            
        }

        #endregion
    }
}

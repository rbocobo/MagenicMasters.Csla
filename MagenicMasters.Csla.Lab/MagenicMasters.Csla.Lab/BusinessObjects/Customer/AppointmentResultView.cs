using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Contracts.Customer;
namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentResultView : ReadOnlyBaseScopeCore<AppointmentResultView>, IAppointmentResultView
    {
        #region Business Methods

        public static readonly PropertyInfo<DateTime> StartDateTimeProperty =
        PropertyInfoRegistration.Register<AppointmentResultView, DateTime>(_ => _.StartDateTime);
        public DateTime StartDateTime
        {
            get { return this.ReadProperty(AppointmentResultView.StartDateTimeProperty); }
            private set { this.LoadProperty(AppointmentResultView.StartDateTimeProperty, value); }
        }


        public static readonly PropertyInfo<DateTime> EndDateTimeProperty =
         PropertyInfoRegistration.Register<AppointmentResultView, DateTime>(_ => _.EndDateTime);
        public DateTime EndDateTime
        {
            get { return this.ReadProperty(AppointmentResultView.EndDateTimeProperty); }
            private set { this.LoadProperty(AppointmentResultView.EndDateTimeProperty, value); }
        }


        public static readonly PropertyInfo<string> DesignerNameProperty =
        PropertyInfoRegistration.Register<AppointmentResultView, string>(_ => _.DesignerName);
        [MaxLength(200)]
        public string DesignerName
        {
            get { return this.ReadProperty(AppointmentResultView.DesignerNameProperty); }
            private set { this.LoadProperty(AppointmentResultView.DesignerNameProperty, value); }
        }


        public static readonly PropertyInfo<decimal> FeeProperty =
        PropertyInfoRegistration.Register<AppointmentResultView, decimal>(_ => _.Fee);
        public decimal Fee
        {
            get { return this.ReadProperty(AppointmentResultView.FeeProperty); }
            private set { this.LoadProperty(AppointmentResultView.FeeProperty, value); }
        }


        public static readonly PropertyInfo<decimal> PartialFeeProperty =
        PropertyInfoRegistration.Register<AppointmentResultView, decimal>(_ => _.PartialFee);
        public decimal PartialFee
        {
            get { return this.ReadProperty(AppointmentResultView.PartialFeeProperty); }
            private set { this.LoadProperty(AppointmentResultView.PartialFeeProperty, value); }
        }
        [NonSerialized]
        private IDesignerRepository designerRepository;
        [Dependency]
        public IDesignerRepository DesignerRepository
        {
            get { return designerRepository; }
            set { designerRepository = value; }
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

        private void Child_Fetch(IAppointmentData childData)
        {
            // TODO: load values from childData

            var designer = this.designerRepository.GetDesigner(childData.DesignerId);
            this.LoadProperty(StartDateTimeProperty, childData.DateTime);
            this.LoadProperty(EndDateTimeProperty, childData.DateTime.AddHours(1));
            this.LoadProperty(DesignerNameProperty, designer.Name);
            
        }

        #endregion
    }
}

using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.CslaLab.Core;


namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class DesignerUtilizationReportView : ReadOnlyBase<DesignerUtilizationReportView>
    {
        #region Business Methods


        public static readonly PropertyInfo<string> DesignerNameProperty =
    PropertyInfoRegistration.Register<DesignerUtilizationReportView, string>(_ => _.DesignerName);
        [MaxLength(200)]
        public string DesignerName
        {
            get { return this.ReadProperty(DesignerUtilizationReportView.DesignerNameProperty); }
            private set { this.LoadProperty(DesignerUtilizationReportView.DesignerNameProperty, value); }
        }

        public static readonly PropertyInfo<bool> IsBusyProperty =
    PropertyInfoRegistration.Register<DesignerUtilizationReportView, bool>(_ => _.IsBusy);
        public bool IsBusy
        {
            get { return this.ReadProperty(DesignerUtilizationReportView.IsBusyProperty); }
            private set { this.LoadProperty(DesignerUtilizationReportView.IsBusyProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> AppointmentProperty =
    PropertyInfoRegistration.Register<DesignerUtilizationReportView, DateTime>(_ => _.Appointment);
        public DateTime Appointment
        {
            get { return this.ReadProperty(DesignerUtilizationReportView.AppointmentProperty); }
            private set { this.LoadProperty(DesignerUtilizationReportView.AppointmentProperty, value); }
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

        #region Factory Methods

        internal static DesignerUtilizationReportView GetReadOnlyChild(object childData)
        {
            return DataPortal.FetchChild<DesignerUtilizationReportView>(childData);
        }

        private DesignerUtilizationReportView()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(object childData)
        {
            // TODO: load values from childData
        }

        #endregion
    }
}

using System;
using Csla;
using MagenicMasters.CslaLab.Core;

namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class UtilizationReportView : ReadOnlyBase<UtilizationReportView>
    {
        #region Business Methods


        public static readonly PropertyInfo<DateTime> DateFromProperty =
	        PropertyInfoRegistration.Register<UtilizationReportView,DateTime>(_ => _.DateFrom);
        public DateTime DateFrom
        {
	        get { return this.ReadProperty(UtilizationReportView.DateFromProperty); }
	        private set { this.LoadProperty(UtilizationReportView.DateFromProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> DateToProperty =
            PropertyInfoRegistration.Register<UtilizationReportView, DateTime>(_ => _.DateTo);
        public DateTime DateTo
        {
            get { return this.ReadProperty(UtilizationReportView.DateToProperty); }
            private set { this.LoadProperty(UtilizationReportView.DateToProperty, value); }
        }

        public static readonly PropertyInfo<DesignerUtilizationReportViewCollection> ReportViewProperty =
            PropertyInfoRegistration.Register<UtilizationReportView, DesignerUtilizationReportViewCollection>(_ => _.ReportView);
        public DesignerUtilizationReportViewCollection ReportView
        {
            get { return this.ReadProperty(UtilizationReportView.ReportViewProperty); }
            private set { this.LoadProperty(UtilizationReportView.ReportViewProperty, value); }
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

        private void DataPortal_Fetch(int criteria)
        {
            // TODO: load values
        }

        #endregion
    }
}

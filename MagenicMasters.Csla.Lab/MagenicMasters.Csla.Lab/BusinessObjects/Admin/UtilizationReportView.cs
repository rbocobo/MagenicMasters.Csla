using System;
using Csla;

namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class UtilizationReportView : ReadOnlyBase<UtilizationReportView>
    {
        #region Business Methods

        public static readonly PropertyInfo<DateTime> DateFromProperty = RegisterProperty<DateTime>(c => c.DateFrom);
        public DateTime DateFrom
        {
            get { return ReadProperty(DateFromProperty); }
            private set { LoadProperty(DateFromProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> DateToProperty = RegisterProperty<DateTime>(c => c.DateTo);
        public DateTime DateTo
        {
            get { return ReadProperty(DateToProperty); }
            private set { LoadProperty(DateToProperty, value); }
        }

        public static readonly PropertyInfo<DesignerUtilizationReportViewCollection> ReportViewProperty = RegisterProperty<DesignerUtilizationReportViewCollection>(c => c.ReportView);
        public DesignerUtilizationReportViewCollection ReportView
        {
            get { return ReadProperty(ReportViewProperty); }
            private set { LoadProperty(ReportViewProperty, value); }
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

        public static UtilizationReportView GetReadOnlyRoot(int id)
        {
            return DataPortal.Fetch<UtilizationReportView>(id);
        }

        private UtilizationReportView()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(int criteria)
        {
            // TODO: load values
        }

        #endregion
    }
}

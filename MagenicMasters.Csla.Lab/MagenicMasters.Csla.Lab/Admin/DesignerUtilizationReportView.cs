using System;
using Csla;

namespace MagenicMasters.Csla.Lab.Admin
{
    [Serializable]
    public class DesignerUtilizationReportView : ReadOnlyBase<DesignerUtilizationReportView>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> DesignerNameProperty = RegisterProperty<string>(c => c.DesignerName);
        public string DesignerName
        {
            get { return ReadProperty(DesignerNameProperty); }
            private set { LoadProperty(DesignerNameProperty, value); }
        }

        public static readonly PropertyInfo<bool> IsBusyProperty = RegisterProperty<bool>(c => c.IsBusy);
        public bool IsBusy
        {
            get { return ReadProperty(IsBusyProperty); }
            private set { LoadProperty(IsBusyProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> AppointmentProperty = RegisterProperty<DateTime>(c => c.Appointment);
        public DateTime Appointment
        {
            get { return ReadProperty(AppointmentProperty); }
            private set { LoadProperty(AppointmentProperty, value); }
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

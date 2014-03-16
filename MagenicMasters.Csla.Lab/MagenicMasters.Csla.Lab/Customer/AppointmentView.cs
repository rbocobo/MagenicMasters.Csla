using System;
using Csla;

namespace MagenicMasters.Csla.Lab.Customer
{
    [Serializable]
    public class AppointmentView : ReadOnlyBase<AppointmentView>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> AppointmentIdProperty = RegisterProperty<int>(c => c.AppointmentId);
        public int AppointmentId
        {
            get { return ReadProperty(AppointmentIdProperty); }
            private set { LoadProperty(AppointmentIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> StartDateTimeProperty = RegisterProperty<DateTime>(c => c.StartDateTime);
        public DateTime StartDateTime
        {
            get { return ReadProperty(StartDateTimeProperty); }
            private set { LoadProperty(StartDateTimeProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndDateTimeProperty = RegisterProperty<DateTime>(c => c.EndDateTime);
        public DateTime EndDateTime
        {
            get { return ReadProperty(EndDateTimeProperty); }
            private set { LoadProperty(EndDateTimeProperty, value); }
        }

        public static readonly PropertyInfo<string> DesignerNameProperty = RegisterProperty<string>(c => c.DesignerName);
        public string DesignerName
        {
            get { return ReadProperty(DesignerNameProperty); }
            private set { LoadProperty(DesignerNameProperty, value); }
        }

        public static readonly PropertyInfo<decimal> FeeProperty = RegisterProperty<decimal>(c => c.Name);
        public decimal Name
        {
            get { return ReadProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
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

        internal static AppointmentView GetReadOnlyChild(object childData)
        {
            return DataPortal.FetchChild<AppointmentView>(childData);
        }

        private AppointmentView()
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

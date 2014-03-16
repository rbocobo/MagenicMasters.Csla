using System;
using Csla;

namespace MagenicMasters.Csla.Lab.Customer
{
    [Serializable]
    public class AppointmentResultView : ReadOnlyBase<AppointmentResultView>
    {
        #region Business Methods

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

        public static readonly PropertyInfo<decimal> FeeProperty = RegisterProperty<decimal>(c => c.Fee);
        public decimal Fee
        {
            get { return ReadProperty(FeeProperty); }
            private set { LoadProperty(FeeProperty, value); }
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

        internal static AppointmentResultView GetReadOnlyChild(object childData)
        {
            return DataPortal.FetchChild<AppointmentResultView>(childData);
        }

        private AppointmentResultView()
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

using System;
using Csla;
using System.ComponentModel.DataAnnotations;

namespace MagenicMasters.CslaLab.Customer
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
        [MaxLength(200)]
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



        #region Data Access

        private void Child_Fetch(object childData)
        {
            // TODO: load values from childData
        }

        #endregion
    }
}

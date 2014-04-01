using System;
using Csla;
using System.ComponentModel.DataAnnotations;

namespace MagenicMasters.CslaLab.Designer
{
    [Serializable]
    public class AppointmentView : ReadOnlyBase<AppointmentView>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> CustomerNameProperty = RegisterProperty<string>(c => c.CustomerName);
        [MaxLength(200)]
        public string CustomerName
        {
            get { return GetProperty(CustomerNameProperty); }
            set { LoadProperty(CustomerNameProperty, value); }
        }

        public static readonly PropertyInfo<string> SpecialtyProperty = RegisterProperty<string>(c => c.Specialty);
        [MaxLength(100)]
        public string Specialty
        {
            get { return GetProperty(SpecialtyProperty); }
            set { LoadProperty(SpecialtyProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> DateProperty = RegisterProperty<DateTime>(c => c.Date);
        public DateTime Date
        {
            get { return GetProperty(DateProperty); }
            set { LoadProperty(DateProperty, value); }
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

using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.Csla.Lab.Contracts;
using MagenicMasters.Csla.Lab.Core;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class TimeEntry : BusinessBaseScopeCore<TimeEntry>, ITimeEntry
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> StartDateTimeProperty = RegisterProperty<DateTime>(c => c.StartDateTime);
        [Required]
        public DateTime StartDateTime
        {
            get { return GetProperty(StartDateTimeProperty); }
            set { SetProperty(StartDateTimeProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndDateTimeProperty = RegisterProperty<DateTime>(c => c.EndDateTime);
        [Required]
        public DateTime EndDateTime
        {
            get { return GetProperty(EndDateTimeProperty); }
            set { SetProperty(EndDateTimeProperty, value); }
        }
        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            var innerRule1 = new TopOfTheHourRule(StartDateTimeProperty);
            var innerRule2 = new TopOfTheHourRule(EndDateTimeProperty);
            BusinessRules.AddRule(new TimeRangeRule(StartDateTimeProperty, EndDateTimeProperty, innerRule1, innerRule2));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        internal static TimeEntry NewEditableChild()
        {
            return DataPortal.CreateChild<TimeEntry>();
        }

        internal static TimeEntry GetEditableChild(object childData)
        {
            return DataPortal.FetchChild<TimeEntry>(childData);
        }

        public TimeEntry()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        protected override void Child_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.Child_Create();
        }

        private void Child_Fetch(object childData)
        {
            // TODO: load values
        }

        private void Child_Insert(object parent)
        {
            // TODO: insert values
        }

        private void Child_Update(object parent)
        {
            // TODO: update values
        }

        private void Child_DeleteSelf(object parent)
        {
            // TODO: delete values
        }

        #endregion
    }
}

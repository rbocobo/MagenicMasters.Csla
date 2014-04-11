using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class TimeEntry : BusinessBaseScopeCore<TimeEntry>, ITimeEntry
    {
        #region Business Methods


        public static readonly PropertyInfo<int> IdProperty =
    PropertyInfoRegistration.Register<TimeEntry, int>(_ => _.Id);
        public int Id
        {
            get { return this.ReadProperty(TimeEntry.IdProperty); }
            set { this.SetProperty(TimeEntry.IdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> StartDateTimeProperty =
    PropertyInfoRegistration.Register<TimeEntry, DateTime>(_ => _.StartDateTime);
        [Required]
        public DateTime StartDateTime
        {
            get { return this.ReadProperty(TimeEntry.StartDateTimeProperty); }
            set { this.SetProperty(TimeEntry.StartDateTimeProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndDateTimeProperty =
    PropertyInfoRegistration.Register<TimeEntry, DateTime>(_ => _.EndDateTime);
        [Required]
        public DateTime EndDateTime
        {
            get { return this.ReadProperty(TimeEntry.EndDateTimeProperty); }
            set { this.SetProperty(TimeEntry.EndDateTimeProperty, value); }
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

using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using Csla.Rules;
using Csla.Rules.CommonRules;
using MagenicMasters.CslaLab.Common;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core;

namespace MagenicMasters.CslaLab.Designer
{
    [Serializable]
    public class DayScheduleOverride : BusinessBaseScopeCore<DayScheduleOverride>, IDayScheduleOverride
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> DateProperty = RegisterProperty<DateTime>(c => c.Date);
        [Required]
        public DateTime Date
        {
            get { return GetProperty(DateProperty); }
            set { SetProperty(DateProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> StartTimeProperty = RegisterProperty<DateTime>(c => c.StartTime);
        [Required]
        public DateTime StartTime
        {
            get { return GetProperty(StartTimeProperty); }
            set { SetProperty(StartTimeProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndTimeProperty = RegisterProperty<DateTime>(c => c.EndTime);
        [Required]
        public DateTime EndTime
        {
            get { return GetProperty(EndTimeProperty); }
            set { SetProperty(EndTimeProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();
            BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, DateProperty, UserRole.Designers));
            BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, DateProperty, UserRole.Designers));
            BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, StartTimeProperty, UserRole.Designers));
            BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, EndTimeProperty, UserRole.Designers));
            BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, StartTimeProperty, UserRole.Designers));
            BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, EndTimeProperty, UserRole.Designers));

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
            
        }

        #endregion


        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(int criteria)
        {
            // TODO: load values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // TODO: insert values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // TODO: update values
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Id);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int criteria)
        {
            // TODO: delete values
        }

        #endregion
    }
}

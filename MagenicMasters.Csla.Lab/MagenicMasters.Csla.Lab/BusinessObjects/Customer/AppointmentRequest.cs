using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.CslaLab;
using MagenicMasters.Csla.Lab.Contracts;
using MagenicMasters.Csla.Lab.Core.Contracts;
using MagenicMasters.Csla.Lab.CustomAttributes;
using MagenicMasters.Csla.Lab.Core;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentRequest : BusinessBaseScopeCore<AppointmentRequest>, IAppointmentRequest
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<bool> IsFullDesignerProperty = RegisterProperty<bool>(c => c.IsFullDesigner);
        [Required]
        public bool IsFullDesigner
        {
            get { return GetProperty(IsFullDesignerProperty); }
            set { SetProperty(IsFullDesignerProperty, value); }
        }

        public static readonly PropertyInfo<int> SpecialtyIdProperty = RegisterProperty<int>(c => c.SpecialtyId);
        [Required]
        public int SpecialtyId
        {
            get { return GetProperty(SpecialtyIdProperty); }
            set { SetProperty(SpecialtyIdProperty, value); }
        }

        public static readonly PropertyInfo<int> CustomerIdProperty = RegisterProperty<int>(c => c.CustomerId);
        [Required]
        public int CustomerId
        {
            get { return GetProperty(CustomerIdProperty); }
            set { SetProperty(CustomerIdProperty, value); }
        }

        public static readonly PropertyInfo<ITimeEntries> TimeEntriesProperty = RegisterProperty<ITimeEntries>(c => c.TimeEntries);
        public ITimeEntries TimeEntries
        {
            get { return GetProperty(TimeEntriesProperty); }
            set { SetProperty(TimeEntriesProperty, value); }
        }

        [InjectedObjectPortal]
        public IChildObjectPortal ChildObjectPortal { get; set; }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();
            BusinessRules.AddRule(new HasTimeEntriesRule());
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

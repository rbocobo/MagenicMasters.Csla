using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using Csla.Rules.CommonRules;

namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class DesignerRate : BusinessBase<DesignerRate>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<int> DesignerIdProperty = RegisterProperty<int>(c => c.DesignerId);
        [Required]
        public int DesignerId
        {
            get { return GetProperty(DesignerIdProperty); }
            set { SetProperty(DesignerIdProperty, value); }
        }

        public static readonly PropertyInfo<int> DayTypeIdProperty = RegisterProperty<int>(c => c.DayTypeId);
        [Required]
        public int DayTypeId
        {
            get { return GetProperty(DayTypeIdProperty); }
            set { SetProperty(DayTypeIdProperty, value); }
        }

        public static readonly PropertyInfo<decimal> AmountProperty = RegisterProperty<decimal>(c => c.Amount);
        [Required]
        public decimal Amount
        {
            get { return GetProperty(AmountProperty); }
            set { SetProperty(AmountProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();
            BusinessRules.AddRule(new MinValue<decimal>(AmountProperty, 0));
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

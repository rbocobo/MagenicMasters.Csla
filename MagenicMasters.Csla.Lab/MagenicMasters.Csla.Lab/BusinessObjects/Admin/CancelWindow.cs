using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using Csla.Rules.CommonRules;
namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class CancelWindow : BusinessBase<CancelWindow>
    {
        #region Business Methods

        public static readonly PropertyInfo<decimal> FeeProperty = RegisterProperty<decimal>(c => c.Fee);
        [Required]
        public decimal Fee
        {
            get { return GetProperty(FeeProperty); }
            set { SetProperty(FeeProperty, value); }
        }


        public static readonly PropertyInfo<int> NumberOfDaysProperty = RegisterProperty<int>(c => c.NumberOfDays);
        [Required]
        public int NumberOfDays
        {
            get { return GetProperty(NumberOfDaysProperty); }
            set { SetProperty(NumberOfDaysProperty, value); }
        }
        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();
            BusinessRules.AddRule(new MinValue<decimal>(FeeProperty, 1));
            BusinessRules.AddRule(new MinValue<int>(NumberOfDaysProperty, 0));
            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        public static CancelWindow NewEditableRoot()
        {
            return DataPortal.Create<CancelWindow>();
        }

        public static CancelWindow GetEditableRoot(int id)
        {
            return DataPortal.Fetch<CancelWindow>(id);
        }

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<CancelWindow>(id);
        }

        private CancelWindow()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        //[RunLocal]
        //protected override void DataPortal_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //    base.DataPortal_Create();
        //}

        //private void DataPortal_Fetch(int criteria)
        //{
        //    // TODO: load values
        //}

        //[Transactional(TransactionalTypes.TransactionScope)]
        //protected override void DataPortal_Insert()
        //{
        //    // TODO: insert values
        //}

        //[Transactional(TransactionalTypes.TransactionScope)]
        //protected override void DataPortal_Update()
        //{
        //    // TODO: update values
        //}

        //[Transactional(TransactionalTypes.TransactionScope)]
        //protected override void DataPortal_DeleteSelf()
        //{
        //    DataPortal_Delete(this.Id);
        //}

        //[Transactional(TransactionalTypes.TransactionScope)]
        //private void DataPortal_Delete(int criteria)
        //{
        //    // TODO: delete values
        //}

        #endregion
    }
}

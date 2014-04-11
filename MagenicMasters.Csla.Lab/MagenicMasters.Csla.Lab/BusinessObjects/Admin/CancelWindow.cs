using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using Csla.Rules.CommonRules;
using MagenicMasters.CslaLab.Core;
namespace MagenicMasters.CslaLab.Admin
{
    [Serializable]
    public class CancelWindow : BusinessBase<CancelWindow>
    {
        #region Business Methods


        public static readonly PropertyInfo<decimal> FeeProperty =
    PropertyInfoRegistration.Register<CancelWindow, decimal>(_ => _.Fee);
        [Required]
        public decimal Fee
        {
            get { return this.GetProperty(CancelWindow.FeeProperty); }
            set { this.SetProperty(CancelWindow.FeeProperty, value); }
        }


        public static readonly PropertyInfo<int> NumberOfDaysProperty =
    PropertyInfoRegistration.Register<CancelWindow, int>(_ => _.NumberOfDays);
        [Required]
        public int NumberOfDays
        {
            get { return this.GetProperty(CancelWindow.NumberOfDaysProperty); }
            set { this.SetProperty(CancelWindow.NumberOfDaysProperty, value); }
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

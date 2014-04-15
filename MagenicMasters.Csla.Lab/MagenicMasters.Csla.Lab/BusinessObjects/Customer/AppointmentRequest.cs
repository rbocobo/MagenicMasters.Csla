using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using MagenicMasters.CslaLab;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Contracts.Customer;

namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class AppointmentRequest : BusinessBaseScopeCore<AppointmentRequest>, IAppointmentRequest
    {
        #region Business Methods


        public static readonly PropertyInfo<int> IdProperty =
    PropertyInfoRegistration.Register<AppointmentRequest, int>(_ => _.Id);
        public int Id
        {
            get { return this.GetProperty(AppointmentRequest.IdProperty); }
            set { this.SetProperty(AppointmentRequest.IdProperty, value); }
        }


        public static readonly PropertyInfo<bool> IsFullDesignerProperty =
    PropertyInfoRegistration.Register<AppointmentRequest, bool>(_ => _.IsFullDesigner);
        [Required]
        public bool IsFullDesigner
        {
            get { return this.GetProperty(AppointmentRequest.IsFullDesignerProperty); }
            set { this.SetProperty(AppointmentRequest.IsFullDesignerProperty, value); }
        }


        public static readonly PropertyInfo<int> SpecialtyIdProperty =
    PropertyInfoRegistration.Register<AppointmentRequest, int>(_ => _.SpecialtyId);
        [Required]
        public int SpecialtyId
        {
            get { return this.GetProperty(AppointmentRequest.SpecialtyIdProperty); }
            set { this.SetProperty(AppointmentRequest.SpecialtyIdProperty, value); }
        }

        public static readonly PropertyInfo<int> CustomerIdProperty =
    PropertyInfoRegistration.Register<AppointmentRequest, int>(_ => _.CustomerId);
        [Required]
        public int CustomerId
        {
            get { return this.GetProperty(AppointmentRequest.CustomerIdProperty); }
            set { this.SetProperty(AppointmentRequest.CustomerIdProperty, value); }
        }

        public static readonly PropertyInfo<ITimeEntries> TimeEntriesProperty =
    PropertyInfoRegistration.Register<AppointmentRequest, ITimeEntries>(_ => _.TimeEntries);
        public ITimeEntries TimeEntries
        {
            get { return this.GetProperty(AppointmentRequest.TimeEntriesProperty); }
            set { this.SetProperty(AppointmentRequest.TimeEntriesProperty, value); }
        }

        [NonSerialized]
        private IChildObjectPortal childObjectPortal;
        [Dependency]
        public IChildObjectPortal ChildObjectPortal
        {
            get { return this.childObjectPortal; }
            set { this.childObjectPortal = value; }
        }

        [NonSerialized]
        private IObjectPortal<AppointmentRequest> objectPortal;
        [Dependency]
        public IObjectPortal<AppointmentRequest> ObjectPortal
        {
            get { return this.objectPortal; }
            set { this.objectPortal = value; }
        }

        #endregion

        #region Factory Methods



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
            base.DataPortal_Create();
            //LoadProperty(TimeEntriesProperty, DataPortal.Create<TimeEntryCollection>());
        }

        #endregion

        //[Dependency]
        //public DataAccess.RepositoryContracts.ICustomerRepository CustomerRepository
        //{
        //    get;
        //    set;
        //}
        //[Dependency]
        //public DataAccess.RepositoryContracts.IDesignerRepository DesignerRepository
        //{
        //    get;
        //    set;
        //}
        //[Dependency]
        //public DataAccess.RepositoryContracts.IAppointmentRepository AppointmentRepository
        //{
        //    get;
        //    set;
        //}
    }
}

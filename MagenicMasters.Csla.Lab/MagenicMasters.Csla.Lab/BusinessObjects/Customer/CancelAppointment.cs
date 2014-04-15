using System;
using Csla;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Contracts.Customer;
namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class CancelAppointment : CommandBaseScopeCore<CancelAppointment>, ICancelAppointment
    {

        public static readonly PropertyInfo<int> AppointmentIdProperty =
        PropertyInfoRegistration.Register<CancelAppointment, int>(_ => _.AppointmentId);
        public int AppointmentId
        {
            get { return this.ReadProperty(CancelAppointment.AppointmentIdProperty); }
            private set { this.LoadProperty(CancelAppointment.AppointmentIdProperty, value); }
        }


        public static readonly PropertyInfo<decimal> ChargesProperty =
        PropertyInfoRegistration.Register<CancelAppointment, decimal>(_ => _.Charges);
        public decimal Charges
        {
            get { return this.ReadProperty(CancelAppointment.ChargesProperty); }
            private set { this.LoadProperty(CancelAppointment.ChargesProperty, value); }
        }

        [NonSerialized]
        private IAppointmentRepository appointmentRepository;
        [Dependency]
        public IAppointmentRepository AppointmentRepository
        {
            get { return appointmentRepository; }
            set { appointmentRepository = value; }
        }

        [NonSerialized]
        private IObjectPortal<ICancelAppointment> objectPortal;
        [Dependency]
        public IObjectPortal<ICancelAppointment> ObjectPortal
        {
            get { return objectPortal; }
            set { objectPortal = value; }
        }

        #region Authorization Methods

        public static bool CanExecuteCommand()
        {
            // TODO: customize to check user role
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        #endregion

        protected void DataPortal_Create(int appointmentId)
        {
            this.LoadProperty(AppointmentIdProperty, appointmentId);
        }

        protected override void DataPortal_Execute()
        {
            var charges = this.AppointmentRepository.CancelAppointment(this.AppointmentId); 
            this.LoadProperty(ChargesProperty, charges);
        }       
    }
}

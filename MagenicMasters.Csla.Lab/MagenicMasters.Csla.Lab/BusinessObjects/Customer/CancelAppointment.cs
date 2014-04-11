using System;
using Csla;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.Core;
namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class CancelAppointment : CommandBase<CancelAppointment>
    {

        public CancelAppointment(int appointmentId)
        {
            this.LoadProperty(AppointmentIdProperty, appointmentId);
        }


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

        [Dependency]
        public IAppointmentRepository AppointmentRepository { get; set; }

        [Dependency]
        public IObjectPortal<CancelAppointment> ObjectPortal { get; set; }

        #region Authorization Methods

        public static bool CanExecuteCommand()
        {
            // TODO: customize to check user role
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        #endregion

        protected override void DataPortal_Execute()
        {
            var charges = this.AppointmentRepository.CancelAppointment(this.AppointmentId); 
            this.LoadProperty(ChargesProperty, charges);
        }       
    }
}

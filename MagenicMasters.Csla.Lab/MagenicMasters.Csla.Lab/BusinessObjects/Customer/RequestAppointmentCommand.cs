using Csla;
using Csla.Core;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Contracts.Customer;
using MagenicMasters.CslaLab.Core;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.CustomAttributes;
using MagenicMasters.CslaLab.Customer;
using MagenicMasters.CslaLab.DataAccess;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MagenicMasters.CslaLab.Customer
{
    [Serializable]
    public class RequestAppointmentCommand : CommandBaseScopeCore<RequestAppointmentCommand>, IRequestAppointmentCommand
    {

        #region Properties

  
        public static readonly PropertyInfo<IAppointmentRequest> AppointmentRequestProperty =
        PropertyInfoRegistration.Register<RequestAppointmentCommand, IAppointmentRequest>(_ => _.AppointmentRequest);
        public IAppointmentRequest AppointmentRequest
        {
            get { return this.ReadProperty(RequestAppointmentCommand.AppointmentRequestProperty); }
            private set { this.LoadProperty(RequestAppointmentCommand.AppointmentRequestProperty, value); }
        }

        public static readonly PropertyInfo<IAppointmentResultView> AppointmentRequestResultProperty =
        PropertyInfoRegistration.Register<RequestAppointmentCommand, IAppointmentResultView>(_ => _.AppointmentRequestResult);
        public IAppointmentResultView AppointmentRequestResult
        {
            get { return this.ReadProperty(RequestAppointmentCommand.AppointmentRequestResultProperty); }
            private set { this.LoadProperty(RequestAppointmentCommand.AppointmentRequestResultProperty, value); }
        }

        [NonSerialized]
        private IAppointmentRepository appointmentRepository;
        [Dependency]
        public IAppointmentRepository AppointmentRepository
        {
            get { return this.appointmentRepository; }
            set { this.appointmentRepository = value; }
        }

        [NonSerialized]
        private IObjectPortal<RequestAppointmentCommand> objectPortal;
        [Dependency]
        public IObjectPortal<RequestAppointmentCommand> ObjectPortal
        {
            get { return this.objectPortal; }
            set { this.objectPortal = value; }
        }

        #endregion

        #region  Methods


        #endregion

        #region Authorization Methods

        public static bool CanExecuteCommand()
        {
            // TODO: customize to check user role
            //return Csla.ApplicationContext.User.IsInRole("Role");
            return true;
        }

        #endregion

        #region Data Access


        protected override void DataPortal_Execute()
        {

            var dateTimeRange = new List<DateTimeRange>(); //Todo: Assign from property
            var result = this.AppointmentRepository.BuildAppointment(this.AppointmentRequest.CustomerId, 
                this.AppointmentRequest.SpecialtyId, 
                this.AppointmentRequest.IsFullDesigner, 
                dateTimeRange);

            var apptResult = DataPortal.FetchChild<AppointmentResultView>(result);
            LoadProperty(AppointmentRequestResultProperty, apptResult);
        }


        protected void DataPortal_Create(IAppointmentRequest criteria)
        {
            LoadProperty(AppointmentRequestProperty, criteria);
        }

        #endregion

    }
}

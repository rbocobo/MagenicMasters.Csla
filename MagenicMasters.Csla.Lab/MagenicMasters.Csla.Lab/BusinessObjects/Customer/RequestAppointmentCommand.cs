using Csla;
using Csla.Core;
using MagenicMasters.CslaLab.Contracts;
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
    public class RequestAppoinmentCommand : CommandBaseScopeCore<RequestAppoinmentCommand>, IRequestAppointmentCommand
    {

        #region Properties

  
        public static readonly PropertyInfo<IAppointmentRequest> AppointmentRequestProperty =
        PropertyInfoRegistration.Register<RequestAppoinmentCommand, IAppointmentRequest>(_ => _.AppointmentRequest);
        public IAppointmentRequest AppointmentRequest
        {
            get { return this.ReadProperty(RequestAppoinmentCommand.AppointmentRequestProperty); }
            private set { this.LoadProperty(RequestAppoinmentCommand.AppointmentRequestProperty, value); }
        }

        public static readonly PropertyInfo<IAppointmentResultView> AppointmentRequestResultProperty =
        PropertyInfoRegistration.Register<RequestAppoinmentCommand, IAppointmentResultView>(_ => _.AppointmentRequestResult);
        public IAppointmentResultView AppointmentRequestResult
        {
            get { return this.ReadProperty(RequestAppoinmentCommand.AppointmentRequestResultProperty); }
            private set { this.LoadProperty(RequestAppoinmentCommand.AppointmentRequestResultProperty, value); }
        }

        [Dependency]
        public IAppointmentRepository AppointmentRepository
        { get; set; }

        [Dependency]
        public IObjectPortal<RequestAppoinmentCommand> ObjectPortal { get; set; }

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

            this.AppointmentRepository.BuildAppointment(this.AppointmentRequest.CustomerId, 
                this.AppointmentRequest.SpecialtyId, 
                this.AppointmentRequest.IsFullDesigner, 
                this.AppointmentRequest
                    .TimeEntries
                    .Select(_ => new DateTimeRange() 
                        { 
                            StartDateTime = _.StartDateTime, 
                            EndDateTime = _.EndDateTime }).ToList());
        }

        #endregion
    }
}

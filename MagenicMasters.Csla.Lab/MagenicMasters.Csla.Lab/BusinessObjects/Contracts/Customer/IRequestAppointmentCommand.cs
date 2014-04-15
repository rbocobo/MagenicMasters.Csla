using MagenicMasters.Csla.Lab.Core.Contracts;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Contracts.Customer;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts.Customer
{
    public interface IRequestAppointmentCommand : ICommandBaseCore
    {
        IAppointmentRequest AppointmentRequest {get;}
        IAppointmentResultView AppointmentRequestResult { get;  }
        IAppointmentRepository AppointmentRepository { get; set; }
    }
}

using MagenicMasters.CslaLab.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts
{
    public interface IRequestAppointmentCommand
    {
        IAppointmentRequest AppointmentRequest {get;}
        IAppointmentResultView AppointmentRequestResult { get;  }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagenicMasters.Csla.Lab.BusinessContracts
{
    public interface IAppointmentRequest
    {
        bool IsFullDesigner { get;set; }
        int SpecialtyId { get; set; }
        int CustomerId { get; set; }
        ITimeEntries TimeEntries { get; set; }
    }
}

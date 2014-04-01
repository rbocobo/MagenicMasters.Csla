using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagenicMasters.Csla.Lab.Contracts
{
    public interface IAppointmentRequest : IBusinessBase
    {
        bool IsFullDesigner { get;set; }
        int SpecialtyId { get; set; }
        int CustomerId { get; set; }
        ITimeEntries TimeEntries { get; set; }
    }
}

using Csla;
using MagenicMasters.CslaLab.Contracts.Customer;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagenicMasters.CslaLab.Contracts.Customer
{
    public interface IAppointmentRequest : IBusinessBase
    {
        bool IsFullDesigner { get;set; }
        int SpecialtyId { get; set; }
        int CustomerId { get; set; }
        ITimeEntries TimeEntries { get; set; }
        //ICustomerRepository CustomerRepository { get; set; }
        //IDesignerRepository DesignerRepository { get; set; }
        //IAppointmentRepository AppointmentRepository { get; set; }
    }
}

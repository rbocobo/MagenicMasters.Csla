using MagenicMasters.Csla.Lab.Core.Contracts;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.BusinessObjects.Contracts
{
    public interface ICancelAppointment : ICommandBaseCore
    {
        int AppointmentId{get;}
        decimal Charges{get;}
        IAppointmentRepository AppointmentRepository{get;set;}
        IObjectPortal<ICancelAppointment> ObjectPortal{get;set;}
    }
}

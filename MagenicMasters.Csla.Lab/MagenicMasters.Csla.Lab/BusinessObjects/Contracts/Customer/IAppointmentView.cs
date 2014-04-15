using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts.Customer
{
    public interface IAppointmentView : IReadOnlyBaseCore
    {
        int AppointmentId { get; }
        DateTime StartDateTime { get; }
        DateTime EndDateTime { get; }
        string DesignerName { get; }
        decimal Fee { get; }
    }
}

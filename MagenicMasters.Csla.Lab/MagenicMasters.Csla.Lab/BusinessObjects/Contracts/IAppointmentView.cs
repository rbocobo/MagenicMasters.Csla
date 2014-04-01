using MagenicMasters.CslaLab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.BusinessObjects.Contracts
{
    public interface IAppointmentView : IReadOnlyBaseCore
    {
        string CustomerName { get; set; }
        string Specialty { get; set; }
        DateTime Date { get; set; }
    }
}

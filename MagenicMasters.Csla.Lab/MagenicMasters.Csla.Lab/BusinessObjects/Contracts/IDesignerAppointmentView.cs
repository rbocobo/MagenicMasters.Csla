using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts
{
    public interface IDesignerAppointmentView : IReadOnlyBaseCore
    {
        string CustomerName { get; }
        string Specialty { get; }
        DateTime Date { get; }
    }
}

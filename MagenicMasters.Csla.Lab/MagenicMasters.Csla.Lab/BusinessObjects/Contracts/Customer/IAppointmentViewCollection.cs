using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.Csla.Lab.Core.Contracts;
namespace MagenicMasters.CslaLab.Contracts.Customer
{
    public interface IAppointmentViewCollection : IReadOnlyListBaseCore<IAppointmentView>
    {
    }
}

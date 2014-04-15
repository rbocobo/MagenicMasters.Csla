using MagenicMasters.CslaLab.Core.Contracts;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts.Designer
{
    public interface IAppointmentViewCollection : IReadOnlyListBaseCore<IAppointmentView>
    {
        IAppointmentRepository AppointmentRepository { get; set; }
    }
}

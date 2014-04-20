using Csla;
using Csla.Core;
using MagenicMasters.CslaLab.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts.Designer
{
    public interface IDayScheduleOverride : IBusinessBase
    {
        int Id { get; set; }
        int WeekScheduleId { get; set; }
        DateTime Date { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        IScheduleRepository ScheduleRepository { get; set; }
    }
}

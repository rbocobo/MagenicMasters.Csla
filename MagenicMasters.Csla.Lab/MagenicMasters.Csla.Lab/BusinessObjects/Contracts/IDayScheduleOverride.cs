using Csla;
using Csla.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Contracts
{
    public interface IDayScheduleOverride : IBusinessBase
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }

    }
}

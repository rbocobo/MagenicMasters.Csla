using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.Contracts
{
    public interface IHasTimeEntries
    {
        ITimeEntries TimeEntries { get; }
    }
}

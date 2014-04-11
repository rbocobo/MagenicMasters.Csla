using Csla;
using MagenicMasters.Csla.Lab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    [Serializable]
    public abstract class CommandBaseCore<T>
        :CommandBase<T>, ICommandBaseCore
        where T: CommandBaseCore<T>
    {
    }
}

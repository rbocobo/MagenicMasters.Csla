using Csla;
using MagenicMasters.CslaLab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    [Serializable]
    public abstract class ReadOnlyBaseCore<T>
        :ReadOnlyBase<T>, IReadOnlyBaseCore
        where T : ReadOnlyBaseCore<T>
    {
    }
}

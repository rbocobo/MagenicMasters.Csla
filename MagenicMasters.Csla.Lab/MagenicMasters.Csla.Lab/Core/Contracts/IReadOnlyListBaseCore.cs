using Csla;
using MagenicMasters.CslaLab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core.Contracts
{
    public interface IReadOnlyListBaseCore<T> : IReadOnlyListBase<T>
        where T: IReadOnlyBaseCore
    {
    }
}

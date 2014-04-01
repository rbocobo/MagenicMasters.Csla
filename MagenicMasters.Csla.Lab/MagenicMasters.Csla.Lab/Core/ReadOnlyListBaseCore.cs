using Csla;
using Csla.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    public class ReadOnlyListBaseCore<T,C> : ReadOnlyListBase<T,C>, IReadOnlyListBase<C>
        where T: ReadOnlyListBaseCore<T,C>
        where C : IReadOnlyObject
    {
    }
}

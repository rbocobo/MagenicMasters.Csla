using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.Core.Contracts
{
    public interface IChildObjectPortal
    {
        T CreateChild<T>();
        T CreateChild<T>(params object[] parameters);
        T FetchChild<T>();
        T FetchChild<T>(params object[] parameters);
    }
}

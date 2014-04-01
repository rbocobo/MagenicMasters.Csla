using Csla;
using MagenicMasters.CslaLab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core
{
    public sealed class ChildObjectPortal : IChildObjectPortal
    {
        public T CreateChild<T>()
        {
            return DataPortal.CreateChild<T>();
        }

        public T CreateChild<T>(params object[] parameters)
        {
            return DataPortal.CreateChild<T>(parameters);
        }

        public T FetchChild<T>()
        {
            return DataPortal.FetchChild<T>();
        }

        public T FetchChild<T>(params object[] parameters)
        {
            return DataPortal.FetchChild<T>(parameters);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.Csla.Lab.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public sealed class InjectedObjectPortalAttribute : Attribute
    {
    }
}

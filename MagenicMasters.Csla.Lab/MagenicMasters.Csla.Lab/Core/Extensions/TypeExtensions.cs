using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Core.Extensions
{
    public static class TypeExtensions 
    {
        public static Type GetConcreteType(this Type @this) 
        { if (@this == null || !@this.IsInterface || string.IsNullOrWhiteSpace(@this.Namespace)) 
        { return @this; } 
            var concreteTypeName = string.Concat( @this.Namespace.Replace(".Contracts", string.Empty), ".", @this.Name.Substring(1), ", ", @this.Assembly.FullName); 
            var typ = Type.GetType(concreteTypeName);

            if(typ == null)
            {
                //Resolve Business Object's Namespace
                var domainList = new List<string> { ".Admin", ".Customer", ".Designer" };
                foreach(var d in domainList)
                {
                    concreteTypeName = string.Concat(@this.Namespace.Replace(".Contracts", d).Replace("CslaLab", "CslaLab"), ".", @this.Name.Substring(1), ", ", @this.Assembly.FullName);
                    typ = Type.GetType(concreteTypeName);
                    if (typ != null)
                        return typ;
                }
            }
            return typ;
        } 
    } 
    //See more at: http://magenic.com/Blog/AbstractionsinCSLA#sthash.y62aH8xs.dpuf
}

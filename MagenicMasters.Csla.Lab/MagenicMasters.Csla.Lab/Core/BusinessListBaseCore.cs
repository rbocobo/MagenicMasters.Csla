using MagenicMasters.Csla.Lab.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Core;
namespace MagenicMasters.Csla.Lab.Core
{
    [Serializable]
    public class BusinessListBaseCore<T,C> : BusinessListBase<T,C>, IBusinessListBase<C>
        where T: BusinessListBaseCore<T,C>
        where C : IEditableBusinessObject
    {
    }
}

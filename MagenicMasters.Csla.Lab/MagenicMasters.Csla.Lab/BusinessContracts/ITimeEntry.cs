using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
namespace MagenicMasters.Csla.Lab.BusinessContracts
{
    public interface ITimeEntry : IBusinessBase
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
    }
}

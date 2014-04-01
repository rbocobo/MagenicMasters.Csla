using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using MagenicMasters.Csla.Lab.Core.Contracts;
namespace MagenicMasters.Csla.Lab.Contracts
{
    public interface ITimeEntry : IBusinessBaseCore
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
    }
}

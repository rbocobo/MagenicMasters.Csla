using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using MagenicMasters.CslaLab.Core.Contracts;
namespace MagenicMasters.CslaLab.Contracts
{
    public interface ITimeEntry : IBusinessBaseCore
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
    }
}

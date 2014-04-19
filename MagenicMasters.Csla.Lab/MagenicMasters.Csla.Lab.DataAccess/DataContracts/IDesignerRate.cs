using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface IDesignerRate
    {
        int Id { get; set; }
        int DesignerId { get; set; }
        decimal Rate { get; set; }
    }
}

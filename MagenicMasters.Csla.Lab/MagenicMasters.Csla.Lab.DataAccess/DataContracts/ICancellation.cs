using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface ICancellation
    {
        int Id { get; set; }
        int Window { get; set; }
        decimal Fee { get; set; }
    }
}

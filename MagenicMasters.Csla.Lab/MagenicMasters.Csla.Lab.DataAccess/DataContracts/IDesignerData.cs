using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface IDesignerData
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsFull { get; set; }
    }
}

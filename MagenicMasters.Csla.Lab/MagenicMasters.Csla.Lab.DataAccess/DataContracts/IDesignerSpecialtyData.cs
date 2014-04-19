using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface IDesignerSpecialtyData
    {
        int Id { get; set; }
        int DesignerId { get; set; }
        int SpecialtyId { get; set; }
        //IDesignerData Designer { get; set; }
        //ISpecialtyData Specialty { get; set; }
    }
}

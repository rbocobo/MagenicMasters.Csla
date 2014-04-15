using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.DataAccess.DataContracts
{
    public interface IAppointmentData
    {
        int Id { get; set; }
        int CustomerId { get; set; }
        int DesignerId { get; set; }
        int SpecialtyId { get; set; }
        DateTime DateTime { get; set; }
        decimal Fee { get; set; }
        decimal PartialFee { get; set; }
        int CancelWindow { get; set; }
        int Status { get; set; }
    }
}

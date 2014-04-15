using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.Csla.Lab.EF.Models
{
    public partial class Appointment : IAppointmentData
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DesignerId { get; set; }
        public int SpecialtyId { get; set; }
        public System.DateTime DateTime { get; set; }
        public decimal Fee { get; set; }
        public decimal PartialFee { get; set; }
        public int CancelWindow { get; set; }
        public int Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Designer Designer { get; set; }
    }
}

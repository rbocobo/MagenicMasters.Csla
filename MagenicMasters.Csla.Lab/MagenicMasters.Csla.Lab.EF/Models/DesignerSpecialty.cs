using System;
using System.Collections.Generic;

namespace MagenicMasters.Csla.Lab.EF.Models
{
    public partial class DesignerSpecialty
    {
        public int Id { get; set; }
        public int DesignerId { get; set; }
        public int SpecialtyId { get; set; }
        public virtual Designer Designer { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}

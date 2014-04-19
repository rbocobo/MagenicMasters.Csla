using System;
using System.Collections.Generic;
using MagenicMasters.CslaLab.DataAccess.DataContracts;
namespace MagenicMasters.CslaLab.DataAccess.Models
{
    public partial class DesignerSpecialty: IDesignerSpecialtyData
    {
        public int Id { get; set; }
        public int DesignerId { get; set; }
        public int SpecialtyId { get; set; }
        public virtual Designer Designer { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}

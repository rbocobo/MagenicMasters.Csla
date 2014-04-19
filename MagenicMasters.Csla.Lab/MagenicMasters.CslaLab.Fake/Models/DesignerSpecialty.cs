using System;
using System.Collections.Generic;
using MagenicMasters.CslaLab.DataAccess.DataContracts;

namespace MagenicMasters.CslaLab.Fake.Models
{
    public partial class DesignerSpecialty: IDesignerSpecialtyData
    {
        public int Id { get; set; }
        public int DesignerId { get; set; }
        public int SpecialtyId { get; set; }
        public  Designer Designer { get; set; }
        public  Specialty Specialty { get; set; }
    }
}

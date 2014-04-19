using System;
using System.Collections.Generic;
using MagenicMasters.CslaLab.DataAccess.DataContracts;

namespace MagenicMasters.CslaLab.DataAccess.Models
{
    public partial class Specialty : ISpecialtyData
    {
        public Specialty()
        {
            this.DesignerSpecialties = new List<DesignerSpecialty>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DesignerSpecialty> DesignerSpecialties { get; set; }
    }
}

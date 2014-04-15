using System;
using System.Collections.Generic;

namespace MagenicMasters.Csla.Lab.EF.Models
{
    public partial class Specialty
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

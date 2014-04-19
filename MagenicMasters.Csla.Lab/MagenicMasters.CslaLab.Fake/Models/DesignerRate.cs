using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.CslaLab.Fake.Models
{
    public partial class DesignerRate : IDesignerRate
    {

        public int Id { get; set; }
        public int DesignerId { get; set; }
        public decimal Rate { get; set; }
        public  Designer Designer { get; set; }
    }
}

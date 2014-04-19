using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.CslaLab.Fake.Models
{
    public partial class Cancellation : ICancellation 
    {
        public int Id { get; set; }
        public int Window { get; set; }
        public decimal Fee { get; set; }
    }
}

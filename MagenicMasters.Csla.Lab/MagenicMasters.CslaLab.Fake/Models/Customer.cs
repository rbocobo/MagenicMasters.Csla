using MagenicMasters.CslaLab.DataAccess.DataContracts;
using System;
using System.Collections.Generic;

namespace MagenicMasters.CslaLab.Fake.Models
{
    public partial class Customer : ICustomerData
    {
        public Customer()
        {
            this.Appointments = new List<Appointment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public  ICollection<Appointment> Appointments { get; set; }
    }
}

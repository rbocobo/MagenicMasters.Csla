using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Criteria
{
    public class CustomerAppointmentViewCriteria : CriteriaBase<CustomerAppointmentViewCriteria>
    {

        public CustomerAppointmentViewCriteria(string custName, string specialty, DateTime date)
        {
            LoadProperty(CustomerNameProperty, custName);
            LoadProperty(SpecialtyProperty, specialty);
            LoadProperty(DateProperty, date);
        }

        public static readonly PropertyInfo<string> CustomerNameProperty = RegisterProperty<string>(c => c.CustomerName);
        public string CustomerName
        {
            get { return ReadProperty(CustomerNameProperty); }
            private set { LoadProperty(CustomerNameProperty, value); }
        }

        public static readonly PropertyInfo<string> SpecialtyProperty = RegisterProperty<string>(c => c.Specialty);
        public string Specialty
        {
            get { return ReadProperty(SpecialtyProperty); }
            private set { LoadProperty(SpecialtyProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> DateProperty = RegisterProperty<DateTime>(c => c.Date);
        public DateTime Date
        {
            get { return ReadProperty(DateProperty); }
            private set { LoadProperty(DateProperty, value); }
        }
    }
}

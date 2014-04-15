using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.Core;
namespace MagenicMasters.CslaLab.Criteria
{
    public class AppointmentViewCriteria : CriteriaBase<AppointmentViewCriteria>
    {

        public  AppointmentViewCriteria(int appointmentId, DateTime startDateTime, DateTime endDateTime, string name, decimal fee )
        {
            LoadProperty(AppointmentIdProperty, appointmentId);
            LoadProperty(StartDateTimeProperty, startDateTime);
            LoadProperty(EndDateTimeProperty, endDateTime);
            LoadProperty(NameProperty, name);
            LoadProperty(FeeProperty, fee);
        }

        public static readonly PropertyInfo<int> AppointmentIdProperty =
    PropertyInfoRegistration.Register<AppointmentViewCriteria, int>(_ => _.AppointmentId);
        public int AppointmentId
        {
            get { return this.ReadProperty(AppointmentViewCriteria.AppointmentIdProperty); }
            private set { this.LoadProperty(AppointmentViewCriteria.AppointmentIdProperty, value); }
        }


        public static readonly PropertyInfo<DateTime> StartDateTimeProperty =
    PropertyInfoRegistration.Register<AppointmentViewCriteria, DateTime>(_ => _.StartDateTime);
        public DateTime StartDateTime
        {
            get { return this.ReadProperty(AppointmentViewCriteria.StartDateTimeProperty); }
            private set { this.LoadProperty(AppointmentViewCriteria.StartDateTimeProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndDateTimeProperty =
    PropertyInfoRegistration.Register<AppointmentViewCriteria, DateTime>(_ => _.EndDateTime);
        public DateTime EndDateTime
        {
            get { return this.ReadProperty(AppointmentViewCriteria.EndDateTimeProperty); }
            private set { this.LoadProperty(AppointmentViewCriteria.EndDateTimeProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty =
    PropertyInfoRegistration.Register<AppointmentViewCriteria, string>(_ => _.Name);
        public string Name
        {
            get { return this.ReadProperty(AppointmentViewCriteria.NameProperty); }
            private set { this.LoadProperty(AppointmentViewCriteria.NameProperty, value); }
        }

        public static readonly PropertyInfo<decimal> FeeProperty =
    PropertyInfoRegistration.Register<AppointmentViewCriteria, decimal>(_ => _.Fee);
        public decimal Fee
        {
            get { return this.ReadProperty(AppointmentViewCriteria.FeeProperty); }
            private set { this.LoadProperty(AppointmentViewCriteria.FeeProperty, value); }
        }
        
    }
}

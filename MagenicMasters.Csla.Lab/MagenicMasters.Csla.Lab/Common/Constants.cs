using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagenicMasters.CslaLab.Common
{
    public static class ValidationMessages
    {
        public static readonly string StartTimePropertyArgumentInvalid = "Start Time Is Invalid";
        public static readonly string EndTimePropertyArgumentInvalid = "End Time Is Invalid";
        public static readonly string PropertyIsNotOfTypeDatetime = "Property is not of type Datetime";
        public static readonly string PrimaryPropertyInvalid  = "Property Is Invalid";
        public static readonly string ArgumentExceptionIsExpected = "ArgumentException is expected";
        public static readonly string PropertyValueNotSet = "Property Value Not Set";

        public static readonly string NoTimeEntries = "Request has no time entries";
        public static readonly string NotOnTopOfAnHour = "{0} isn't on top of the hour";
        public static readonly string MinHourInvalid = "Minimum of one (1) hour difference is required";
        public static readonly string InnerRuleNotSupplied = "Inner Rule Not Supplied";
        public static readonly string InvalidTimeRange = "Start Time must be earlier than End Time";

        public static string FormatMessage(this string @this, params object[] args)
        {
            return string.Format(@this, args);
        }
    }
}

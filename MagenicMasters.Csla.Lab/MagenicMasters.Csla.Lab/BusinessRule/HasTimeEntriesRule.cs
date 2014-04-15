using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Core;
using Csla.Rules;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Common;
using MagenicMasters.CslaLab.Contracts.Customer;
namespace MagenicMasters.CslaLab
{
    public class HasTimeEntriesRule : BusinessRule
    {
        protected override void Execute(RuleContext context)
        {
            if (((IAppointmentRequest)context.Target).TimeEntries == null || ((IAppointmentRequest)context.Target).TimeEntries.Count < 1)
            {
                context.AddErrorResult(ValidationMessages.NoTimeEntries);
            }
        }
    }
}

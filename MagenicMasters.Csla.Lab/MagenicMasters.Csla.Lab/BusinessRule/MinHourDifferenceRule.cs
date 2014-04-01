using Csla;
using Csla.Rules;
using Csla.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagenicMasters.CslaLab.Common;

namespace MagenicMasters.CslaLab
{
    public class MinHourDifferenceRule : BusinessRule
    {
        private IPropertyInfo startPropertyInfo;
        private IPropertyInfo endPropertyInfo;

        public MinHourDifferenceRule(IPropertyInfo startTimeProperty, IPropertyInfo endTimeProperty)
            : base(startTimeProperty)
        {
            startPropertyInfo = startTimeProperty;
            endPropertyInfo = endTimeProperty;

            InputProperties.Add(startPropertyInfo);
            InputProperties.Add(endPropertyInfo);

            AffectedProperties.Add(endPropertyInfo);
        }

        protected override void Execute(RuleContext context)
        {
            DateTime startTime = (DateTime)context.InputPropertyValues[startPropertyInfo];
            DateTime endTime = (DateTime)context.InputPropertyValues[endPropertyInfo];

            if ((endTime - startTime).Hours <= 0)
            {
                context.AddErrorResult(ValidationMessages.MinHourInvalid);
            }
        }
    }
}

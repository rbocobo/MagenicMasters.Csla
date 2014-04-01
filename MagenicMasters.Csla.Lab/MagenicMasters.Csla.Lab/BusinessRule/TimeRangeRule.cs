using System;
using Csla;
using Csla.Rules;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla.Core;
using MagenicMasters.CslaLab.Common;

namespace MagenicMasters.CslaLab
{
    public class TimeRangeRule : BusinessRule
    {

        private IPropertyInfo startPropertyInfo;
        private IPropertyInfo endPropertyInfo;
        public IBusinessRule InnerTopOfHourStartRule { get; private set; }
        public IBusinessRule InnerTopOfHourEndRule { get; private set; }

        public TimeRangeRule(IPropertyInfo startTimeProperty, IPropertyInfo endTimeProperty, 
            IBusinessRule innerTopOfHourStartRule, 
            IBusinessRule innerTopOfHourEndRule ): base(startTimeProperty)
        {

            if (startTimeProperty == null)
                throw new ArgumentException(ValidationMessages.StartTimePropertyArgumentInvalid);

            if (endTimeProperty == null)
                throw new ArgumentException(ValidationMessages.EndTimePropertyArgumentInvalid);

            if (startTimeProperty.Type != typeof(DateTime))
                throw new ArgumentException(ValidationMessages.PropertyIsNotOfTypeDatetime);

            if (endTimeProperty.Type != typeof(DateTime))
                throw new ArgumentException(ValidationMessages.PropertyIsNotOfTypeDatetime);

            if (innerTopOfHourStartRule == null)
                throw new ArgumentException(ValidationMessages.InnerRuleNotSupplied);

            if (innerTopOfHourEndRule == null)
                throw new ArgumentException(ValidationMessages.InnerRuleNotSupplied);

            InnerTopOfHourStartRule = innerTopOfHourStartRule;
            InnerTopOfHourEndRule = innerTopOfHourEndRule;

            startPropertyInfo = startTimeProperty;
            endPropertyInfo = endTimeProperty;

            InputProperties.Add(startPropertyInfo);
            InputProperties.Add(endPropertyInfo);

            AffectedProperties.Add(endPropertyInfo);

            RuleUri.AddQueryParameter("topHourStartRule", System.Uri.EscapeUriString(innerTopOfHourStartRule.RuleName));
            RuleUri.AddQueryParameter("topHourEndRule", System.Uri.EscapeUriString(innerTopOfHourEndRule.RuleName));
        }

        protected override void Execute(RuleContext context)
        {

            DateTime startTime = (DateTime) context.InputPropertyValues[startPropertyInfo];
            DateTime endTime = (DateTime)context.InputPropertyValues[endPropertyInfo];

            if (DateTime.Compare(startTime, endTime) >= 0)
            {
                context.AddErrorResult(ValidationMessages.InvalidTimeRange);
            }
            else if ((endTime - startTime).Hours <= 0)
            {
                context.AddErrorResult(ValidationMessages.MinHourInvalid);
            }
            else
            {
                var chainedContext1 = context.GetChainedContext(InnerTopOfHourStartRule);
                InnerTopOfHourStartRule.Execute(chainedContext1);

                var chainedContext2 = context.GetChainedContext(InnerTopOfHourEndRule);
                InnerTopOfHourEndRule.Execute(chainedContext2);
            }

            
        }
    }
}

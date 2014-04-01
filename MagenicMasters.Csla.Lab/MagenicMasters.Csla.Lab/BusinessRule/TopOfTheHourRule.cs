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
    public class TopOfTheHourRule : BusinessRule
    {
        public TopOfTheHourRule(IPropertyInfo PrimaryProperty)
            :base(PrimaryProperty)
        {

            if(PrimaryProperty == null)
            {
                throw new ArgumentException(ValidationMessages.PrimaryPropertyInvalid);
            }

            if(!PrimaryProperty.Type.Equals(typeof(DateTime)))
                throw new ArgumentException(ValidationMessages.PropertyIsNotOfTypeDatetime);

            InputProperties.Add(PrimaryProperty);
        }

        protected override void Execute(RuleContext context)
        {

            DateTime time = (DateTime)context.InputPropertyValues[PrimaryProperty];

            if (time.Equals(DateTime.MinValue))
                throw new ArgumentException(ValidationMessages.PropertyValueNotSet);

            if(time.Minute > 0)
            {
                context.AddErrorResult(ValidationMessages.NotOnTopOfAnHour.FormatMessage(PrimaryProperty.Name));
            }
        }
    }
}

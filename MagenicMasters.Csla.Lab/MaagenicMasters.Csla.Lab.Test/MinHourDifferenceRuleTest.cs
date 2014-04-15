using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Csla.Core;
using MagenicMasters.CslaLab;
using Csla.Rules;
using System.Collections.Generic;
using MagenicMasters.CslaLab.Contracts;
using MagenicMasters.CslaLab.Contracts.Designer;

namespace MaagenicMasters.CslaLab.Test
{
    [TestClass]
    public class MinHourDifferenceRuleTest
    {
        [TestMethod]
        public void MinHourDifferenceIsAtLeastAnHour()
        {
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 0, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 10, 30, 0));

            var newRule = new MinHourDifferenceRule(startTimeProperty.Object, endTimeProperty.Object);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            ruleInterface.Execute(ruleContext);


            Assert.IsTrue(ruleContext.Results.Count > 0);

            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 8, 0, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 9, 30, 0));
            ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            ruleInterface = (IBusinessRule)newRule;

            ruleInterface.Execute(ruleContext);
            Assert.IsTrue(ruleContext.Results.Count == 0);
        }


    }
}

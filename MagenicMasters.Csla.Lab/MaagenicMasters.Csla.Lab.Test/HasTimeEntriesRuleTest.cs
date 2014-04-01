using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MagenicMasters.CslaLab.Contracts;
using Csla.Rules;
using MagenicMasters.CslaLab;
using System.Linq;
using MagenicMasters.CslaLab.Common;
namespace MaagenicMasters.CslaLab.Test
{
    [TestClass]
    public class HasTimeEntriesRuleTest
    {
        [TestMethod]
        public void FailsWhen_NoTimeEntriesSupplied()
        {
            // arrange
            var mockObject = new Mock<IAppointmentRequest>(MockBehavior.Strict);
            mockObject.Setup(_ => _.TimeEntries.Count).Returns(0);

            var rule = (IBusinessRule)new HasTimeEntriesRule();
            var ruleContext = new RuleContext(null, rule, mockObject.Object, null);

            // act
            rule.Execute(ruleContext);

            // assert
            mockObject.VerifyAll();
            Assert.IsNotNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.NoTimeEntries));
        }

        [TestMethod]
        public void PassesWhen_OneTimeEntryIsSupplied()
        {
            // arrange
            var mockObject = new Mock<IAppointmentRequest>(MockBehavior.Strict);
            mockObject.Setup(_ => _.TimeEntries.Count).Returns(1);

            var rule = (IBusinessRule)new HasTimeEntriesRule();
            var ruleContext = new RuleContext(null, rule, mockObject.Object, null);

            // act
            rule.Execute(ruleContext);

            // assert
            mockObject.VerifyAll();
            Assert.IsTrue(ruleContext.Results.Count(_ => _.Severity == RuleSeverity.Error) == 0);
        }

        [TestMethod]
        public void PassesWhen_MoreThanOneTimeEntriesAreSupplied()
        {
            // arrange
            var mockObject = new Mock<IAppointmentRequest>(MockBehavior.Strict);
            mockObject.Setup(_ => _.TimeEntries.Count).Returns(2);

            var rule = (IBusinessRule)new HasTimeEntriesRule();
            var ruleContext = new RuleContext(null, rule, mockObject.Object, null);

            // act
            rule.Execute(ruleContext);

            // assert
            mockObject.VerifyAll();
            Assert.IsTrue(ruleContext.Results.Count(_ => _.Severity == RuleSeverity.Error) == 0);
        }
    }
}

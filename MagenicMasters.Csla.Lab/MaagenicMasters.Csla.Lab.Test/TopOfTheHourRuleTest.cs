using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Csla.Core;
using MagenicMasters.CslaLab;
using Csla.Rules;
using System.Collections.Generic;
using MagenicMasters.CslaLab.Common;
using MagenicMasters.CslaLab.Contracts;
using System.Linq;
namespace MaagenicMasters.CslaLab.Test
{
    [TestClass]
    public class TopOfTheHourRuleTest
    {
       

        [TestMethod]
        public void FailsWhen_OnTopOfAnHourPrimaryPropertyIsNotSupplied()
        {
            //arrange
            var expectedException = default(ArgumentException);


            //assert
            try
            {
                new TopOfTheHourRule(null);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            //Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.PrimaryPropertyInvalid);
            Assert.IsNotNull(expectedException,  ValidationMessages.ArgumentExceptionIsExpected);
        }

        [TestMethod]
        public void FailsWhen_OnTopOfAnHourPrimaryPropertySuppliedWithInvalidType()
        {
            //arrange
            var expectedException = default(ArgumentException);
            var primaryProperty = new Mock<IPropertyInfo>();
            primaryProperty.Setup(s => s.Name).Returns("StartTime");
            primaryProperty.Setup(s => s.Type).Returns(typeof(string));


            //act
            try
            {
                new TopOfTheHourRule(primaryProperty.Object);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.PropertyIsNotOfTypeDatetime);
        }

        [TestMethod]
        public void PassesWhen_TopOfAnHourPrimaryPropertyIsSupplied()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            //act

           var rule = new TopOfTheHourRule(startTimeProperty.Object);
           
            //assert
           Assert.IsNotNull(rule);
        }

        [TestMethod]
        public void FailsWhen_OnTopOfAnHourPrimaryPropertyValueNotSet()
        {
            // arrange
            var expectedException = default(ArgumentException);
            var primaryProperty = new Mock<IPropertyInfo>();
            primaryProperty.Setup(s => s.Name).Returns("PrimaryProperty");
            primaryProperty.Setup(s => s.Type).Returns(typeof(DateTime));
            
            var wsMock = new Mock<IWorkSchedule>();
            var newRule = new TopOfTheHourRule(primaryProperty.Object);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { primaryProperty.Object, wsMock.Object.StartTime } });
            var ruleInterface = (IBusinessRule)newRule;

            
            // act
            try
            {
                ruleInterface.Execute(ruleContext);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            
            // assert
            Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.PropertyValueNotSet);
        }

        [TestMethod]
        public void PassesWhen_OnTopOfAnHourPrimaryPropertyValueIsSet()
        {
            // arrange
            var expectedException = default(ArgumentException);
            var primaryProperty = new Mock<IPropertyInfo>();
            primaryProperty.Setup(s => s.Name).Returns("PrimaryProperty");
            primaryProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty(_ => _.StartTime, new DateTime(2014, 4, 1, 1, 0, 0));
            var newRule = new TopOfTheHourRule(primaryProperty.Object);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { primaryProperty.Object, wsMock.Object.StartTime } });
            var ruleInterface = (IBusinessRule)newRule;


            // act
            try
            {
                ruleInterface.Execute(ruleContext);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }

            // assert
            Assert.IsNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
        }

        [TestMethod]
        public void FailsWhen_PrimaryPropertyValueIsNotOnTopOfAnHour()
        {
            // arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 30, 0));

            var newRule = new TopOfTheHourRule(startTimeProperty.Object);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime } });
            var ruleInterface = (IBusinessRule)newRule;

            // act
            ruleInterface.Execute(ruleContext);
            // assert
            Assert.IsNotNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.NotOnTopOfAnHour.FormatMessage(startTimeProperty.Object.Name)));
        }

        [TestMethod]
        public void PassesWhen_PrimaryPropertyValueIsOnTopOfAnHour()
        {
            // arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 0, 0));

            var newRule = new TopOfTheHourRule(startTimeProperty.Object);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime } });
            var ruleInterface = (IBusinessRule)newRule;

            // act
            ruleInterface.Execute(ruleContext);
            // assert
            Assert.IsNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.NotOnTopOfAnHour.FormatMessage(startTimeProperty.Object.Name)));
        }
    }
}

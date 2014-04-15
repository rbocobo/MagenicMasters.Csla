using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Csla;
using Csla.Core;
using MagenicMasters.CslaLab;
using Csla.Rules;
using MagenicMasters.CslaLab.Designer;
using MagenicMasters.CslaLab.Common;
using MagenicMasters.CslaLab.Contracts;
using System.Linq;
using MagenicMasters.CslaLab.Contracts.Designer;
namespace MaagenicMasters.CslaLab.Test
{
    /// <summary>
    /// Summary description for DesignerTest
    /// </summary>
    [TestClass]
    public class TimeRangeRuleTest
    {
        public TimeRangeRuleTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void PassesWhen_StartTimeIsBeforeEndTime()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014,4,1,10,0,0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 11, 0, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, {endTimeProperty.Object, wsMock.Object.EndTime} });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.InvalidTimeRange));

           
        }

        [TestMethod]
        public void FailsWhen_StartTimeIsAfterEndTime()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 0, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 9, 30, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert
            Assert.IsNotNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.InvalidTimeRange));
        }

        [TestMethod]
        public void FailsWhen_StartTimeEqualsEndTime()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 0, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 10, 0, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert
            Assert.IsNotNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.InvalidTimeRange));
        }

        [TestMethod]
        public void FailsWhen_StartTimePropertyIsNotSupplied()
        {
            //arrange
            var expectedException = default(ArgumentException);

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));
            //assert
            try
            {

                new TimeRangeRule(null, endTimeProperty.Object, null,null);
            }
            catch(ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            //Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.StartTimePropertyArgumentInvalid);
        }

        [TestMethod]
        public void PassesWhen_StartTimePropertyIsSupplied()
        {
            //arrange

            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            //act
            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);

            //asset
            Assert.IsNotNull(newRule);
        }

        [TestMethod]
        public void FailsWhen_StartTimePropertyIsInvalidType()
        {
            //arrange
            var expectedException = default(ArgumentException);
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(string));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(string));
            //assert
            try
            {
                new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, null, null);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            //Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.PropertyIsNotOfTypeDatetime);
        }

        [TestMethod]
        public void PassesWhen_StartTimePropertyIsValidType()
        {
            //arrange
            var expectedException = default(ArgumentException);
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));
            //act
            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object,innerRule1, innerRule2 );

            //assert
            Assert.IsNotNull(newRule);
        }





        [TestMethod]
        public void FailsWhen_EndTimePropertyIsNotSupplied()
        {
            //arrange
            var expectedException = default(ArgumentException);

            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("startTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));
            //assert
            try
            {

                new TimeRangeRule(startTimeProperty.Object, null,null, null);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            //Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.EndTimePropertyArgumentInvalid);
        }

        [TestMethod]
        public void PassesWhen_EndTimePropertyIsSupplied()
        {
            //arrange

            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            //act
            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);

            //asset
            Assert.IsNotNull(newRule);
        }

        [TestMethod]
        public void FailsWhen_EndTimePropertyIsInvalidType()
        {
            //arrange
            var expectedException = default(ArgumentException);
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(string));
            //assert
            try
            {
                new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, null, null);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            //Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.PropertyIsNotOfTypeDatetime);
        }

        [TestMethod]
        public void PassesWhen_EndTimePropertyIsValidType()
        {
            //arrange
            var expectedException = default(ArgumentException);
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("endTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));
            //act
            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);

            //assert
            Assert.IsNotNull(newRule);
        }

        [TestMethod]
        public void PassesWhen_InnerRuleStartTimeIsOnTopOfAnHour()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 0, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 11, 0, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.NotOnTopOfAnHour.FormatMessage(startTimeProperty.Object.Name)));
        }

        [TestMethod]
        public void FailsWhen_InnerRuleStartTimeIsNotOnTopOfAnHour()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 30, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 11, 30, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNotNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.NotOnTopOfAnHour.FormatMessage(startTimeProperty.Object.Name)));
        }


        [TestMethod]
        public void PassesWhen_InnerRuleEndTimeIsOnTopOfAnHour()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 0, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 11, 0, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.NotOnTopOfAnHour.FormatMessage(endTimeProperty.Object.Name)));
        }

        [TestMethod]
        public void FailsWhen_InnerRuleEndTimeIsNotOnTopOfAnHour()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 00, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 11, 30, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNotNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.NotOnTopOfAnHour.FormatMessage(endTimeProperty.Object.Name)));
        }

        [TestMethod]
        public void FailsWhen_WindowIsLessThanAnHour()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 00, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 10, 30, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNotNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.MinHourInvalid));
        }

        [TestMethod]
        public void PassesWhen_WindowIsExactlyAnHour()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 10, 00, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 11, 0, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.MinHourInvalid));
        }

        [TestMethod]
        public void PassesWhen_WindowIsMoreThanAnHour()
        {
            //arrange
            var startTimeProperty = new Mock<IPropertyInfo>();
            startTimeProperty.Setup(s => s.Name).Returns("StartTime");
            startTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("EndTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));


            var wsMock = new Mock<IWorkSchedule>();
            wsMock.SetupProperty<DateTime>(ws => ws.StartTime, new DateTime(2014, 4, 1, 9, 00, 0));
            wsMock.SetupProperty<DateTime>(ws => ws.EndTime, new DateTime(2014, 4, 1, 11, 0, 0));

            var innerRule1 = new TopOfTheHourRule(startTimeProperty.Object);
            var innerRule2 = new TopOfTheHourRule(endTimeProperty.Object);
            var newRule = new TimeRangeRule(startTimeProperty.Object, endTimeProperty.Object, innerRule1, innerRule2);
            var ruleContext = new RuleContext(null, newRule, wsMock.Object, new Dictionary<IPropertyInfo, object>() { { startTimeProperty.Object, wsMock.Object.StartTime }, { endTimeProperty.Object, wsMock.Object.EndTime } });
            var ruleInterface = (IBusinessRule)newRule;

            //act
            ruleInterface.Execute(ruleContext);

            //assert

            Assert.IsNull(ruleContext.Results.SingleOrDefault(_ => _.Description == ValidationMessages.MinHourInvalid));
        }

        [TestMethod]
        public void FailsWhen_StartTimeTopOfHourInnerRuleIsNotSupplied()
        {
            //arrange
            var expectedException = default(ArgumentException);

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("StartTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var StartTimeProperty = new Mock<IPropertyInfo>();
            StartTimeProperty.Setup(s => s.Name).Returns("EndTime");
            StartTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));
            //assert
            try
            {

                new TimeRangeRule(StartTimeProperty.Object, endTimeProperty.Object, null, new TopOfTheHourRule(endTimeProperty.Object));
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            //Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.InnerRuleNotSupplied);
        }

        [TestMethod]
        public void FailsWhen_EndTimeTopOfHourInnerRuleIsNotSupplied()
        {
            //arrange
            var expectedException = default(ArgumentException);

            var endTimeProperty = new Mock<IPropertyInfo>();
            endTimeProperty.Setup(s => s.Name).Returns("StartTime");
            endTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));

            var StartTimeProperty = new Mock<IPropertyInfo>();
            StartTimeProperty.Setup(s => s.Name).Returns("EndTime");
            StartTimeProperty.Setup(s => s.Type).Returns(typeof(DateTime));
            //assert
            try
            {

                new TimeRangeRule(StartTimeProperty.Object, endTimeProperty.Object, new TopOfTheHourRule(StartTimeProperty.Object) ,null);
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }
            //assert
            //Assert.IsNotNull(expectedException, ValidationMessages.ArgumentExceptionIsExpected);
            Assert.AreEqual(expectedException.Message, ValidationMessages.InnerRuleNotSupplied);
        }
    }
}

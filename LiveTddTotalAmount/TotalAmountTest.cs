using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace LiveTddTotalAmount
{
    [TestClass]
    public class TotalAmountTest
    {
        private IRepository<Budget> _repository = Substitute.For<IRepository<Budget>>();
        private Accounting _accounting;

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_repository);
        }

        [TestMethod]
        public void no_budget()
        {
            GivenBudgets();
            TotalAmountShouldBe(
                new DateTime(2018, 4, 1),
                new DateTime(2018, 4, 1),
                0);
        }

        [TestMethod]
        public void one_effecttive_day_period_inside_budget_month()
        {
            GivenBudgets(
                    new Budget() { YearMonth = "201804", Amount = 30}
                );
            TotalAmountShouldBe(
                new DateTime(2018, 4, 1),
                new DateTime(2018, 4, 1),
                1);
        }

        [TestMethod]
        public void no_effecttive_day_period_befor_budget_month()
        {
            GivenBudgets(
                new Budget() { YearMonth = "201804", Amount = 30 }
            );
            TotalAmountShouldBe(
                new DateTime(2018, 3, 31),
                new DateTime(2018, 3, 31),
                0);
        }

        [TestMethod]
        public void no_effecttive_day_period_after_budget_month()
        {
            GivenBudgets(
                new Budget() { YearMonth = "201804", Amount = 30 }
            );
            TotalAmountShouldBe(
                new DateTime(2018, 5, 1),
                new DateTime(2018, 5, 1),
                0);
        }

        [TestMethod]
        public void one_effecttive_day_period_overlap_budget_month_firstday()
        {
            GivenBudgets(
                new Budget() { YearMonth = "201804", Amount = 30 }
            );
            TotalAmountShouldBe(
                new DateTime(2018, 3, 31),
                new DateTime(2018, 4, 1),
                1);
        }

        private void TotalAmountShouldBe(DateTime startDate, DateTime endDate, int excepted)
        {
            var amount = _accounting.TotalAmount(startDate, endDate);
            Assert.AreEqual(excepted, amount);
        }

        private void GivenBudgets(params Budget[] bugets)
        {
            _repository.GetAll().Returns(bugets.ToList());
        }
    }
}

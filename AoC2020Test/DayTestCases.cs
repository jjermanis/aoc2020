using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoC2020;

namespace AoC2020Test
{
    [TestClass]
    public class DayTestCases
    {
        [TestMethod]
        public void Day01()
        {
            var d = new Day01();
            Assert.AreEqual(719796, d.ExpensePairProduct());
            Assert.AreEqual(144554112, d.ExpenseTripleProduct());
        }

        [TestMethod]
        public void Day02()
        {
            var d = new Day02();
            Assert.AreEqual(643, d.ValidPasswordCount());
            Assert.AreEqual(388, d.ValidPasswordCountV2());
        }
    }
}

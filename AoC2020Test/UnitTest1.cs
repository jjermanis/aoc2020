using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoC2020;

namespace AoC2020Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Day01()
        {
            var d = new Day01();
            Assert.AreEqual(719796, d.ExpensePairProduct());
            Assert.AreEqual(144554112, d.ExpenseTripleProduct());
        }
    }
}

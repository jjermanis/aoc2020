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

        [TestMethod]
        public void Day03()
        {
            var d = new Day03();
            Assert.AreEqual(216, d.SimpleTreeCount());
            Assert.AreEqual(6708199680, d.TreeCountProduct());
        }

        [TestMethod]
        public void Day04()
        {
            var d = new Day04();
            Assert.AreEqual(226, d.SimpleValidCount());
            Assert.AreEqual(160, d.ComplexVaidCount());
        }

        [TestMethod]
        public void Day05()
        {
            var d = new Day05();
            Assert.AreEqual(959, d.MaxSeatID());
            Assert.AreEqual(527, d.MySeatID());
        }

        [TestMethod]
        public void Day06()
        {
            var d = new Day06();
            Assert.AreEqual(6457, d.QuestionsSumAnyone());
            Assert.AreEqual(3260, d.QuestionsSumEveryone());
        }

        [TestMethod]
        public void Day07()
        {
            var d = new Day07();
            Assert.AreEqual(326, d.CanContainShinyGoldBagCount());
            Assert.AreEqual(5635, d.BagsInsideShinyGoldBagCount());
        }

        [TestMethod]
        public void Day08()
        {
            var d = new Day08();
            Assert.AreEqual(1744, d.AccumulatorAfterRepeatedInstruction());
            Assert.AreEqual(1174, d.AccumulatorForTerminatingProgram());
        }
    }
}

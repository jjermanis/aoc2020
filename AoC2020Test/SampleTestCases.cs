using AoC2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2020Test
{
    [TestClass]
    public class SampleTestCases
    {
        [TestMethod]
        public void Day07()
        {
            var d = new Day07("Day07Test.txt");
            Assert.AreEqual(4, d.CanContainShinyGoldBagCount());
            Assert.AreEqual(32, d.BagsInsideShinyGoldBagCount());
        }

        [TestMethod]
        public void Day09()
        {
            var d = new Day09("Day09Test.txt");
            Assert.AreEqual(127, d.InvalidNumber(5));
            Assert.AreEqual(62, d.EncryptionWeakness(127));
        }

        [TestMethod]
        public void Day10()
        {
            var d = new Day10("Day10Test.txt");
            Assert.AreEqual(35, d.JoltageDifferenceProduct());
            Assert.AreEqual(8, d.PossibleCombinations());

            var d2 = new Day10("Day10Test2.txt");
            Assert.AreEqual(220, d2.JoltageDifferenceProduct());
            Assert.AreEqual(19208, d2.PossibleCombinations());
        }
    }
}

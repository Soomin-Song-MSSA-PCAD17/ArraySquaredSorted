using NUnit.Framework;
using static SortedArrays;
namespace Test_ArraySquareSorted
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAllPositive()
        {
            int[] inputArray = { 1, 2, 3 };
            int[] expectedOutputArray = { 1, 4, 9 };
            int[] actualOutputArray = SortedSquares(inputArray);
            Assert.That(actualOutputArray, Is.EqualTo(expectedOutputArray));
        }

        [Test]
        public void TestAllNegative()
        {
            int[] inputArray = { -4, -3, -2, -1 };
            int[] expectedOutputArray = { 1, 4, 9, 16 };
            int[] actualOutputArray = SortedSquares(inputArray);
            Assert.That(actualOutputArray, Is.EqualTo(expectedOutputArray));
        }

        [Test]
        public void TestMixedIntegers()
        {
            int[] inputArray = { -3, -2, -1, 0, 3, 5 };
            int[] expectedOutputArray = { 0, 1, 4, 9, 9, 25 };
            int[] actualOutputArray = SortedSquares(inputArray);
            Assert.That(actualOutputArray, Is.EqualTo(expectedOutputArray));
        }

        [Test]
        public void TestEmptyArray()
        {
            int[] inputArray = { };
            int[] expectedOutputArray = { };
            int[] actualOutputArray = SortedSquares(inputArray);
            Assert.That(actualOutputArray, Is.EqualTo(expectedOutputArray));
        }

        [Test]
        public void TestNullArray()
        {
            Assert.Throws<ArgumentNullException>(() => SortedSquares(null));

        }
    }
}
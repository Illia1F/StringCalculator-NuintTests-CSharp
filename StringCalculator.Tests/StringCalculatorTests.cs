using NUnit.Framework;
using System;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        [Test]
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        [TestCase("1,2,1000", 1003)]
        [TestCase("1,2,1001", 3)]
        [TestCase("1\n5", 6)]
        [TestCase("1\n5,1", 7)]
        [TestCase("1\n5,1", 7)]
        [TestCase("//[;]\n1;5;1", 7)]
        [TestCase("//[;][x]\n1;5;1", 7)]
        [TestCase("//[;][xxx]\n1;5xxx1", 7)]
        [TestCase("//[;]\n1;5;1001", 6)]
        [TestCase("//[;][#]\n1;5;1001", 6)]
        [TestCase("//[;][$$#]\n1$$#5;1001", 6)]
        [TestCase("//[;]\n1;5;1000", 1006)]
        [TestCase("//[;][^]\n1;5;1000", 1006)]
        [TestCase("//[;][%%%%]\n1;5%%%%1000", 1006)]
        [TestCase("//[;]\n1;5;10000", 6)]
        [TestCase("//[;][^]\n1;5^10000", 6)]
        [TestCase("//[;][&&&&]\n1;5&&&&10000", 6)]
        public void BasicTest(string numbers, int result)
        {
            Assert.That(StringCalculator.Add(numbers), Is.EqualTo(result));
        }

        [Test]
        [TestCase("-1,2", "-1")]
        [TestCase("1\n5,-1", "-1")]
        [TestCase("1\n-5,-1", "-5 -1")]
        [TestCase("//[;]\n1;-5;1", "-5")]
        public void TestWithNegativeNumbers(string numbers, string negativeNumbers)
        {
            var exceptionMessage = $"negatives not allowed {negativeNumbers}";

            Assert.That(() => StringCalculator.Add(numbers),
                Throws.TypeOf<ArgumentException>()
                .With
                .Message
                .EqualTo(exceptionMessage));
        }
    }
}
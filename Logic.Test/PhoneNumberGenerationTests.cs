using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using Logic;

using NUnit.Framework;

namespace Logic.Test
{
    [TestFixture]
    public class PhoneNumberGenerationTests
    {


        [Test(Description = "Phone Number Generator Should Create All Combinations Correctly")]
        public void ShouldCreateCorrectCombos()
        {
            //Arrange


            //Act
            var combos = PhoneNumberGenerator.GenerateNumbers("23").ToList();
            Assert.AreEqual(combos.Count, 16);
            Assert.IsTrue(combos.SequenceEqual(new string[] {
                "23","2D","2E","2F",
                "A3","AD","AE","AF",
                "B3","BD","BE","BF",
                "C3","CD","CE","CF"}), "Did No generate all phone numbers correctly");

        }
        [Test]
        public void ShouldCalculateCombinationsCountCorrectly()
        {
            var count = PhoneNumberGenerator.NumVariations("234");

            Assert.AreEqual(count, 64);
        }


        [Test]
        public void ShouldCalculatePageSizeCorrectly()
        {
            var pageCount = PhoneNumberGenerator.NumPages("23", 3);

            Assert.AreEqual(pageCount, 6);
        }
    }
}

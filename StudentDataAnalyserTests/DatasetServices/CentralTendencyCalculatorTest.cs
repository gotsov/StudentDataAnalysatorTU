using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentDataAnalysator.DatasetServices;
using System;
using System.Collections.Generic;

namespace StudentDataAnalyserTests
{
    [TestClass]
    public class CentralTendencyCalculatorTest

    {

        [TestMethod]
        public void TestGetMedian()
        {
            var input = new List<double>()
                    {
                         1, 2, 3, 4, 5, 6,7
                    };
            double expectedMedian = 4;

            double actualMedian = CentralTendencyCalculator.GetMedian(input);

            Assert.AreEqual(expectedMedian, actualMedian);

        }

        [TestMethod]
        public void TestGetMedian2()
        {
            var input = new List<double>()
                    {
                        1, 2, 3, 3, 3, 4, 5, 6
                    };
            double expectedMedian = 3;

            double actualMedian = CentralTendencyCalculator.GetMedian(input);

            Assert.AreEqual(expectedMedian, actualMedian);

        }

        [TestMethod]
        public void TestGetMedian3()
        {
            var input = new List<double>()
                    {
                         1, 2, 3, 4, 5, 6, 7, 8
                    };
            double expectedMedian = 4.5;

            double actualMedian = CentralTendencyCalculator.GetMedian(input);

            Assert.AreEqual(expectedMedian, actualMedian);

        }


        [TestMethod]
        public void TestGetMode()
        {
            var input = new List<double>()
                    {
                          1, 2, 3, 3, 3, 4, 4, 6
                    };
            var expectedMode = new List<double> { 3 };

            List <double> actualMode = CentralTendencyCalculator.GetMode(input);

            CollectionAssert.AreEqual(expectedMode, actualMode);

        }


        [TestMethod]
        public void TestGetMode2()
        {
            var input = new List<double>()
                    {
                           1, 2, 2, 3, 4, 5, 5, 6
                    };
            var expectedMode =  new List<double> { 2, 5};

            List<double> actualMode = CentralTendencyCalculator.GetMode(input);

            CollectionAssert.AreEqual(expectedMode, actualMode);

        }


        [TestMethod]
        public void TestGetMode3()
        {
            var input = new List<double>()
                    {
                          1, 3, 5, 10, 12, 14, 20
                    };

            CollectionAssert.AreEqual(new List<double>(), CentralTendencyCalculator.GetMode(input));

        }

        [TestMethod]
        public void TestGetAverage()
        {
            var input = new List<double>()
                    {
                          10, 2, 38, 23, 38, 23, 21
                    };

            double actualAverage = CentralTendencyCalculator.GetAverage(input);  

            Assert.AreEqual(22.14, Math.Round(actualAverage, 2));

        }

        [TestMethod]
        public void TestGetAverage1()
        {
            var input = new List<double>()
                    {
                          10, 2, 38, 23, 38, 23, 21
                    };

            double actualAverage = CentralTendencyCalculator.GetAverage(input);

            Assert.AreEqual(22.14, Math.Round(actualAverage, 2));

        }


        [TestMethod]
        public void TestGetAverage2()
        {
            var input = new List<double>()
                    {
                          15, 26, 39, 16,29
                    };

            double actualAverage = CentralTendencyCalculator.GetAverage(input);

            Assert.AreEqual(25, Math.Round(actualAverage, 2));

        }

        [TestMethod]
        public void TestGetAverage3()
        {
            var input = new List<double>()
                    {
                          6, 40, 32, 5, 12, 24 
                    };

            double actualAverage = CentralTendencyCalculator.GetAverage(input);

            Assert.AreEqual(19.83, Math.Round(actualAverage, 2));

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.DatasetServices
{
    public class StatisticalDispersionCalculator
    {
        public static int CalculateMinMaxDispersion(List<int> CoursesViewedByEachStudent)
        {
            int minDispersion, maxDispersion, minMaxDispersion;
            minDispersion = CoursesViewedByEachStudent.Min();
            maxDispersion = CoursesViewedByEachStudent.Max();
            minMaxDispersion = maxDispersion - minDispersion;
            return minMaxDispersion;
        }
        public static double CalculateVariance(List<int> CoursesViewedByEachStudent)
        {
            double averageCountOfViewedCourses;
            averageCountOfViewedCourses = CoursesViewedByEachStudent.Average();
            Console.WriteLine(averageCountOfViewedCourses);
            double sumOfSquares = 0.0;
            foreach (int num in CoursesViewedByEachStudent)
            {
                sumOfSquares += Math.Pow((num - averageCountOfViewedCourses), 2.0);
            }
            Console.WriteLine(sumOfSquares);
            return sumOfSquares / (double)(CoursesViewedByEachStudent.Count - 1);
        }
        public static double CalculateStandartDeviation(List<int> CoursesViewedByEachStudent)
        {
            return Math.Sqrt(CalculateVariance(CoursesViewedByEachStudent));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.DatasetServices
{
    public class CentralTendencyCalculator
    {
        public static double GetMedian(List<double> list)
        {
            int size = list.Count;
            int mid = size / 2;
            double median = (size % 2 != 0) ?  list[mid] : (list[mid] + list[mid - 1]) / 2;
            return median;
        }

        public static List<Double> GetMode(List<double> list)
        {
            int maxcount = list.GroupBy(i => i).Max(grp => grp.Count());
            List<double> modeList = list.GroupBy(i => i).Where(grp => grp.Count() == maxcount).Select(grp => grp.Key).ToList();
            if(maxcount == 1)
            {
                return new List<double>();
            }
            return modeList;
        }

        public static double GetAverage(List<double> list)
        {
            return list.Average(r => r);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Models
{
    public class StatisticalDispersionResult
    {
        private int minMaxDispersion;
        private double variance;
        private double standartDeviation;

        public StatisticalDispersionResult(int minMaxDispersion, double variance, double standartDeviation)
        {
            this.minMaxDispersion = minMaxDispersion;
            this.variance = variance;
            this.standartDeviation = standartDeviation;
        }

        public int MinMaxDispersion
        {
            get { return minMaxDispersion; }
            set { minMaxDispersion = value; }
        }
        public double Variance
        {
            set { variance = value; }
            get { return variance; }
        }
        public double StandartDeviation
        {
            set { standartDeviation = value; }
            get { return standartDeviation; }
        }
    }
}

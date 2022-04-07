using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Models
{
    public class CentralTendencyResult
    {
        private double median;
        private string mode;
        private double average;

        public CentralTendencyResult(double median, string mode, double average)
        {
            this.median = median;
            this.mode = mode;
            this.average = average;
        }

        public double Median
        {
            get { return median; }
            set { median = value; }
        }

        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public double Average
        {
            get { return average; }
            set { average = value; }
        }
    }
}

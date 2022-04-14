using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Models
{
    public class FrequencyDistributionResult
    {
       private string viewedCourses;
       private int frequency;
       private string relativeFrequency;

        public FrequencyDistributionResult(string viewedCourses, int frequency, string relativeFrequency)
        {
            this.viewedCourses = viewedCourses;
            this.frequency = frequency;
            this.relativeFrequency = relativeFrequency;
        }

        public string ViewedCourses
        {
            get { return viewedCourses; }
            set { viewedCourses = value; }
        }
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }  
        }
        public string RelativeFrequency
        {
            get { return relativeFrequency; }
            set { relativeFrequency = value; }
        }
    }
}

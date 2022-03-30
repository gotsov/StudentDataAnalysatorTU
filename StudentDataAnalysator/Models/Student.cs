using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Models
{
    public class Student
    {
        private double id;
        private double result;

        public Student(double id, double result)
        {
            this.id = id;
            this.result = result;
        }

        public double Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public double Result
        {
            get { return this.result; }
            set { this.result = value; }
        }
    }
}

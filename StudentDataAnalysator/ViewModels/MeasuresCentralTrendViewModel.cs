using StudentDataAnalysator.DatasetServices;
using StudentDataAnalysator.Events;
using StudentDataAnalysator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.ViewModels
{
    public class MeasuresCentralTrendViewModel : BaseViewModel
    {
        private ObservableCollection<CentralTendencyResult> tendencyResult;
        private ObservableCollection<Student> studentsList;
        private List<double> results = new List<double>();
        public MeasuresCentralTrendViewModel()
        {
            TendencyResult = new ObservableCollection<CentralTendencyResult>();

            SingletonClass.TestEventAggregator.GetEvent<GetStudentsResultsListEvent>().Subscribe(SetStudentsList);
            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Publish("");

            GetAllStudentsResults();
            CalculateCentralTendencyResult(results);
        }

        public ObservableCollection<Student> StudentsList
        {
            get
            {
                return studentsList;
            }
            set
            {
                studentsList = value;
                OnPropertyChanged("StudentsList");
            }
        }

        public ObservableCollection<CentralTendencyResult> TendencyResult
        {
            get { return tendencyResult; }
            set 
            {
                tendencyResult = value;
                OnPropertyChanged("TendencyResult");
            }
        }

        private void CalculateCentralTendencyResult(List<double> results)
        {
            double median = CentralTendencyCalculator.GetMedian(results);
            double average = CentralTendencyCalculator.GetAverage(results);

            string modesToString = MergeModesToOneString(results);

            TendencyResult.Add(new CentralTendencyResult(median, modesToString, average));
        }

        private string MergeModesToOneString(List<double> results)
        {
            List<double> modes = CentralTendencyCalculator.GetMode(results);
            string modesToString = "";

            if (modes.Count() > 1)
            {
                modesToString += modes[0].ToString();
                modes.RemoveAt(0);

                foreach (double mode in modes)
                {
                    modesToString += ", " + mode.ToString();
                }
            }
            else
                modesToString += modes[0];

            return modesToString;
        }

        private void GetAllStudentsResults()
        {
            foreach(Student student in StudentsList)
            {
                results.Add(student.Result);
            }
        }

        private void SetStudentsList(ObservableCollection<Student> newList)
        {
            StudentsList = newList;
        }
    }
}

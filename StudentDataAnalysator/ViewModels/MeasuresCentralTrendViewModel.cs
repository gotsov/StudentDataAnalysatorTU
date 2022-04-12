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
        private ObservableCollection<Log> logsList;
        private List<double> results = new List<double>();
        private Dictionary<double, int> studentCoursesViewedDict;

        public MeasuresCentralTrendViewModel()
        {
            TendencyResult = new ObservableCollection<CentralTendencyResult>();
            StudentCoursesViewedDict = new Dictionary<double, int>();

            SingletonClass.TestEventAggregator.GetEvent<GetStudentsResultsListEvent>().Subscribe(SetStudentsList);
            SingletonClass.TestEventAggregator.GetEvent<GetLogsListEvent>().Subscribe(SetLogsList);
            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Publish("");

            GetCoursesViewByUsers();
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

        public ObservableCollection<Log> LogsList
        {
            get
            {
                return logsList;
            }
            set
            {
                logsList = value;
                OnPropertyChanged("LogsList");
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

        public Dictionary<double, int> StudentCoursesViewedDict
        {
            get { return studentCoursesViewedDict; }
            set
            {
                studentCoursesViewedDict = value;
                OnPropertyChanged("StudentCoursesViewedDict");
            }
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

        private void GetCoursesViewByUsers()
        {
            FillDictionaryWithCoursesViewedData();

            List<double> coursesViewedByUser = SetListToCalculateTendencies();

            CalculateCentralTendencyResult(coursesViewedByUser);
        }

        private void CalculateCentralTendencyResult(List<double> results)
        {
            double median = CentralTendencyCalculator.GetMedian(results);
            double average = Math.Round(CentralTendencyCalculator.GetAverage(results), 2);

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

        private void FillDictionaryWithCoursesViewedData()
        {
            int count;
            foreach (var student in StudentsList)
            {
                count = 0;
                foreach (var log in LogsList)
                {
                    if (log.Description.Contains(student.Id.ToString()) && log.EventName == "Course viewed")
                    {
                        count++;
                        StudentCoursesViewedDict[student.Id] = count;
                    }
                }
            }
        }

        private List<double> SetListToCalculateTendencies()
        {
            List<double> result = new List<double>();

            foreach (int coursesViewed in StudentCoursesViewedDict.Values)
            {
                result.Add(coursesViewed);
            }
            
            return result;
        }
                    

    private void SetLogsList(ObservableCollection<Log> newLogsList)
        {
            StudentsList = newList;
        }
    }
}

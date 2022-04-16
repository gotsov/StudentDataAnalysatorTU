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
    public class DistractionMeasuresViewModel : BaseViewModel
    {
        private ObservableCollection<StatisticalDispersionResult> dispersionResult;
        private List<double> studentIds;
        private List<int> coursesViewedByEachStudent;
        private ObservableCollection<Log> logsList;
        private Dictionary<double, int> studentCoursesViewedDict;
        public DistractionMeasuresViewModel()
        {
            studentIds = new List<double>();
            DispersionResult = new ObservableCollection<StatisticalDispersionResult>();
            CoursesViewedByEachStudent = new List<int>();
            StudentCoursesViewedDict = new Dictionary<double, int>();
            
            SingletonClass.TestEventAggregator.GetEvent<GetLogsListEvent>().Subscribe(SetLogsList);
            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Publish("");
            SetDispersionOfViewedCourses();
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
        public ObservableCollection<StatisticalDispersionResult> DispersionResult
        {
            get { return dispersionResult; }
            set
            {
                dispersionResult = value;
                OnPropertyChanged("DispersionResult");
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
        public List<int> CoursesViewedByEachStudent
        {
            get { return coursesViewedByEachStudent; }
            set
            {
                coursesViewedByEachStudent = value;
                OnPropertyChanged("CoursesViewedByEachStudent");
            }
        }
        private void SetDispersionOfViewedCourses()
        {
            ExtractAllStudentsFromLogs();
            FillDictionaryWithCoursesViewedData();
            FillCountOfViewedCoursesByStudents();
            CalculateDispersionResult();
        }
        private void ExtractAllStudentsFromLogs()
        {
            double studentId;
            foreach (Log log in LogsList)
            {
                studentId = Double.Parse(log.Description.Substring(18, 4));
                if (!studentIds.Contains(studentId))
                {
                    studentIds.Add(studentId);
                }
            }
        }
        private void FillDictionaryWithCoursesViewedData()
        {
            int coursesViewed;
            foreach (double id in studentIds)
            {
                coursesViewed = 0;
                foreach (Log log in LogsList)
                {
                    if (log.Description.Contains(id.ToString()) && log.EventName == "Course viewed")
                    {
                        coursesViewed++;
                        StudentCoursesViewedDict[id] = coursesViewed;
                    }
                }
            }
        }
        private void FillCountOfViewedCoursesByStudents()
        {
            foreach (var student in StudentCoursesViewedDict)
            {
                CoursesViewedByEachStudent.Add(student.Value);
            }
            CoursesViewedByEachStudent = CoursesViewedByEachStudent.OrderBy(n => n).ToList();
        }
        private void CalculateDispersionResult()
        {
            int minMaxDispersion;
            double variance, standartDeviation;
            minMaxDispersion = StatisticalDispersionCalculator.CalculateMinMaxDispersion(CoursesViewedByEachStudent);
            variance = StatisticalDispersionCalculator.CalculateVariance(CoursesViewedByEachStudent);
            standartDeviation = StatisticalDispersionCalculator.CalculateStandartDeviation(CoursesViewedByEachStudent);
            DispersionResult.Add(new StatisticalDispersionResult(minMaxDispersion, Math.Round(variance, 2), Math.Round(standartDeviation, 2)));
        }
        private void SetLogsList(ObservableCollection<Log> newLogsList)
        {
            LogsList = newLogsList;
        }
    }
}


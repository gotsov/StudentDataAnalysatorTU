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
    public class FrequencyDistributionViewModel : BaseViewModel
    {
        private ObservableCollection<FrequencyDistributionResult> frequencyResult;
        private List<double> studentIds;
        private ObservableCollection<Log> logsList;
        private List<double> results = new List<double>();
        private Dictionary<double, int> studentCoursesViewedDict;
        private SortedDictionary<int, int> frequencyViewedCoursesDict;
        public FrequencyDistributionViewModel()
        {
            studentIds = new List<double>();
            FrequencyResult = new ObservableCollection<FrequencyDistributionResult>();
            StudentCoursesViewedDict = new Dictionary<double, int>();
            FrequencyViewedCoursesDict = new SortedDictionary<int, int>();
           
            SingletonClass.TestEventAggregator.GetEvent<GetLogsListEvent>().Subscribe(SetLogsList);
            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Publish("");
            SetFrequencyOfViewedCourses();
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
        public ObservableCollection<FrequencyDistributionResult> FrequencyResult
        {
            get { return frequencyResult; }
            set
            {
                frequencyResult = value;
                OnPropertyChanged("FrequencyResult");
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
        public SortedDictionary<int, int> FrequencyViewedCoursesDict
        {
            get { return frequencyViewedCoursesDict; }
            set
            {
                frequencyViewedCoursesDict = value;
                OnPropertyChanged("FrequencyViewedCoursesDict");
            }
        }
        private void SetFrequencyOfViewedCourses()
        {
            ExtractAllStudentsFromLogs();
            FillDictionaryWithCoursesViewedData();
            FillFrequencyOfViewedCourses();
            CalculateFrequencyDistributionResult();
        }
        private void ExtractAllStudentsFromLogs()
        {
            double studentId;
            foreach(Log log in LogsList)
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
        private void FillFrequencyOfViewedCourses()
        {
            Dictionary<int, int> UnsortedFrequencies = new Dictionary<int, int>();
            int studentsCount;
            foreach (var student in StudentCoursesViewedDict)
            {
                if (!UnsortedFrequencies.ContainsKey(student.Value))
                {
                    studentsCount = 1;
                    UnsortedFrequencies[student.Value] = studentsCount;
                }
                else
                {
                    UnsortedFrequencies.TryGetValue(student.Value, out studentsCount);
                    studentsCount++;
                    UnsortedFrequencies[student.Value] = studentsCount;
                }
            }
            FrequencyViewedCoursesDict = new SortedDictionary<int, int>(UnsortedFrequencies);
        }
        private void CalculateFrequencyDistributionResult()
        {
            int absoluteFrequency;
            double relativeFrequency,totalPercentage=0;
            absoluteFrequency = FrequencyDistributionCalculator.CalculateAbsoluteFrequency(FrequencyViewedCoursesDict);
            foreach (var frequency in frequencyViewedCoursesDict) 
            {
                relativeFrequency = FrequencyDistributionCalculator.CalculateRelativeFrequency(frequencyViewedCoursesDict,frequency.Value);
                FrequencyResult.Add(new FrequencyDistributionResult(frequency.Key.ToString(), frequency.Value, relativeFrequency.ToString()+"%"));
                totalPercentage += relativeFrequency;
            }
            FrequencyResult.Add(new FrequencyDistributionResult(
                "Общо", 
                absoluteFrequency,
                Math.Round(totalPercentage, 1).ToString()+"%")
                );
        }
        private void SetLogsList(ObservableCollection<Log> newLogsList)
        {
            LogsList = newLogsList;
        }
    }
}


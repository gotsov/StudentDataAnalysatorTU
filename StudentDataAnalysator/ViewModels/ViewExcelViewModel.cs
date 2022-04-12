using StudentDataAnalysator.Events;
using StudentDataAnalysator.Models;
using StudentDataAnalysator.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentDataAnalysator.Enums.Enums;

namespace StudentDataAnalysator.ViewModels
{
    public class ViewExcelViewModel : BaseViewModel
    {
        private ObservableCollection<Student> studentsList;
        private ObservableCollection<Log> logsList;
        private ObservableCollection<string> tableTypeNames;
        private string selectedTableTypeToShow;
        private int tableType;

        public ViewExcelViewModel()
        {
            StudentsList = new ObservableCollection<Student>();
            LogsList = new ObservableCollection<Log>();
            TableTypeNames = new ObservableCollection<string>();

            TableTypeNames.Add("Резултати");
            TableTypeNames.Add("Дейности");

            SelectedTableTypeToShow = TableTypeNames.FirstOrDefault();

            SingletonClass.TestEventAggregator.GetEvent<GetStudentsResultsListEvent>().Subscribe(SetStudentsList);
            SingletonClass.TestEventAggregator.GetEvent<GetLogsListEvent>().Subscribe(SetLogsList);

            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Publish("");
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

        public ObservableCollection<string> TableTypeNames
        {
            get { return tableTypeNames; }
            set
            {
                tableTypeNames = value;
                OnPropertyChanged("TableTypeNames");
            }
        }

        public string SelectedTableTypeToShow
        {
            get { return selectedTableTypeToShow; }
            set
            {
                selectedTableTypeToShow = value;

                if(SelectedTableTypeToShow == TableTypeNames.FirstOrDefault())
                    TableType = (int)TableTypeEnum.StudentsResultTable;
                else
                    TableType = (int)TableTypeEnum.StudentsLogsTable;


                OnPropertyChanged("SelectedTableTypeToShow");
            }
        }


        public int TableType
        {
            get { return tableType; }
            set
            {
                tableType = value;
                OnPropertyChanged("TableType");
            }
        }

        private void GetTableType(string path)
        {
            ExcelFileLoaderService _excelDataReader = new ExcelFileLoaderService(path);

            if (_excelDataReader.GetTableType() == (int)TableTypeEnum.StudentsResultTable)
                TableType = (int)TableTypeEnum.StudentsResultTable;
            else
                TableType = (int)TableTypeEnum.StudentsLogsTable;
        }

        private void SetStudentsList(ObservableCollection<Student> newStudentsList)
        {
            TableType = (int)TableTypeEnum.StudentsResultTable;
            StudentsList = newStudentsList;

            SelectedTableTypeToShow = TableTypeNames.FirstOrDefault();
        }

        private void SetLogsList(ObservableCollection<Log> newLogsList)
        {
            TableType = (int)TableTypeEnum.StudentsLogsTable;
            LogsList = newLogsList;

            SelectedTableTypeToShow = TableTypeNames.LastOrDefault();
        }

    }
}
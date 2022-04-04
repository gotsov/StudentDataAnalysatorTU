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
        private ObservableCollection<Student> studentsList = new ObservableCollection<Student>();
        private ObservableCollection<Log> logsList = new ObservableCollection<Log>();
        private int tableType;

        public ViewExcelViewModel()
        {
            SingletonClass.TestEventAggregator.GetEvent<UpdateTableEvent>().Subscribe(LoadTable);
            SingletonClass.TestEventAggregator.GetEvent<UpdateSelectedPathEvent>().Publish("");
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

        public int TableType
        {
            get { return tableType; }
            set
            {
                tableType = value;
                OnPropertyChanged("TableType");
            }
        }

        private void LoadTable(string path)
        {
            ExcelFileLoaderService _excelDataReader = new ExcelFileLoaderService(path);

            if (_excelDataReader.GetTableType() == (int)TableTypeEnum.StudentsResultTable)
            {
                TableType = (int)TableTypeEnum.StudentsResultTable;
                StudentsList = _excelDataReader.StudentListFromExcelTable();
            }
            else
            {
                TableType = (int)TableTypeEnum.StudentsLogsTable;
                LogsList = _excelDataReader.LogListFromExcelTable();
            }
        }

    }
}

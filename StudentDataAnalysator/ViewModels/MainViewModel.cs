﻿using Microsoft.Win32;
using StudentDataAnalysator.Commands;
using StudentDataAnalysator.ViewModels;
using StudentDataAnalysator.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using StudentDataAnalysator.Models;
using static StudentDataAnalysator.Enums.Enums;
using Prism.Events;
using StudentDataAnalysator.Events;

namespace StudentDataAnalysator
{
    public class MainViewModel : BaseViewModel
    {
        private int switchView;
        private RelayCommand _openViewExcelViewCommand;
        private RelayCommand _openFrequencyViewCommand;
        private RelayCommand _openMeasursCentralTrendViewCommand;
        private RelayCommand _openDistractionMeasuresViewCommand;
        private RelayCommand _openCorrelationAnalysisViewViewCommand;
        private RelayCommand _searchFileCommand;
        private string _selectedPath;
        private string _selectedPathStudentsResults;
        private string _selectedPathLogs;
        private ExcelFileLoaderService _excelDataReader;
        private bool _isButtonEnabled;

        private ObservableCollection<Student> studentsList;
        private ObservableCollection<Log> logsList;

        public MainViewModel()
        {
            StudentsList = new ObservableCollection<Student>();
            LogsList = new ObservableCollection<Log>();

            SingletonClass.TestEventAggregator.GetEvent<UpdateListsEvent>().Subscribe(SendList);
            IsButtonEnabled = false;

            SelectedPathStudentsResults = "Избери файл с резултати на студентите (StudentsResults)";
            SelectedPathLogs = "Избери файл с дейности на студентите (Logs_Course / StudentActivities)";
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

                SingletonClass.TestEventAggregator.GetEvent<GetStudentsResultsListEvent>().Publish(StudentsList);
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

                SingletonClass.TestEventAggregator.GetEvent<GetLogsListEvent>().Publish(LogsList);
            }
        }
        public RelayCommand OpenViewExcelViewCommand
        {
            get
            {
                if (_openViewExcelViewCommand == null)
                {
                    _openViewExcelViewCommand = new RelayCommand(param => this.ChangeSwitchView(0),
                        param => true);
                }
                return _openViewExcelViewCommand;
            }
        }

        public RelayCommand OpenFrequencyViewCommand
        {
            get
            {
                if (_openFrequencyViewCommand == null)
                {
                    _openFrequencyViewCommand = new RelayCommand(param => this.ChangeSwitchView(1),
                        param => true);
                }
                return _openFrequencyViewCommand;
            }
        }

        public RelayCommand OpenMeasursCentralTrendViewCommand
        {
            get
            {
                if (_openMeasursCentralTrendViewCommand == null)
                {
                    _openMeasursCentralTrendViewCommand = new RelayCommand(param => this.ChangeSwitchView(2),
                        param => true);
                }
                return _openMeasursCentralTrendViewCommand;
            }
        }

        public RelayCommand OpenDistractionMeasuresViewCommand
        {
            get
            {
                if (_openDistractionMeasuresViewCommand == null)
                {
                    _openDistractionMeasuresViewCommand = new RelayCommand(param => this.ChangeSwitchView(3),
                        param => true);
                }
                return _openDistractionMeasuresViewCommand;
            }
        }

        public RelayCommand OpenCorrelationAnalysisViewViewCommand
        {
            get
            {
                if (_openCorrelationAnalysisViewViewCommand == null)
                {
                    _openCorrelationAnalysisViewViewCommand = new RelayCommand(param => this.ChangeSwitchView(4),
                        param => true);
                }
                return _openCorrelationAnalysisViewViewCommand;
            }
        }
        public RelayCommand SearchFileCommand
        {
            get
            {
                if (_searchFileCommand == null)
                {
                    _searchFileCommand = new RelayCommand(param => this.ExecuteOpenFileDialog(),
                        param => true);
                }
                return _searchFileCommand;
            }
        }

        public string SelectedPath
        {
            get
            {
                return _selectedPath;
            }
            set
            {
                _selectedPath = value;

                _excelDataReader = new ExcelFileLoaderService(SelectedPath);

                if (_excelDataReader.IsFileExcel(SelectedPath))
                {
                    OnPropertyChanged("SelectedPath");

                    GetExcelData(SelectedPath);
                }
                else
                {
                    MessageBox.Show("Invalid file. File must be .xls");
                }

                SwitchView = 0;
            }
        }

        public string SelectedPathStudentsResults
        {
            get { return _selectedPathStudentsResults; }
            set
            {
                _selectedPathStudentsResults = value;
                OnPropertyChanged("SelectedPathStudentsResults");
            }
        }

        public string SelectedPathLogs
        {
            get { return _selectedPathLogs; }
            set
            {
                _selectedPathLogs = value;
                OnPropertyChanged("SelectedPathLogs");
            }
        }

        public int SwitchView
        {
            get { return switchView; }
            set
            {
                switchView = value;
                OnPropertyChanged("SwitchView");
            }
        }

        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
                OnPropertyChanged("IsButtonEnabled");
            }
        }

        public void ChangeSwitchView(int viewNum)
        {
            SwitchView = viewNum;
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            SelectedPath = dialog.FileName;
        }

        public void SendList(string test)
        {
            if (SelectedPath != null)
            {
                    StudentsList = StudentsList;
                    LogsList = LogsList;
            }
        }

        private void GetExcelData(string path)
        {
            ExcelFileLoaderService _excelDataReader = new ExcelFileLoaderService(path);

            if (IsTableStudentsResults())
            {
                StudentsList = _excelDataReader.StudentListFromExcelTable();
                SelectedPathStudentsResults = SelectedPath;
                IsButtonEnabled = true;
            }
            else
            {
                LogsList = _excelDataReader.LogListFromExcelTable();
                SelectedPathLogs = SelectedPath;
                IsButtonEnabled=false;
            }
        }


        private bool IsTableStudentsResults()
        {
            return _excelDataReader.GetTableType() == (int)TableTypeEnum.StudentsResultTable;
        
        }
    }
}
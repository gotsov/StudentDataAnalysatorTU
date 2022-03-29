using Microsoft.Win32;
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

namespace StudentDataAnalysator
{
    public class MainViewModel : BaseViewModel
    {
        private int switchView;

        private RelayCommand _openFrequencyViewCommand;
        private RelayCommand _openMeasursCentralTrendViewCommand;
        private RelayCommand _openDistractionMeasuresViewCommand;
        private RelayCommand _openCorrelationAnalysisViewViewCommand;
        private RelayCommand _searchFileCommand;
        private string _selectedPath;
        private ExcelFileLoaderService _excelDataReader;


        public MainViewModel()
        {
        }

        public RelayCommand OpenFrequencyViewCommand
        {
            get
            {
                if (_openFrequencyViewCommand == null)
                {
                    _openFrequencyViewCommand = new RelayCommand(param => this.ChangeSwitchView(0),
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
                    _openMeasursCentralTrendViewCommand = new RelayCommand(param => this.ChangeSwitchView(1),
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
                    _openDistractionMeasuresViewCommand = new RelayCommand(param => this.ChangeSwitchView(2),
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
                    _openCorrelationAnalysisViewViewCommand = new RelayCommand(param => this.ChangeSwitchView(3),
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

                _excelDataReader = new ExcelFileLoaderService();

                if (_excelDataReader.IsFileExcel(SelectedPath))
                {
                    _excelDataReader = new ExcelFileLoaderService(SelectedPath);
                    OnPropertyChanged("SelectedPath");
                }
                else
                {
                    MessageBox.Show("Invalid file. File must be excel");
                }
                
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

    }
}

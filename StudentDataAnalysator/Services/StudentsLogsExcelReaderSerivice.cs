using ExcelDataReader;
using StudentDataAnalysator.Interfaces;
using StudentDataAnalysator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Services
{
    public class StudentsLogsExcelReaderSerivice : IExcelFileReaderService
    {

        public ObservableCollection<Log> LogsList { get; set; }
        public void ReadExcel(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            LogsList = new ObservableCollection<Log>();

            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            int n = 0;
            while (reader.Read())
            {
                if (n != 0)
                {
                    LogsList.Add(new Log(
                        DateTime.Parse(reader.GetString(0)),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                       ));
                }
                n++;
            }
        }
    }
}

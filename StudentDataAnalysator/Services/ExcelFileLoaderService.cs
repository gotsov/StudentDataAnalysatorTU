using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using StudentDataAnalysator.Models;

namespace StudentDataAnalysator.Services
{
    public class ExcelFileLoaderService
    {
        private string path;
        public ObservableCollection<Student> StudentsList { get; set; }
        public ObservableCollection<Log> LogsList { get; set; }

        public ExcelFileLoaderService()
        {

        }
        public ExcelFileLoaderService(string path)
        {
            this.path = path;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            StudentsList = new ObservableCollection<Student>();
            LogsList = new ObservableCollection<Log>();


            ReadExcel2();
        }

        public bool IsFileExcel(string path)
        {
            string fileExt = Path.GetExtension(path);

            if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                return true;
            else
                return false;

            return false;
        }

        private void ReadExcel()
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                if (reader.GetFieldType(1) != typeof(string) && reader.GetFieldType(1) != null)
                {
                    StudentsList.Add(new Student(reader.GetDouble(0), reader.GetDouble(1)));
                }
            }
        }

        private void ReadExcel2()
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            int n = 0;
            while (reader.Read())
            {
                if (n!=0)
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
            foreach(var log in LogsList)
                Console.WriteLine(log.ToString);
        }

        public ObservableCollection<Student> StudentListFromExcelTable()
        {
            return StudentsList;
        }

        public ObservableCollection<Log> LogListFromExcelTable()
        {
            return LogsList;
        }
    }
}

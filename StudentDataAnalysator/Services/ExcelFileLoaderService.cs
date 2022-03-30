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

        public ExcelFileLoaderService()
        {

        }
        public ExcelFileLoaderService(string path)
        {
            this.path = path;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            StudentsList = new ObservableCollection<Student>();

            ReadExcel();
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

        public ObservableCollection<Student> GetStudentsListFromExcelTable()
        {
            return StudentsList;
        }
    }
}

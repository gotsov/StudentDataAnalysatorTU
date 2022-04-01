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
    public class StudentsResultsExcelReaderService : IExcelFileReaderService
    {

        public ObservableCollection<Student> StudentsList { get; set; }
        public void ReadExcel(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            StudentsList = new ObservableCollection<Student>();

            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                if (reader.GetFieldType(1) != typeof(string) && reader.GetFieldType(1) != null)
                {
                    StudentsList.Add(new Student(reader.GetDouble(0), reader.GetDouble(1)));
                }
            }

            stream.Close();
        }

    }
}

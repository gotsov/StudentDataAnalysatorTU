﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using StudentDataAnalysator.Models;
using static StudentDataAnalysator.Enums.Enums;

namespace StudentDataAnalysator.Services
{
    public class ExcelFileLoaderService
    {
        private string path;

        public ExcelFileLoaderService(string path)
        {
            this.path = path;
        }

        public bool IsFileExcel(string path)
        {
            string fileExt = Path.GetExtension(path);

            if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                return true;
            else
                return false;
        }

        public int GetTableType()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);

            if (reader.FieldCount > 2)
            {
                stream.Close();
                return (int)TableTypeEnum.StudentsLogsTable;
            }
            else
            {
                stream.Close();
                return (int)TableTypeEnum.StudentsResultTable;
            }
        }

        public ObservableCollection<Student> StudentListFromExcelTable()
        {
            StudentsResultsExcelReaderService service = new StudentsResultsExcelReaderService();
            service.ReadExcel(path);

            return service.StudentsList;
        }

        public ObservableCollection<Log> LogListFromExcelTable()
        {
            StudentsLogsExcelReaderSerivice service = new StudentsLogsExcelReaderSerivice();
            service.ReadExcel(path);

            return service.LogsList;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace StudentDataAnalysator.Services
{
    public class ExcelFileLoaderService
    {
        private string path;
        private FileStream stream;

        public ExcelFileLoaderService()
        {

        }
        public ExcelFileLoaderService(string path)
        {
            this.path = path;

            stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

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
    }
}

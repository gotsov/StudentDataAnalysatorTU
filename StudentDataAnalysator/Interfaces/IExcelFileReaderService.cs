using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysator.Interfaces
{
    public interface IExcelFileReaderService
    {
        public void ReadExcel(string path);
    }
}

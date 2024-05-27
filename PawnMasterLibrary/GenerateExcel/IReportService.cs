using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary.GenerateExcel
{
    public interface IReportService
    {
        void GenerateCombinedReport(IEnumerable<Product> products, string filePath);
    }
}

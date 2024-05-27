using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary.GenerateExcel
{
    public class ReportService : IReportService
    {
        public void GenerateCombinedReport(IEnumerable<Product> products, string filePath)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products Report");

                // Заголовки
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Название";
                worksheet.Cells[1, 3].Value = "Описание";
                worksheet.Cells[1, 4].Value = "Дата покупки";
                worksheet.Cells[1, 5].Value = "Цена покупки";
                worksheet.Cells[1, 6].Value = "Дата продажи";
                worksheet.Cells[1, 7].Value = "Цена продажи";
                worksheet.Cells[1, 8].Value = "Имя сотрудника";

                int row = 2; 

                foreach (var product in products)
                {
                    worksheet.Cells[row, 1].Value = product.ID;
                    worksheet.Cells[row, 2].Value = product.Name;
                    worksheet.Cells[row, 3].Value = product.Description;
                    worksheet.Cells[row, 4].Value = product.DateBuy;
                    worksheet.Cells[row, 5].Value = product.PriceBuy;

                    if (product.IsSale)
                    {
                        worksheet.Cells[row, 6].Value = product.DateSale;
                        worksheet.Cells[row, 7].Value = product.PriceSale;
                        worksheet.Cells[row, 8].Value = product.EmployeeName;
                    }

                    row++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                File.WriteAllBytes(filePath, package.GetAsByteArray());
            }
        }
    }
}


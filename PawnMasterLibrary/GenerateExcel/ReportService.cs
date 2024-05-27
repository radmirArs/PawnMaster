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
            var currentProducts = products.Where(p => !p.IsSale).ToList();
            var soldProducts = products.Where(p => p.IsSale).ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products Report");

                // Заголовки для текущих товаров
                worksheet.Cells[1, 1].Value = "Текущие товары";
                worksheet.Cells[2, 1].Value = "ID";
                worksheet.Cells[2, 2].Value = "Название";
                worksheet.Cells[2, 3].Value = "Описание";
                worksheet.Cells[2, 4].Value = "Дата покупки";
                worksheet.Cells[2, 5].Value = "Цена покупки";

                for (int i = 0; i < currentProducts.Count; i++)
                {
                    var product = currentProducts[i];
                    worksheet.Cells[i + 3, 1].Value = product.ID;
                    worksheet.Cells[i + 3, 2].Value = product.Name;
                    worksheet.Cells[i + 3, 3].Value = product.Description;
                    worksheet.Cells[i + 3, 4].Value = product.DateBuy;
                    worksheet.Cells[i + 3, 5].Value = product.PriceBuy;
                }

                int soldProductsStartRow = currentProducts.Count + 5;

                // Заголовки для проданных товаров
                worksheet.Cells[soldProductsStartRow - 1, 1].Value = "Проданные товары";
                worksheet.Cells[soldProductsStartRow, 1].Value = "ID";
                worksheet.Cells[soldProductsStartRow, 2].Value = "Название";
                worksheet.Cells[soldProductsStartRow, 3].Value = "Описание";
                worksheet.Cells[soldProductsStartRow, 4].Value = "Дата покупки";
                worksheet.Cells[soldProductsStartRow, 5].Value = "Цена покупки";
                worksheet.Cells[soldProductsStartRow, 6].Value = "Дата продажи";
                worksheet.Cells[soldProductsStartRow, 7].Value = "Цена продажи";
                worksheet.Cells[soldProductsStartRow, 8].Value = "Имя сотрудника";

                for (int i = 0; i < soldProducts.Count; i++)
                {
                    var product = soldProducts[i];
                    worksheet.Cells[soldProductsStartRow + i + 1, 1].Value = product.ID;
                    worksheet.Cells[soldProductsStartRow + i + 1, 2].Value = product.Name;
                    worksheet.Cells[soldProductsStartRow + i + 1, 3].Value = product.Description;
                    worksheet.Cells[soldProductsStartRow + i + 1, 4].Value = product.DateBuy;
                    worksheet.Cells[soldProductsStartRow + i + 1, 5].Value = product.PriceBuy;
                    worksheet.Cells[soldProductsStartRow + i + 1, 6].Value = product.DateSale;
                    worksheet.Cells[soldProductsStartRow + i + 1, 7].Value = product.PriceSale;
                    worksheet.Cells[soldProductsStartRow + i + 1, 8].Value = product.EmployeeName;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                File.WriteAllBytes(filePath, package.GetAsByteArray());
            }
        }
    }
}


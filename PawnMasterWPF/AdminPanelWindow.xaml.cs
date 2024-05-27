using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using PawnMasterLibrary;
using PawnMasterLibrary.GenerateExcel;

namespace PawnMasterWPF
{
    public partial class AdminPanelWindow : Window
    {
        private IProductService dataService;
        private IReportService reportService;
        public AdminPanelWindow()
        {
            InitializeComponent();
            dataService = new ProductService();
            reportService = new ReportService();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.Show();
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.LoggedAdmin();
            mainWindow.Show();
            Close();
        }


        private void UserDataGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = ObjectControl.Deserialize<Employee>();
            UserDataGrid.ItemsSource = employees;
        }

        private void AllProductDataGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<Product> products = ObjectControl.Deserialize<Product>();
            AllProductDataGrid.ItemsSource = products;
        }

        private void DownloadProduct_Click(object sender, RoutedEventArgs e)
        {
            var products = dataService.GetAllProducts();
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Сохранить отчет",
                FileName = "Отчет по товарам.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    reportService.GenerateCombinedReport(products, filePath);
                    MessageBox.Show($"Отчет по товарам успешно сохранен");
                }
                catch
                {
                    MessageBox.Show($"Ошибка при генерации отчета");
                }
            }
        }
    }
}


    


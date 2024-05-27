using System.Collections.Generic;
using System.Windows;
using PawnMasterLibrary;

namespace PawnMasterWPF
{
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
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
    }
}

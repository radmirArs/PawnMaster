using PawnMasterLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PawnMasterWPF
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
        }
        void UserDataGridСompletion()
        {
            
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.Show();
            Close();
        }

        private void ImageAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.LoggedAdmin();
            mainWindow.Show();
            Close();
        }

        private void DeleteProduct_click(object sender, RoutedEventArgs e)
        {

        }

        private void UserDataGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = EmployeeControl.ReceivingEmployeeInfo();
            UserDataGrid.ItemsSource = employees;
        }

    }
}

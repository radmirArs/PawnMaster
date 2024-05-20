using System.Windows;
using PawnMasterLibrary;
using System.ComponentModel;

namespace PawnMasterWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    string _role;
    //надо добавлят имя пользователя на первом экране

    private Employee loggedEmployee;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ImageAdd_Click(object sender, RoutedEventArgs e)
    {

    }

    private void PurchaseProduct_click(object sender, RoutedEventArgs e)
    {

    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        LoginWindow loginWindow = new LoginWindow();
        // loginWindow.Topmost = true;
        // loginWindow.Closed += (s, args) => { this.IsEnabled = true; };
        // this.IsEnabled = false;
        loginWindow.Show();
        Close();
    }
    
    private void AdminPanel_Click(object sender, RoutedEventArgs e)
    {
        if(_role == "A")
        {
            AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
            adminPanelWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Недостаточно прав");
        }
    }

    public void LoggedUserAdd(Employee LoggedEmployee)
    {
        loggedEmployee = LoggedEmployee;
        NameUser.Text = loggedEmployee.EmployeeFullName;
        _role = "U";
    }

    public void LoggedAdmin()
    {
        NameUser.Text = "Админ";
        _role = "A";
    }

    private void ProductAdd_Click(object sender, RoutedEventArgs e)
    {

    }
}
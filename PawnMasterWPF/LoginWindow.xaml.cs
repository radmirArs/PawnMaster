using System.Windows;
using PawnMasterLibrary;

namespace PawnMasterWPF;

public partial class LoginWindow : Window
{
    Employee LoggedEmloyee;
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        string login = UserLoginTextBox.Text;
        string password = UserPasswordTextBox.Text;
        if(login == "admin" && password == "admin")
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.LoggedAdmin();
            mainWindow.Show();
            Close();
        }

        else
        {
            LoginUser loginUser = new LoginUser(login, password);
            LoggedEmloyee = loginUser.LoggedEmployee();
            if (loginUser.Successfully)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.LoggedUserAdd(LoggedEmloyee);
                mainWindow.Show();
                Close();
            }

            else
                MessageBox.Show("Неверно введен логин или пароль");
        }
        
    }
}

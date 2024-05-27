using System.Windows;
using PawnMasterLibrary;

namespace PawnMasterWPF;

public partial class LoginWindow : Window
{
    private ILoginService loginService;

    public LoginWindow()
    {
        InitializeComponent();
        loginService = new LoginUserService();
        loginService.LoginSucceeded += LoginService_LoginSucceeded;
        loginService.LoginFailed += LoginService_LoginFailed;
    }
    private void LoginService_LoginSucceeded(object sender, LoginEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.LoggedUserAdd(e.Employee);
        mainWindow.Show();
        Close();
    }

    private void LoginService_LoginFailed(object sender, LoginEventArgs e)
    {
        MessageBox.Show("Неверно введен логин или пароль");
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

        if (login == "admin" && password == "admin")
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.LoggedAdmin();
            mainWindow.Show();
            Close();
        }
        else
        {
            loginService.Login(login, password);
        }
    }

    





}

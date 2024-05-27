using System.Windows;
using PawnMasterLibrary;

namespace PawnMasterWPF;

public partial class LoginWindow : Window
{
    public delegate void LoginSuccessfulDelegate(object sender, Employee e);

    public event LoginSuccessfulDelegate LoginSuccessful;
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

        if (login == "admin" && password == "admin")
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.LoggedAdmin();
            mainWindow.Show();
            Close();
        }
        else
        {
            LoginUser loginUser = new LoginUser(login, password);
            Employee loggedImplyee = loginUser.Login();
            if (loggedImplyee == null)
            {
                MessageBox.Show("Неверно введен логин или пароль");
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.LoggedUserAdd(loggedImplyee);
                mainWindow.Show();
                Close();
            }
            
        }
    }

    protected virtual void OnLoginSuccessful(Employee e)
    {
        LoginSuccessful?.Invoke(this, e);
    }

}

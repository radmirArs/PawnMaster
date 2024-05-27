using System.Windows;
using PawnMasterLibrary;

namespace PawnMasterWPF;

public partial class AddEmployeeWindow : Window
{
    public AddEmployeeWindow()
    {
        InitializeComponent();
    }

    private void AddEmployee_Click(object sender, RoutedEventArgs e)
    {
        string fullNameEmployee = FullNameEmployeeTextBox.Text;
        string phoneNumberEmployee = PhoneNumberEmployeeTextBox.Text;
        string emailEmployee = EmailEmployeeTextBox.Text;
        string loginEmployee = LoginEmployeeTextBox.Text;
        string passwordEmployee = PasswordEmployeeTextBox.Text;
        if (loginEmployee != "admin")
        {
            string errorString = CheckDataEployee(fullNameEmployee, phoneNumberEmployee, emailEmployee, loginEmployee, passwordEmployee);
            if (errorString == "")
            {

                Employee newEmployee = new Employee()
                {
                    FullName = fullNameEmployee,
                    PhoneNumber = phoneNumberEmployee,
                    Email = emailEmployee,
                    Password = passwordEmployee,
                    Login = loginEmployee
                };
                ObjectControl.AddSerialize(newEmployee);
                FullNameEmployeeTextBox.Text = "";
                PhoneNumberEmployeeTextBox.Text = "";
                EmailEmployeeTextBox.Text = "";
                LoginEmployeeTextBox.Text = "";
                PasswordEmployeeTextBox.Text = "";
            }
            else
                MessageBox.Show(errorString);
        }
    }

    void ChechRepeat()
    {

    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
        adminPanelWindow.Show();
        Close();
    }

    private string CheckDataEployee(string fullName, string phoneNumber, string email, string login, string password)
    {
        string errorMessage = "";
        if (fullName == "" || fullName.Length <= 5)
            errorMessage += "Поле \"ФИО\"  не удовлетворяет требованиям" + '\n';
        if (phoneNumber == "" || phoneNumber.Length <= 5)
            errorMessage += "Поле \"Номер телефона\" не удовлетворяет требованиям" + '\n';
        if (email == "" || email.Length <= 5)
            errorMessage += "Поле \"Почта\" не удовлетворяет требованиям" + '\n';
        if (login == "" || login.Length <= 5)
            errorMessage += "Поле \"логин\" не удовлетворяет требованиям" + '\n';
        if (password == "" || password.Length <= 5)
            errorMessage += "Поле \"пароль\" не удовлетворяет требованиям";

        return errorMessage;
    }
}

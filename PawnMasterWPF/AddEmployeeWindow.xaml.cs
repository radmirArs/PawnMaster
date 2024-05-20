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
            if (CheckDataEployee(fullNameEmployee, phoneNumberEmployee, emailEmployee, loginEmployee, passwordEmployee))
            {
                Employee newEmployee = new Employee()
                {
                    EmployeeFullName = fullNameEmployee,
                    EmployeePhoneNumber = phoneNumberEmployee,
                    EmployeeEmail = emailEmployee,
                    EmployeePassword = passwordEmployee,
                    EmployeeLogin = loginEmployee
                };
                EmployeeControl.AddEmployee(newEmployee);
                FullNameEmployeeTextBox.Text = "";
                PhoneNumberEmployeeTextBox.Text = "";
                EmailEmployeeTextBox.Text = "";
                LoginEmployeeTextBox.Text = "";
                PasswordEmployeeTextBox.Text = "";
            }
            else
                MessageBox.Show("Поля не должны быть пустыми и длина не должна быть менее 5 символов");
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
        adminPanelWindow.Show();
        Close();
    }

    private bool CheckDataEployee(string fullName, string phoneNumber, string email, string login, string password)
    {
        string errorMessage = "";
        if (fullName != "" || fullName.Length <= 5)
            errorMessage += "Поле \"ФИО\"  не удовлетворяет требованиям" + '\n';
        else if (phoneNumber != "" || phoneNumber.Length <= 5)
            errorMessage += "Поле \"Номер телефона\" не удовлетворяет требованиям" + '\n';
        else if (email != "" || email.Length <= 5)
            errorMessage += "Поле \"Почта\" не удовлетворяет требованиям" + '\n';
        else if (login != "" || login.Length <= 5)
            errorMessage += "Поле \"логин\" не удовлетворяет требованиям" + '\n';
        else if (password != "" || password.Length <= 5)
            errorMessage += "Поле \"пароль\" не удовлетворяет требованиям";
        
        if (errorMessage != "")
            return true;
        else
            return false;
    }
}

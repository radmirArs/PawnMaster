using System.Text.Json;
using PawnMasterWPF;

using PawnMasterLibrary;

namespace PawnMasterLibrary;

public class LoginUser
{
    private string _login;
    private string _password;
    public bool Successfully;
    public LoginUser(string login, string password)
    {
        _password = password;
        _login = login;
        Successfully = false;
    }

    public Employee LoggedEmployee()
    {
        List<Employee> employees = EmployeeControl.ReceivingEmployeeInfo();

        foreach (var employee in employees)
        {
            if (employee.EmployeePassword == _password && employee.EmployeeLogin == _login)
            {
                Console.WriteLine(employee.EmployeePassword);
                Employee employeeLogged = new Employee()
                {
                    EmployeeFullName = employee.EmployeeFullName,
                    EmployeePhoneNumber = employee.EmployeePhoneNumber,
                    EmployeeEmail = employee.EmployeeEmail,
                    EmployeePassword = _password,
                    EmployeeLogin = _login
                };
                Successfully = true;
                return employeeLogged;
            }
        }
        return null;
    }
}

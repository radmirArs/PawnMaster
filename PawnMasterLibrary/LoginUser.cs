using System;
using System.Collections.Generic;
using System.Linq;

namespace PawnMasterLibrary
{
    public class LoginUser
    {
        private string _login;
        private string _password;

        public LoginUser(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public Employee Login()
        {
            List<Employee> employees = ObjectControl.Deserialize<Employee>();
            Employee employeeLogged = employees.FirstOrDefault(employee =>
                employee.Password == _password && employee.Login == _login);
            return employeeLogged;
        }
    }
}

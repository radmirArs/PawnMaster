using System;
using System.Collections.Generic;
using System.Linq;

namespace PawnMasterLibrary
{
    public class LoginUserService : ILoginService
    {
        public event EventHandler<LoginEventArgs> LoginSucceeded;
        public event EventHandler<LoginEventArgs> LoginFailed;

        public Employee Login(string login, string password)
        {
            List<Employee> employees = ObjectControl.Deserialize<Employee>();
            Employee employee = employees.FirstOrDefault(e => e.Login == login && e.Password == password);

            if (employee != null)
            {
                OnLoginSucceeded(new LoginEventArgs { Employee = employee });
                return employee;
            }
            else
            {
                OnLoginFailed(new LoginEventArgs { Employee = null });
                return null;
            }
        }

        protected virtual void OnLoginSucceeded(LoginEventArgs e)
        {
            LoginSucceeded?.Invoke(this, e);
        }

        protected virtual void OnLoginFailed(LoginEventArgs e)
        {
            LoginFailed?.Invoke(this, e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary
{
    public interface ILoginService
    {
        event EventHandler<LoginEventArgs> LoginSucceeded;
        event EventHandler<LoginEventArgs> LoginFailed;

        Employee Login(string login, string password);
    }

    public class LoginEventArgs : EventArgs
    {
        public Employee Employee { get; set; }
    }
}

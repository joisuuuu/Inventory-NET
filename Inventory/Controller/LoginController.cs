using Inventory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Controller
{
    public class LoginController
    {
        private LoginView g_loginView;

        public LoginController(LoginView LoginView)
        {
            g_loginView = LoginView;
        }
    }
}

using Inventory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Controller
{
    public class RegistrationController
    {
        private RegistrationView g_registrationView;

        public RegistrationController(RegistrationView RegistrationView)
        {
            g_registrationView = RegistrationView;
        }
    }
}

using Inventory.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.View
{
    public partial class LoginView : Form
    {
        private LoginController g_loginController;

        public LoginView()
        {
            InitializeComponent();

            g_loginController = new LoginController(this);
        }
    }
}

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

namespace Inventory
{
    public partial class DashboardView : Form
    {
        private DashboardController g_dashboardController;

        public DashboardView()
        {
            InitializeComponent();
            g_dashboardController = new DashboardController(this);
        }
    }
}

using Inventory.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool;

namespace Inventory.Controller
{
    public class DashboardController
    {
        private DashboardView g_dashboardView;
        private MovablePanel g_movablePanel;

        private Form g_activeForm = null;

        private bool g_IsSideMenuCollapsed;

        private bool g_HasInitialized = false;

        public DashboardController(DashboardView DashboardView)
        {
            g_dashboardView = DashboardView;

            // make window movable without FormBorder (s)
            g_movablePanel = new MovablePanel(g_dashboardView, new Panel[] { g_dashboardView.panelMenu, g_dashboardView.panelBody, g_dashboardView.panelSideMenu });
            // make window movable without FormBorder (e)

            // events (s)
            #region events
            g_dashboardView.btnClose.Click -= btnClose_Click;
            g_dashboardView.btnClose.Click += btnClose_Click;

            g_dashboardView.btnMaximize.Click -= btnMaximize_Click;
            g_dashboardView.btnMaximize.Click += btnMaximize_Click;

            g_dashboardView.btnMinimize.Click -= btnMinimize_Click;
            g_dashboardView.btnMinimize.Click += btnMinimize_Click;

            g_dashboardView.btnMenuUserProfile.Click -= btnMenuUserProfile_Click;
            g_dashboardView.btnMenuUserProfile.Click += btnMenuUserProfile_Click;

            g_dashboardView.timerSideMenu.Tick -= timerSideMenu_Tick;
            g_dashboardView.timerSideMenu.Tick += timerSideMenu_Tick;

            g_dashboardView.btnHamburgerMenu.Click -= btnHamburgerMenu_Click;
            g_dashboardView.btnHamburgerMenu.Click += btnHamburgerMenu_Click;

            g_dashboardView.btnSubMenuUserProfile.Click -= btnSubMenuUserProfile_Click;
            g_dashboardView.btnSubMenuUserProfile.Click += btnSubMenuUserProfile_Click;

            g_dashboardView.btnMenuHome.Click -= btnMenuHome_Click;
            g_dashboardView.btnMenuHome.Click += btnMenuHome_Click;
            #endregion
            // events (e)

            this.SetupToolTips();
            this.InitializeDefaultView();
            this.InitializeDefaultMenu();

            g_HasInitialized = true;
        }

        private void InitializeDefaultMenu()
        {
            this.ShowHideSubMenus(false);
        }

        private void ShowHideSubMenus(bool Show)
        {
            g_dashboardView.panelSubMenuUserProfile.Visible = Show;

            if (!Show)
            {
                this.SetMenuText();
                this.UnselectSubMenus();
            }
        }

        private void UnselectSubMenus()
        {
            g_dashboardView.btnSubMenuLogout.selected = false;
            g_dashboardView.btnSubMenuUserProfile.selected = false;
        }

        private void ShowHideSubMenu(Panel SubMenu)
        {
            SubMenu.Visible = SubMenu.Visible ? false : true;
        }

        private void InitializeDefaultView()
        {
            HomeView view = new HomeView(); // by Default
            this.OpenChildForm(view);
        }

        private void OpenChildForm(Form ChildForm)
        {
            if (g_activeForm == ChildForm)
            {
                g_activeForm.Refresh();
                return;
            }
                
            if (g_activeForm != null)
                g_activeForm.Close();

            g_activeForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.Dock = DockStyle.Fill;
            
            g_dashboardView.panelBody.Controls.Add(ChildForm);
            g_dashboardView.panelBody.Tag = ChildForm;

            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void SetupToolTips()
        {
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(g_dashboardView.btnClose, "Exit App");
            toolTip.SetToolTip(g_dashboardView.btnMaximize, "Maximize/Minimize App");
            toolTip.SetToolTip(g_dashboardView.btnMinimize, "Hide App");
        }

        /// <summary>
        /// BUTTON METHODS
        /// </summary>
        #region Button methods
        #region Form Controls
        private void btnClose_Click(object sender, EventArgs e)
        {
            g_dashboardView.Close();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (g_dashboardView.WindowState == FormWindowState.Maximized)
                g_dashboardView.WindowState = FormWindowState.Normal;
            else
                g_dashboardView.WindowState = FormWindowState.Maximized;

            this.OpenChildForm(g_activeForm);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            g_dashboardView.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Menus with Submenus
        private void btnMenuUserProfile_Click(object sender, EventArgs e)
        {
            this.ShowHideSubMenu(g_dashboardView.panelSubMenuUserProfile);

            if (g_dashboardView.panelSubMenuUserProfile.Visible)
                g_dashboardView.btnMenuUserProfile.Text = "     ▼ User Profile";
            else
                g_dashboardView.btnMenuUserProfile.Text = "     ▲ User Profile";
        }
        #endregion

        #region Menus
        private void btnSubMenuUserProfile_Click(object sender, EventArgs e)
        {
            UserProfileView view = new UserProfileView(); // by Default
            this.OpenChildForm(view);
        }

        private void btnMenuHome_Click(object sender, EventArgs e)
        {
            HomeView view = new HomeView(); // by Default

            this.ShowHideSubMenus(false);
            this.OpenChildForm(view);
        }
        #endregion

        #region Hamburger Menu
        private void timerSideMenu_Tick(object sender, EventArgs e)
        {
            if(g_IsSideMenuCollapsed)
            {
                g_dashboardView.panelSideMenu.Width += 60;

                if (g_dashboardView.panelSideMenu.Width == g_dashboardView.panelSideMenu.MaximumSize.Width)
                {
                    g_dashboardView.timerSideMenu.Stop();
                    g_IsSideMenuCollapsed = false;
                }
            }
            else
            {
                g_dashboardView.panelSideMenu.Width -= 60;

                if (g_dashboardView.panelSideMenu.Width == g_dashboardView.panelSideMenu.MinimumSize.Width)
                {
                    g_dashboardView.timerSideMenu.Stop();
                    g_IsSideMenuCollapsed = true;
                }
            }
        }

        private void btnHamburgerMenu_Click(object sender, EventArgs e)
        {
            g_dashboardView.timerSideMenu.Start();

            this.SetMenuText();
        }

        private void SetMenuText()
        {
            if (g_IsSideMenuCollapsed || !g_HasInitialized)
            {
                // set texts (s)
                if (g_dashboardView.panelSubMenuUserProfile.Visible)
                    g_dashboardView.btnMenuUserProfile.Text = "     ▼ User Profile";
                else
                    g_dashboardView.btnMenuUserProfile.Text = "     ▲ User Profile";

                g_dashboardView.btnSubMenuLogout.Text = "     Logout";
                g_dashboardView.btnSubMenuUserProfile.Text = "     User Profile";
                // set texts (e)
            }
            else
            {
                // set texts (s)
                g_dashboardView.btnMenuUserProfile.Text = string.Empty;
                g_dashboardView.btnSubMenuLogout.Text = string.Empty;
                g_dashboardView.btnSubMenuUserProfile.Text = string.Empty;
                // set texts (e)
            }
        }
        #endregion
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    /// <summary>
    /// MJBB 20200713
    /// </summary>
    public class MovablePanel
    {
        private Form g_Form;

        private Panel[] g_Panels;
        private Panel g_Panel;

        private bool mouseDown;
        private Point lastLocation;

        /// <summary>
        /// Form => this is the container of the Panels where the Location will be based
        /// Panel => this is the view where mouse interacts 
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Panels"></param>
        public MovablePanel(Form Form, Panel[] Panels)
        {
            g_Form = Form;
            g_Panels = Panels;
            this.SetupPanel();
        }

        private void SetupPanel()
        {
            foreach(var panel in g_Panels)
            {
                g_Panel = panel;

                g_Panel.MouseDown -= Panel_MouseDown;
                g_Panel.MouseDown += Panel_MouseDown;

                g_Panel.MouseMove -= Panel_MouseMove;
                g_Panel.MouseMove += Panel_MouseMove;

                g_Panel.MouseUp -= Panel_MouseUp;
                g_Panel.MouseUp += Panel_MouseUp;
            }
        }

        /// <summary>
        /// Make window movable without FormBorder
        /// </summary>
        #region Make window movable without FormBorder
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                g_Form.Location = new Point(
                    (g_Form.Location.X - lastLocation.X) + e.X, (g_Form.Location.Y - lastLocation.Y) + e.Y);

                g_Form.Update();
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion
    }
}

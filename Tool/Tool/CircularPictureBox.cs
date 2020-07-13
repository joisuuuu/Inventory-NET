using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool.Tool
{
    // MJBB 20200713
    public class CircularPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Width);
            this.Region = new System.Drawing.Region(graphicsPath);
            base.OnPaint(pe);

            float penWidth = 10F;
            Pen myPen = new Pen(Color.FromArgb(255, 255, 255), penWidth);
            pe.Graphics.DrawEllipse(myPen, new RectangleF(new PointF(0, 0), new
            SizeF((float)(ClientSize.Width - 1), ClientSize.Height - 1)));
            myPen.Dispose();
        }
    }
}

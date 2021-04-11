using System.Drawing;
using System.Drawing.Drawing2D;

namespace Darknet.YoloV3.Extensions
{
    public static class GraphicsExtensions
    {
        /// <summary>
        /// g.DrawCircle(myPen, centerX, centerY, radius);
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        public static void DrawCircle(this Graphics g, Pen pen,
                                      float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        /// <summary>
        /// g.FillCircle(myBrush, centerX, centerY, radius);
        /// </summary>
        /// <param name="g"></param>
        /// <param name="brush"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        /// <summary>
        /// g.DrawRectangle(new Pen(Color.Black), r);
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        public static void DrawRotateRectangle(this Graphics g, Rectangle r, float angle)
        {
            using (Matrix m = new Matrix())
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                m.RotateAt(angle, new PointF(r.Left + (r.Width / 2),
                                          r.Top + (r.Height / 2)));
                g.Transform = m;
                g.DrawRectangle(Pens.Black, r);
                g.ResetTransform();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="xy1"></param>
        /// <param name="xy2"></param>
        /// <param name="xy3"></param>
        /// <param name="xy4"></param>
        /// <param name="color"></param>
        /// <param name="lineWidth"></param>
        public static void DrawRectangle(this Graphics g, PointF xy1, PointF xy2, PointF xy3, PointF xy4, Brush brush, float lineWidth = 1.0f)
        {
            var pen = new System.Drawing.Pen(brush, lineWidth);

            g.DrawLine(pen, xy1, xy2);
            g.DrawLine(pen, xy2, xy3);
            g.DrawLine(pen, xy3, xy4);
            g.DrawLine(pen, xy4, xy1);
        }
    }

}

using Darknet.YoloV3.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Darknet.YoloV3
{
    public class YoloDetection
    {
        /// <summary>
        /// order detections by (X,Y), and distinct
        /// to remove nearby detections
        /// </summary>
        /// <param name="detections"></param>
        /// <param name="unique"></param>
        /// <returns></returns>
        public static List<Detection> SortDetections(List<Detection> detections, bool unique = true)
        {
            detections = detections.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
            if (!unique) return detections;

            var tmp_point_xy = new Point(-1, -1);

            for (int i = 0, j = 0; i < detections.Count(); i++)
            {
                detections[i].ID = ++j;

                if (Math.Abs(detections[i].X - tmp_point_xy.X) < 20
                    && Math.Abs(detections[i].Y - tmp_point_xy.Y) < 20)
                {
                    detections.RemoveAt(i);
                    i--;
                }
                else
                {
                    tmp_point_xy.X = detections[i].X;
                    tmp_point_xy.Y = detections[i].Y;
                }
            }

            return detections;
        }

    }
}

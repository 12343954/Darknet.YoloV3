using Alturos.Yolo;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Darknet.YoloV3
{
    public static class GG
    {
        #region //YOLOv3 config
        public struct YOLOv3
        {
            public static bool Loaded = false;
            public static string BasePath = @"D:\Tensorflow2\darknet\build\darknet\x64\training\11\voc_custom";
            public static string ConfigFile = Path.Combine(BasePath, "yolov3_custom.cfg");
            public static string WeightsFile = Path.Combine(BasePath, @"backup\yolov3_custom_63000.weights");
            public static string NamesFile = Path.Combine(BasePath, "coco_custom.names");
            public static List<string> Names = File.ReadAllLines(NamesFile).ToList<string>();
            public static List<string> Colors = new List<string> {
                "#9b59b6",
                "#3498db",
                "#0095ff",
                "#e74c3c",
                "#34495e",
                "#2ecc71",
                "#f372bf",
                "#14733c",
                "#cc772e",
                "#2ea44f",
                "#2e3acc"
         };

            // use GPU 0
            public static GpuConfig YOLO_GpuConfig = new GpuConfig();
            //YOLO_GpuConfig.GpuIndex = 0;
        }
        #endregion
    }
}

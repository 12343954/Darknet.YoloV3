using Alturos.Yolo;
using Darknet.YoloV3.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darknet.YoloV3
{
    public partial class Form1 : Form
    {
        YoloWrapper YOLO;

        string imagePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Startup();
        }

        void Startup()
        {
            this.Text = $"C# YOLO v3 Test Custom pre-traning model (GPU) {Application.ProductVersion}";
            //button1.Enabled = false;
            button1.Text = "";

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            imagePath = Path.Combine(GG.YOLOv3.BasePath, @"labels\1297.jpg");
            pictureBox1.ImageLocation = imagePath;

            // async load yolo
            Task.Run(() =>
            {
                YoloV3();
            });
        }

        void YoloV3()
        {
            // use GPU 0
            var GpuConfig = new GpuConfig();
            GpuConfig.GpuIndex = 0;

            YOLO = new YoloWrapper(GG.YOLOv3.ConfigFile,
                                    GG.YOLOv3.WeightsFile,
                                    GG.YOLOv3.NamesFile,
                                    GpuConfig);

            button1.Invoke(new Action(() =>
            {
                button1.Image = null;
                button1.Text = "Detect";
            }));
        }

        void Detect()
        {
            if (YOLO == null)
            {
                MessageBox.Show("YOLO not init ...");
                return;
            }

            textBox1.Clear();



            //// #1 directly detect file is ok
            //var detections1 = YOLO.Detect(pictureBox1.ImageLocation);  // OK

            // #2 detect bytes , ERROR
            //var detections2 = YOLO.Detect(File.ReadAllBytes(pictureBox1.ImageLocation));            //ERROR: System.NotImplementedException: 'C++ dll compiled incorrectly'

            //// #3 detect memory bytes , ERROR
            //using (var mm = new MemoryStream())
            //{
            //    pictureBox1.Image.Save(mm, ImageFormat.Png);
            //    //pictureBox1.Image.Save(mm, pictureBox1.Image.RawFormat);
            //    var detections3 = YOLO.Detect(mm.ToArray());                // ERROR:  System.NotImplementedException: 'C++ dll compiled incorrectly'
            //}

            //// #4 detect OpenCvSharp new Mat() image , ERROR
            //// using OpenCvSharp;
            //var mm2 = new Mat(pictureBox1.ImageLocation, ImreadModes.AnyColor | ImreadModes.AnyDepth);
            //var detections4 = YOLO.Detect(mm2.ToBytes());           // ERROR:  System.NotImplementedException: 'C++ dll compiled incorrectly'


            var t1 = new Stopwatch();
            // if yolo loaded, then call detect method
            t1.Start();
            var detections = YOLO.Detect(pictureBox1.ImageLocation);
            t1.Stop();      // record the eta

            label3.Text = Math.Round(1000.0 / t1.ElapsedMilliseconds, 0).ToString();
            label4.Text = $"{t1.ElapsedMilliseconds}ms";

            // convert yolo model to my model
            var list = detections.Select(x => new Detection()
            {
                Type = x.Type,
                Confidence = x.Confidence,
                X = x.X,
                Y = x.Y,
                Width = x.Width,
                Height = x.Height
            }).ToList();

            // unique the result
            list = YoloDetection.SortDetections(list);

            var g = pictureBox1.CreateGraphics();

            // drawing all in threading
            Task.Run(() =>
            {
                DrawDrawBoxes(g, list);
            });
        }

        /// <summary>
        /// Drawing all informations
        /// </summary>
        /// <param name="g"></param>
        /// <param name="list"></param>
        void DrawDrawBoxes(Graphics g, List<Detection> list)
        {
            Invoke(new Action(() =>
            {
                pictureBox1.Refresh();
            }));
            var customColor = System.Drawing.Color.FromArgb(50, System.Drawing.Color.Yellow);
            var shadowBrush = new SolidBrush(customColor);
            var pen = new System.Drawing.Pen(System.Drawing.Color.Yellow, 2);

            var i = 0;

            foreach (var detect in list)
            {
                //Thread.Sleep(500);

                i++;
                var index = GG.YOLOv3.Names.FindIndex(x => x == detect.Type);
                var color = ColorTranslator.FromHtml(GG.YOLOv3.Colors[index]);
                pen.Color = color;
                var index2 = index + 1;
                if (index2 == GG.YOLOv3.Names.Count())
                {
                    index2 = 0;
                }
                var color2 = ColorTranslator.FromHtml(GG.YOLOv3.Colors[index2]);
                var brush = new SolidBrush(color);
                var brush2 = new SolidBrush(color2);


                // 1， draw bbox
                g.DrawRectangle(pen, detect.X, detect.Y, detect.Width, detect.Height);

                // 2，draw text background
                var title = $"{detect.ID} {detect.Type}";
                var font = new Font("Tahoma", 10);
                var bgSize = g.MeasureString(title, font);
                var rect = new RectangleF(detect.X, detect.Y, bgSize.Width, bgSize.Height);
                g.FillRectangle(brush, rect);

                // 3，draw name
                //var color2 = ColorTranslator.FromHtml(colors[index2]);
                //brush = new SolidBrush(color2);

                g.DrawString(
                    title,
                    font,
                    System.Drawing.Brushes.White,
                    new PointF(detect.X, detect.Y));

                Invoke(new Action(() =>
                {
                    textBox1.AppendText($"{detect.ID.ToString().PadRight(3, ' ')} {detect.Type.PadRight(16, ' ')} {detect.Confidence}");
                    textBox1.AppendText(Environment.NewLine);
                }));
            }
        }

        #region // Form Elements Events
        private void button1_Click(object sender, EventArgs e)
        {
            Detect();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            button1_Click(sender, null);
        }

        private void pictureBox1_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            using (var info = Image.FromFile(pictureBox1.ImageLocation))
            {
                this.Width = info.Width + 200;
                this.Height = info.Height + 210;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (YOLO != null)
            {
                YOLO.Dispose();
            }
        }
        #endregion
    }
}

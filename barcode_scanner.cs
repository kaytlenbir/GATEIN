using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ZXing;

namespace Gate_In
{
    public partial class barcode_scanner : Form
    {
        public barcode_scanner()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        public string barcode = null;

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                TxtBox_Barcode.Invoke(new MethodInvoker(delegate ()
                {
                    TxtBox_Barcode.Text = result.ToString();
                    submit_btn.Visible = true;
                    submit_btn_decor_pnl.Visible = true;
                    barcode = result.ToString();
                }));
            }
            pictureBox.Image = bitmap;
        }

        private Size oldSize;
        private void Form1_Load(object sender, EventArgs e) => oldSize = base.Size;

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            foreach (Control cnt in this.Controls)
                ResizeAll(cnt, base.Size);

            oldSize = base.Size;
        }
        private void ResizeAll(Control control, Size newSize)
        {
            int width = newSize.Width - oldSize.Width;
            control.Left += (control.Left * width) / oldSize.Width;
            control.Width += (control.Width * width) / oldSize.Width;

            int height = newSize.Height - oldSize.Height;
            control.Top += (control.Top * height) / oldSize.Height;
            control.Height += (control.Height * height) / oldSize.Height;
        }

        private void test()
        {
            this.Close();

            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                    videoCaptureDevice.Stop();
            }
            foreach (getting_car_details form in Application.OpenForms)
            {
                form.barcode_txt.Text = barcode;
                form.Show();
            }  
            
        }

        private void barcode_scanner_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                ComboBox_Camera.Items.Add(device.Name);
            ComboBox_Camera.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[ComboBox_Camera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            test();
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            foreach (getting_car_details form in Application.OpenForms)
            {
                form.Show();
            }
        }
    }
}
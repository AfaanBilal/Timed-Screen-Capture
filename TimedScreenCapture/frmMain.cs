using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TimedScreenCapture
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        
        Timer t;
        string dDir;
        int secondsInterval;
        
        private void T_Tick(object sender, EventArgs e)
        {
            takeScreenshot();
        }

        private void takeScreenshot()
        {
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            string scrShot = dDir + "scr_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_-_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".jpg";
            bmpScreenshot.Save(scrShot, ImageFormat.Jpeg);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            dDir = Path.GetDirectoryName(Application.ExecutablePath) + @"\_shots\";
            if (!Directory.Exists(dDir))
                Directory.CreateDirectory(dDir);

            // Hide the screenshot directory
            new DirectoryInfo(dDir).Attributes = FileAttributes.Hidden;

            if (Environment.GetCommandLineArgs().Length > 1)
                secondsInterval = Int32.Parse(Environment.GetCommandLineArgs()[1]);
            else
                secondsInterval = 1;

            t = new Timer();
            t.Tick += T_Tick;
            t.Interval = secondsInterval * 1000;
            t.Start();
        }
    }
}

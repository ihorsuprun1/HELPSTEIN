using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN_Frida
{
    class Screenshot
    {
        public string makeScreenshot()
        {
            string imageFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string screenshotFolder = imageFolder + "\\scr";
            if (!Directory.Exists(screenshotFolder))
            {
                System.IO.Directory.CreateDirectory(screenshotFolder);
            }

            // Determine the size of the "virtual screen", which includes all monitors.
            int screenLeft = SystemInformation.VirtualScreen.Left;
            int screenTop = SystemInformation.VirtualScreen.Top;
            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;
            //
            // Hide();
            System.Threading.Thread.Sleep(500);
            //
            DateTime dat1 = DateTime.Now;
            string strdate = dat1.ToString("yyyyMMdd-HHmmss");
            //Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string path = screenshotFolder + "\\" + strdate + ".png";
            using (Bitmap bmp = new Bitmap(screenWidth, screenHeight))
            {
                // Draw the screenshot into our bitmap.
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);
                }
                //Show();

                //BM.Save(path);
                bmp.Save(path);
                return path;
                // Do something with the Bitmap here, like save it to a file:

            }
        }
    }
}

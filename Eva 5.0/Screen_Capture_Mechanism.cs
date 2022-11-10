using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Eva_5._0
{
    internal class Screen_Capture_Mechanism
    {
        private static Random random_number_generator = new Random();

        public static async Task<bool> Screen_Capture_Initiator()
        {
            return await Screen_Capture();
        }

        private static Task<bool> Screen_Capture()
        {
            try
            {
                int screen_height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
                int screen_width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
                System.Drawing.Bitmap captureBitmap = new System.Drawing.Bitmap(screen_width, screen_height, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
                System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;
                System.Drawing.Graphics captureGraphics = System.Drawing.Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);







            File_Name_Generation:

                string file_name = "Eva_Capture" + random_number_generator.Next(int.MaxValue).ToString();

                string path_and_file_name = @"C:\Users\" + System.Environment.UserName + @"\Desktop\" + file_name + ".jpg";

                if (System.IO.File.Exists(path_and_file_name))
                {
                    goto File_Name_Generation;
                }





                captureBitmap.Save(path_and_file_name, System.Drawing.Imaging.ImageFormat.Jpeg);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }


    }
}

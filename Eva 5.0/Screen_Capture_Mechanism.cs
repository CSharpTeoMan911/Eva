using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

            File_Name_Generation:

                string file_name = "Eva_Capture" + random_number_generator.Next(int.MaxValue).ToString();

                string path_and_file_name = @"C:\Users\" + System.Environment.UserName + @"\Desktop\" + file_name + ".jpg";

                if (System.IO.File.Exists(path_and_file_name))
                {
                    goto File_Name_Generation;
                }


                Rectangle bounds = Screen.GetBounds(Point.Empty);

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }
                    bitmap.Save(path_and_file_name, System.Drawing.Imaging.ImageFormat.Jpeg);
                }


                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }


    }
}

using System;
using System.Drawing;
using System.Threading.Tasks;


namespace Eva_5._0
{


    /////////////////////////////////////////////////////////////////////////////
    ///                                                                       ///
    ///                   PRODUCT: EVA A.I. ASSISTANT                         ///
    ///                                                                       ///
    ///                   AUTHOR: TEODOR MIHAIL                               ///
    ///                                                                       ///
    ///                                                                       ///
    /// ANY UNAUTHORISED TRADEMARK USE OF THIS SOFTWARE IS PUNISHABLE BY LAW  ///
    ///                                                                       ///
    /// THE AUTHOR OF THIS SOFTWARE DOES NOT LET ANY PEOPLE PATENT OR USE     ///
    /// THIS PRODUCT'S TRADEMARK.                                             ///
    ///                                                                       ///
    /// DO NOT REMOVE THIS FILE HEADER                                        ///
    ///                                                                       ///
    /////////////////////////////////////////////////////////////////////////////



    internal class Screen_Capture_Mechanism
    {
        private static readonly Random random_number_generator = new Random();

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


                Rectangle bounds = System.Windows.Forms.Screen.GetBounds(Point.Empty);

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

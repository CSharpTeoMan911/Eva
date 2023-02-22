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

        protected static Task<bool> Screen_Capture()
        {
            try
            {

            // SECTION WHERE THE FILE NAME OF THE SCREENSHOT IS GENERATED
            //
            // [ BEGIN ]


            File_Name_Generation:




                // FILE NAME GENERATION USING RANDOM NUMBERS
                string file_name = "Eva_Capture" + random_number_generator.Next(int.MaxValue).ToString();





                // PATH GENERATION USING THE FILE NAME
                string path_and_file_name = @"C:\Users\" + System.Environment.UserName + @"\Desktop\" + file_name + ".jpg";





                // IF THE FILE NAME WITHIN THE SELECTED PATH EXISTS, GO TO 'File_Name_Generation' LABEL AND RESTART THE PROCEDURE
                if (System.IO.File.Exists(path_and_file_name))
                {
                    goto File_Name_Generation;
                }


            // [ END ]

                



                // ON THE USER INTERFACE'S THREAD, SET THE WINDOW STATE OF THE MAIN WINDOW AS MINIMISED
                // IN ORDER FOR THE APPLICATION NOT TO APPEAR IN THE SCREENSHOT
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    System.Windows.Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
                });





                // GET THE SCREEN DIMENSIONS USING POINTS ( 1 POINT = 1.3333 PIXELS )
                Rectangle bounds = System.Windows.Forms.Screen.GetBounds(Point.Empty);





                // ALLOCATE IN MEMORY A BITMAP OBJECT WITH THE WIDTH AND HEIGHT PROPRIETIES OF THE SCREEN
                // WITHIN A USING BLOCK. AFTER THE USING BLOCK FINISHED EXECUTING ITS CONTENTS, THE 
                // BITMAP OBJECT IS DEALLOCATED FROM THE MEMORY AUTOMATICALLY
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {




                    // ALLOCATE IN MEMORY A GRAPHICS OBJECT THAT IS USING AS A BUFFER THE BITMAP IMAGE.
                    // THE GRAPHICS OBJECT IS COPYING THE SCREEN AS AN IMAGE AND SAVES IT WITHIN THE 
                    // BITMAP OBJECT. AFTER THE USING BLOCK FINISHED EXECUTING ITS CONTENTS,THE 
                    // GRAPHICS OBJECT IS DEALLOCATED FROM THE MEMORY AUTOMATICALLY
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        // COPY THE SCREEN AS AN IMAGE USING THE SIZE OF THE SCREEN AS A PARAMETER
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);




                        // DISPOSE THE GRAPHICS OBJECT MANUALLY IN ORDER TO ENSURE THAT IT WAS DISPOSED PROPERLY
                        g.Dispose();
                    }




                    // SAVE THE BITMAP CONTENTS AS AN IMAGE AT A SET PATH AND FILE NAME WITH A SET IMAGE FORMAT.
                    bitmap.Save(path_and_file_name, System.Drawing.Imaging.ImageFormat.Jpeg);




                    // DISPOSE THE BITMAP OBJECT MANUALLY IN ORDER TO ENSURE THAT IT WAS DISPOSED PROPERLY
                    bitmap.Dispose();
                }





                // IF THE SCREENSHOT SCHEDULED TO BE SAVED AT THE SET PATH EXISTS, THE SCREENSHOT PROCEDURE
                // FINISHED. IF THE SCREENSHOT PROCEDURE FINISHED, ON THE USER INTERFACE'S THREAD SET THE
                // APPLICATION'S MAIN WINDOW STATE AS NORMAL.
                if (System.IO.File.Exists(path_and_file_name))
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        System.Windows.Application.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
                    });
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

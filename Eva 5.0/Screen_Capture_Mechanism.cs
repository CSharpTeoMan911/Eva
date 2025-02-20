using System;
using System.Drawing;
using System.Text;
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
        private static StringBuilder file_name = new StringBuilder();
        private static StringBuilder path_and_file_name = new StringBuilder();




        protected static async Task<bool> Screen_Capture()
        {
            try
            {

            // SECTION WHERE THE FILE NAME OF THE SCREENSHOT IS GENERATED
            //
            // [ BEGIN ]


            File_Name_Generation:
                string user_desktop_directory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                // FILE NAME GENERATION USING RANDOM NUMBERS
                file_name.Clear();

                file_name.Append("Eva_Capture");
                file_name.Append(random_number_generator.Next(int.MaxValue).ToString());





                // PATH GENERATION USING THE FILE NAME
                path_and_file_name.Clear();
                path_and_file_name.Append(user_desktop_directory);
                path_and_file_name.Append("\\");
                path_and_file_name.Append(file_name.ToString());
                path_and_file_name.Append(".jpg");




                // IF THE FILE NAME WITHIN THE SELECTED PATH EXISTS, GO TO 'File_Name_Generation' LABEL AND RESTART THE PROCEDURE
                if (System.IO.File.Exists(path_and_file_name.ToString()))
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
                Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

                try
                {
                    // ALLOCATE IN MEMORY A GRAPHICS OBJECT THAT IS USING AS A BUFFER THE BITMAP IMAGE.
                    // THE GRAPHICS OBJECT IS COPYING THE SCREEN AS AN IMAGE AND SAVES IT WITHIN THE 
                    // BITMAP OBJECT. AFTER THE USING BLOCK FINISHED EXECUTING ITS CONTENTS,THE 
                    // GRAPHICS OBJECT IS DEALLOCATED FROM THE MEMORY AUTOMATICALLY

                    Graphics g = Graphics.FromImage(bitmap);

                    try
                    {
                        // COPY THE SCREEN AS AN IMAGE USING THE SIZE OF THE SCREEN AS A PARAMETER
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);




                        System.IO.MemoryStream main_memory_stream = new System.IO.MemoryStream();

                        try
                        {
                            // SAVE THE BITMAP CONTENTS AS AN IMAGE AT A SET PATH AND FILE NAME WITH A SET IMAGE FORMAT.
                            bitmap.Save(main_memory_stream, System.Drawing.Imaging.ImageFormat.Jpeg);






                            System.IO.FileStream picture_file_writer = System.IO.File.Create(path_and_file_name.ToString());

                            try
                            {
                                byte[] picture_binary_data = main_memory_stream.ToArray();
                                await picture_file_writer.WriteAsync(picture_binary_data, 0, picture_binary_data.Length);
                                await picture_file_writer.FlushAsync();
                            }
                            catch
                            {
                                Normalize_The_Window();
                            }
                            finally
                            {
                                picture_file_writer?.Dispose();
                            }


                        }
                        catch
                        {
                            Normalize_The_Window();
                        }
                        finally
                        {
                            main_memory_stream?.Dispose();
                        }


                    }
                    catch
                    {
                        Normalize_The_Window();
                    }
                    finally
                    {
                        g?.Dispose();
                    }


                }
                catch
                {
                    Normalize_The_Window();
                }
                finally
                {
                    // DISPOSE THE BITMAP OBJECT MANUALLY IN ORDER TO ENSURE THAT IT WAS DISPOSED PROPERLY
                    bitmap?.Dispose();
                }
                

                





                // IF THE SCREENSHOT SCHEDULED TO BE SAVED AT THE SET PATH EXISTS, THE SCREENSHOT PROCEDURE
                // FINISHED. IF THE SCREENSHOT PROCEDURE FINISHED, ON THE USER INTERFACE'S THREAD SET THE
                // APPLICATION'S MAIN WINDOW STATE AS NORMAL.
                if (System.IO.File.Exists(path_and_file_name.ToString()))
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        Normalize_The_Window();
                    });
                }



                file_name.Clear();
                path_and_file_name.Clear();

                return true;
            }
            catch
            {
                return false;
            }
        }





        private static void Normalize_The_Window()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                System.Windows.Application.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
            });
        }


    }
}

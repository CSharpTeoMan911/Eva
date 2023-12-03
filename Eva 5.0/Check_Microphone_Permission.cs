using System;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace Eva_5._0
{
    internal class Check_Microphone_Permission
    {
      
        public async static Task<bool> Check_If_Microphone_Available()
        {
            try
            {
                // INITIATE A MEDIA CAPTURE SETTINGS OBJECT. THIS OBJECT MADE FROM THE "MediaCaptureInitializationSettings" CLASS IS USED
                // TO SET THE SETTINGS FOR CAPTURE OF VIDEO/AUDIO INFORMATION FROM THE HARDWARE OF THE MACHINE THROUGH THE OPERATING SYSTEM.
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();


                // SET THE CAPTURE MODE AS AUDIO
                settings.StreamingCaptureMode = StreamingCaptureMode.Audio;


                // SET THE AUDIO CAPTURE MODE AS SPEECH 
                settings.MediaCategory = MediaCategory.Speech;


                // CREATE AN OBJECT FROM THE "MediaCapture" CLASS AND INITIALISE IT USING THE SETTINGS SET IN THE
                // "MediaCaptureInitializationSettings" CLASS OBJECT
                MediaCapture capture = new MediaCapture();
                await capture.InitializeAsync(settings);


                // DISPOSE THE "MediaCapture" CLASS OBJECT FROM MEMORY
                capture.Dispose();

                return true;
            }
            catch
            {
                return false;
            }


            // IF NO ERROR OCCURED, THEN THE SPEECH RECOGNITION IS AVAILABLE
            // OTHERWISE SPEECH RECOGNITION IS NOT AVAILABLE
        }
    }
}

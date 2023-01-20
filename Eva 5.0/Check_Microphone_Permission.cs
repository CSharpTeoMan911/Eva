using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace Eva_5._0
{
    internal class Check_Microphone_Permission
    {
      
        protected async static Task<bool> Check_If_Microphone_Available()
        {
            try
            {
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
                settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
                settings.MediaCategory = MediaCategory.Speech;
                MediaCapture capture = new MediaCapture();
                await capture.InitializeAsync(settings);
                capture.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

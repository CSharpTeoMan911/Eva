using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Set_Process_As_Foreground
    {
        // Import the operating system's user32.dll in order to use the operating system's window focus functionality through the WinAPI
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        // Each time the class is initialised through its constructor, the integer pointer of the application's handle within the operating system is passed and set as the top window
        public Set_Process_As_Foreground(IntPtr Process_Window_Handle)
        {
            SetForegroundWindow(Process_Window_Handle);
        }
    }
}

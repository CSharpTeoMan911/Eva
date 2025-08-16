using System;
using System.Runtime.InteropServices;

namespace Eva_5._0
{
    internal class Recycle_Bine_Cleanup
    {
        [DllImport("Shell32.dll")]
        private static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, int dwFlags);

        protected static void Empty_Recycle_Bin()
        {
            SHEmptyRecycleBin(IntPtr.Zero, null, 1);
        }

    }
}

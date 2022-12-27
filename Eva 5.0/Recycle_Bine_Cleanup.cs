using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Recycle_Bine_Cleanup
    {
        [DllImport("Shell32.dll")]
        private static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, int dwFlags);

        protected static Task<bool> Empty_Recycle_Bin()
        {
            SHEmptyRecycleBin(IntPtr.Zero, null, 1);
            return Task.FromResult(true);
        }

    }
}

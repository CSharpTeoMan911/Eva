using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Process_Cache_Cleanup
    {
        [DllImport("psapi.dll")]
        private static extern int EmptyWorkingSet(IntPtr hwProc);

        protected static void Empty_Recycle_Bin(IntPtr hwProc)
        {
            int succeeded = EmptyWorkingSet(hwProc);
        }
    }
}

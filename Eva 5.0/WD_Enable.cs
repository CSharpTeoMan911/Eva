using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eva_5._0
{
    internal class WD_Enable
    {
        public static async Task<bool> CheckIfEnabled()
        {
            

            StringBuilder args_builder = new StringBuilder("/k \"\"");
            args_builder.Append(Environment.CurrentDirectory);
            args_builder.Append("\\win-x86\\WPF_WD_ALLOW.exe\" \"");
            args_builder.Append(Environment.CurrentDirectory);
            args_builder.Append("\"\"");

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = args_builder.ToString();
            process.StartInfo.UseShellExecute = true;
            process.Start();


            NamedPipeServerStream pipeServer = new NamedPipeServerStream("WD_PIPE", PipeDirection.In);
            pipeServer.WaitForConnection();

            byte[] buffer = new byte[Encoding.UTF8.GetBytes("T").Length];
            await pipeServer.ReadAsync(buffer, 0, buffer.Length);

            string result = Encoding.UTF8.GetString(buffer);

            if (result == "T")
            {
                await Settings.Set_Initial_Setup(true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

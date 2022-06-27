using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Check_User_Current_Administartive_Role
    {
        public static void Check_If_User_Is_Administartor()
        {
            System.Security.Principal.WindowsPrincipal account_prinicipal = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent());

            bool Is_Account_Administartor = account_prinicipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);

            if (Is_Account_Administartor == true)
            {
                Environment.Exit(0);
            }
        }


        ~Check_User_Current_Administartive_Role()
        { }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Check_Role
    {
        public Check_Role()
        {
            Check_User_Role();
        }

        private void Check_User_Role()
        {
            System.Security.Principal.WindowsPrincipal role = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent());

            bool Is_Account_Administartor = role.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);

            if (Is_Account_Administartor == true)
            {
                Environment.Exit(0);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
    
    
    internal class Check_Role
    {
        public Check_Role()
        {
            new Check_Role(true);
        }

        private Check_Role(bool check_current_user_role)
        {
            if(check_current_user_role == true)
            {
                Check_User_Role();
            }
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

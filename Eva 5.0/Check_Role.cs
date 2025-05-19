using System;

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
        public static void Check_User_Role()
        {
            // GETS THE CURRENT USER ON THE CURRENT WINDOWS OS SESSION AND ITS PRIVILEDGE LEVEL
            System.Security.Principal.WindowsPrincipal role = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent());
            bool Is_Account_Administartor = role.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);


            // IF USER THE CURRENT USER HAS ADMINISTRATOR PRIVILEDGE, THE APPLICATION SHUTS DOWN
            // THIS IS DONE TO ENSURE A HIGH LEVEL OF SECURITY DUE TO THE FACT THAT THE 
            // APPLICATION INTERACTS WITH LOW LEVEL SYSTEM FEATURES.
            if (Is_Account_Administartor == true)
            {
                Environment.Exit(0);
            }
        }
    }
}

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


    internal class Settings
    {


        private static bool Sound_Enabled = true;

        public static Task<bool> Get_Settings()
        {
            switch (Sound_Enabled)
            {
                case true:
                    return Task.FromResult(true);

                case false:
                    return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }


        public static Task<bool> Set_Settings(bool Option)
        {
            switch (Option)
            {
                case true:
                    Sound_Enabled = true;
                    break;

                case false:
                    Sound_Enabled = false;
                    break;
            }

            return Task.FromResult(true);
        }

        ~Settings()
        { }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Settings
    {


        private static bool Sound_Enabled = true;

        public Task<bool> Get_Settings()
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


        public Task<bool> Set_Settings(bool Option)
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
    }

    internal class Setting_File_Template
    {
        public string Sound = "Enabled";
    }
}

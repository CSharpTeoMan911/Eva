using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Settings
    {
        public Task<string> Get_Settings()
        {
            switch (System.IO.File.Exists(@"EvaSettings.json"))
            {
                case true:
                    try
                    {
                        using (var Settings_File = new System.IO.StreamReader("EvaSettings.json"))
                        {
                            var SerialisedFile = Settings_File.ReadToEnd();
                            Settings_File.Close();
                            Settings_File.Dispose();

                            dynamic DeserialisedFile = Newtonsoft.Json.JsonConvert.DeserializeObject(SerialisedFile);
                            return Task.FromResult((string)DeserialisedFile["Sound"]);
                        }
                    }
                    catch { }
                    break;

                case false:
                    try
                    {
                        var Template = new Setting_File_Template();

                        dynamic SerialisedFile = Newtonsoft.Json.JsonConvert.SerializeObject(Template, Newtonsoft.Json.Formatting.Indented);
                        System.IO.File.WriteAllText("EvaSettings.json", SerialisedFile);
                    }
                    catch { }
                    break;
            }

            return Task.FromResult("Enabled");
        }


        public Task<bool> Set_Settings(string Option)
        {
            switch (System.IO.File.Exists(@"EvaSettings.json"))
            {
                case true:
                    try
                    {
                        using (var Settings_File = new System.IO.StreamReader("EvaSettings.json"))
                        {
                            var SerialisedFile = Settings_File.ReadToEnd();
                            Settings_File.Close();
                            Settings_File.Dispose();

                            dynamic DeserialisedFile = Newtonsoft.Json.JsonConvert.DeserializeObject(SerialisedFile);
                            DeserialisedFile["Sound"] = Option;

                            dynamic ReSerialisedFile = Newtonsoft.Json.JsonConvert.SerializeObject(DeserialisedFile, Newtonsoft.Json.Formatting.Indented);

                            using (var Write_Settings_File = new System.IO.StreamWriter("EvaSettings.json"))
                            {

                                Write_Settings_File.Write(ReSerialisedFile);
                                Write_Settings_File.Flush();
                                Write_Settings_File.Close();
                                Write_Settings_File.Dispose();
                            }
                        }
                    }
                    catch { }
                    break;

                case false:
                    try
                    {
                        var Template = new Setting_File_Template();

                        dynamic SerialisedFile = Newtonsoft.Json.JsonConvert.SerializeObject(Template, Newtonsoft.Json.Formatting.Indented);
                        System.IO.File.WriteAllText("EvaSettings.json", SerialisedFile);
                    }
                    catch { }
                    break;
            }

            return Task.FromResult(false);
        }
    }

    internal class Setting_File_Template
    {
        public string Sound = "Enabled";
    }
}

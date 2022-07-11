using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eva_5._0
{
    internal class HT__KY_CLS:MainWindow
    {

        private static mrousavy.HotKey ctr_e__hotKey;

        private static mrousavy.HotKey alt_e__hotkey;



        public static Task<bool> REG____HT__KY()
        {
            REG____HT__KY______EXEC();

            return Task.FromResult(true);
        }


        public static Task<bool> UN_REG__E____HT__KY()
        {
            UN_REG____HT__KY______EXEC();

            return Task.FromResult(true);
        }





        private static void REG____HT__KY______EXEC()
        {
           ctr_e__hotKey = new mrousavy.HotKey(System.Windows.Input.ModifierKeys.Control, System.Windows.Input.Key.E, System.Windows.Window.GetWindow(Application.Current.MainWindow), (hotkey) => { MainWindow.Initiate_Recogniser(); } );

           alt_e__hotkey = new mrousavy.HotKey(System.Windows.Input.ModifierKeys.Alt, System.Windows.Input.Key.E, System.Windows.Window.GetWindow(Application.Current.MainWindow), (hotkey) => { MainWindow.Initiate_Recogniser(); });


        }



        private static void UN_REG____HT__KY______EXEC()
        {
            ctr_e__hotKey.Dispose();

            alt_e__hotkey.Dispose();
        }


    }
}

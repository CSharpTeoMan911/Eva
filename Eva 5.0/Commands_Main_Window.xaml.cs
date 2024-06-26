﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for Commands_Main_Window.xaml
    /// </summary>
    public partial class Commands_Main_Window : Window
    {

        private SettingsWindow.OpenSpeech openSpeech;

        public Commands_Main_Window()
        {
            InitializeComponent();
        }

        public Commands_Main_Window(SettingsWindow.OpenSpeech openSpeech_)
        {
            InitializeComponent();
            this.openSpeech = openSpeech_;
        }

        private void Move_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenApplications(object sender, RoutedEventArgs e)
        {
            Commands_Customisation commands_Customisation = new Commands_Customisation(Commands_Customisation.Option.OpenApplications, openSpeech);
            commands_Customisation.ShowDialog();
        }

        private void CloseApplications(object sender, RoutedEventArgs e)
        {
            Commands_Customisation commands_Customisation = new Commands_Customisation(Commands_Customisation.Option.CloseApplications, openSpeech);
            commands_Customisation.ShowDialog();
        }

        private void SearchOnWebApplications(object sender, RoutedEventArgs e)
        {
            Commands_Customisation commands_Customisation = new Commands_Customisation(Commands_Customisation.Option.SearchContentOnWebApplications, openSpeech);
            commands_Customisation.ShowDialog();
        }
    }
}

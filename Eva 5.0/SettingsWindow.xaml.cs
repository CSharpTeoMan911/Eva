﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>



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


    public partial class SettingsWindow : Window
    {
        private System.Timers.Timer AnimationAndFunctionalityTimer;



        private bool WindowIsClosing;



        private double Wheel1Angle;
        private double Wheel2Angle;


        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;
        

        private bool SwitchSecodaryOffsets;
        private double GradientArithmeticSecondaryOffsets;


        private bool AnimationAndFunctionalityTimerDisposed;



        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void MinimiseTheWindow(object sender, RoutedEventArgs e)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        this.WindowState = WindowState.Minimized;

                    }

                }

            }
        }


        private void CloseTheWindow(object sender, RoutedEventArgs e)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        this.Close();

                    }

                }

            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowIsClosing = true;
            App.SettingsWindowOpen = false;
        }

        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            if (WindowIsClosing == false)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        this.DragMove();

                    }

                }

            }
        }

        private async void SettingsWindowLoaded(object sender, RoutedEventArgs e)
        {
            


            AnimationAndFunctionalityTimer = new System.Timers.Timer();
            AnimationAndFunctionalityTimer.Disposed += AnimationAndFunctionalityTimer_Disposed;
            AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
            AnimationAndFunctionalityTimer.Interval = 10;
            AnimationAndFunctionalityTimer.Start();

            this.Topmost = true;


            bool SoundOrOff = await Settings.Get_Sound_Settings();

            switch (SoundOrOff)
            {
                case true:
                    SoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                    SoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                    MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                    SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");


                    break;

                case false:
                    SoundOffButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                    SoundOnButton.Background = new SolidColorBrush(Colors.Transparent);

                    SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                    MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                    break;
            }

        }

        private void AnimationAndFunctionalityTimer_Disposed(object sender, EventArgs e)
        {
            AnimationAndFunctionalityTimerDisposed = true;
        }

        private void AnimationAndFunctionalityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            switch (WindowIsClosing)
            {

                case true:
                    try
                    {
                        AnimationAndFunctionalityTimer.Stop();
                    }
                    catch { }
                    break;

                case false:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {

                        case true:
                            try
                            {
                                AnimationAndFunctionalityTimer.Stop();
                            }
                            catch { }
                            break;

                        case false:

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                switch (Application.Current.MainWindow == null)
                                {

                                    case true:
                                        try
                                        {
                                            AnimationAndFunctionalityTimer.Stop();
                                        }
                                        catch { }
                                        this.Close();
                                        break;

                                    case false:

                                        
                                        switch (Wheel1Angle <= 360)
                                        {
                                            case true:

                                                Wheel1Angle++;
                                                break;

                                            case false:
                                                Wheel1Angle = 0;
                                                break;
                                        }

                                        switch (Wheel2Angle <= 360)
                                        {
                                            case true:

                                                Wheel2Angle++;
                                                break;

                                            case false:
                                                Wheel2Angle = 0;
                                                break;
                                        }


                                        RotateTransform Wheel1Rotate = new RotateTransform()
                                        {
                                            Angle = Wheel1Angle
                                        };
                                        

                                        RotateTransform Wheel2Rotate = new RotateTransform
                                        {
                                            Angle = Wheel2Angle
                                        };

                                        Wheel1.RenderTransform = Wheel1Rotate;

                                        Wheel2.RenderTransform = Wheel2Rotate;


                                        switch (SwitchWindowOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticWindowOffset <= 300)
                                                {
                                                    case true:
                                                        GradientArithmeticWindowOffset++;
                                                        WindowOffset.Offset -= 0.0025;
                                                        break;

                                                    case false:
                                                        SwitchWindowOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticWindowOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticWindowOffset--;
                                                        WindowOffset.Offset += 0.0025;
                                                        break;

                                                    case false:
                                                        SwitchWindowOffset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        switch (SwitchSecodaryOffsets)
                                        {
                                            case false:

                                                switch (GradientArithmeticSecondaryOffsets <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticSecondaryOffsets++;
                                                        CloseButtonOffset.Offset += 0.025;
                                                        MinimiseTheWindowOffset.Offset += 0.025;
                                                        MenuGear2Offset.Offset += 0.025;
                                                        SettingTitleOffset.Offset += 0.01;
                                                        SoundButtonOffset.Offset -= 0.025;
                                                        MuteButtonOffset.Offset -= 0.025;
                                                        InstructionManualTextBlockOffset.Offset -= 0.01;
                                                        ChatGPTApiKeyOffset.Offset -= 0.01;
                                                        break;

                                                    case false:
                                                        SwitchSecodaryOffsets = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticSecondaryOffsets > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticSecondaryOffsets--;
                                                        CloseButtonOffset.Offset -= 0.025;
                                                        MinimiseTheWindowOffset.Offset -= 0.025;
                                                        MenuGear2Offset.Offset -= 0.025;
                                                        SettingTitleOffset.Offset -= 0.01;
                                                        SoundButtonOffset.Offset += 0.025;
                                                        MuteButtonOffset.Offset += 0.025;
                                                        InstructionManualTextBlockOffset.Offset += 0.01;
                                                        ChatGPTApiKeyOffset.Offset += 0.01;
                                                        break;

                                                    case false:
                                                        SwitchSecodaryOffsets = false;
                                                        break;
                                                }
                                                break;
                                        }
                                        break;

                                }
                            });
                            break;
                    }
                    break;
            }
        }

        private async void SoundOn(object sender, RoutedEventArgs e)
        {
            if (App.SettingsWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        await Settings.Set_Sound_Settings(true);

                        SoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                        SoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                        MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                        SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");

                    }

                }

            }
        }

        private async void SoundOff(object sender, RoutedEventArgs e)
        {
            if (App.SettingsWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        await Settings.Set_Sound_Settings(false);

                        SoundOffButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                        SoundOnButton.Background = new SolidColorBrush(Colors.Transparent);

                        SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                        MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");

                    }

                }

            }
        }

        private void OpenInstructionManual(object sender, RoutedEventArgs e)
        {
            if (App.SettingsWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {


                        if (App.InstructionManualOpen != true)
                        {

                            App.InstructionManualOpen = true;
                            InstructionManual instructionManual = new InstructionManual();
                            instructionManual.Show();

                        }

                    }

                }

            }

        }


        private void Set_Chat_Gpt_Api_Key(object sender, RoutedEventArgs e)
        {
            if (App.SettingsWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {


                        if (App.InstructionManualOpen != true)
                        {

                            Chat_GPT_API_Key_Settup_Window chat_GPT_API_Key_Settup_Window = new Chat_GPT_API_Key_Settup_Window();
                            chat_GPT_API_Key_Settup_Window.ShowDialog();

                        }

                    }

                }

            }
        }




        ~SettingsWindow()
        {
            if (AnimationAndFunctionalityTimerDisposed == false)
            {
                if (AnimationAndFunctionalityTimer != null)
                {
                    AnimationAndFunctionalityTimer.Dispose();
                }
            }

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced);
        }
    }
}

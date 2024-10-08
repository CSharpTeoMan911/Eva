﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for InstructionManual.xaml
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


    public partial class InstructionManual : Window
    {

        private System.Timers.Timer AnimationTimer;
        private byte NormalisedOrMaximised;
        private bool TimerDisposed;


        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;

        private bool SwitchSecondaryOffsets;
        private double GradientArithmeticSecondaryOffsets;

        public InstructionManual()
        {
            InitializeComponent();
        }

        private void InstructionManualWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.InstructionManualOpen = false;
        }

        private void InstructionManualWindowLoaded(object sender, RoutedEventArgs e)
        {
            AnimationTimer = new System.Timers.Timer();
            AnimationTimer.Disposed += AnimationTimer_Disposed;
            AnimationTimer.Elapsed += AnimationTimer_Elapsed;
            AnimationTimer.Interval = 10;
            AnimationTimer.Start();

            this.Topmost = true;
        }

        private void AnimationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (App.InstructionManualOpen)
            {
                case false:
                    try
                    {
                        AnimationTimer.Stop();
                    }
                    catch { }
                    break;

                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case true:
                            try
                            {
                                AnimationTimer.Stop();
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
                                            AnimationTimer.Stop();
                                        }
                                        catch { }

                                        break;

                                    case false:

                                        if (App.Application_Error_Shutdown)
                                        {
                                            try
                                            {
                                                if (AnimationTimer != null)
                                                {
                                                    AnimationTimer.Stop();
                                                    this.Close();
                                                }
                                            }
                                            catch { }
                                        }

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


                                        switch (SwitchSecondaryOffsets)
                                        {
                                            case false:

                                                switch (GradientArithmeticSecondaryOffsets <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticSecondaryOffsets++;

                                                        CloseButtonOffset.Offset += 0.025;
                                                        NormaliseOrMaximiseButtonOffset.Offset += 0.025;
                                                        MinimiseTheWindowOffset.Offset += 0.025;
                                                        InstructionManualTitleTextBlockOffset.Offset += 0.025;
                                                        AboutEvaTextBlockOffset.Offset += 0.025;
                                                        AboutEvaContentTextBlockOffset.Offset += 0.025;
                                                        UserInstructionsTextBlockOffset.Offset += 0.025;
                                                        UserInstructionsContentTextBlockOffset.Offset += 0.025;
                                                        CommandsTextBlockOffset.Offset += 0.025;
                                                        CommandsContentTextBlockOffset.Offset += 0.025;
                                                        TroubleshootingTextBlockOffset.Offset += 0.025;
                                                        TroubleshootingContentTextBlockOffset1.Offset += 0.025;
                                                        ExtraFeaturesTextBlockOffset.Offset += 0.025;
                                                        ExtraFeaturesContentTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchSecondaryOffsets = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticSecondaryOffsets > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticSecondaryOffsets--;

                                                        CloseButtonOffset.Offset -= 0.025;
                                                        NormaliseOrMaximiseButtonOffset.Offset -= 0.025;
                                                        MinimiseTheWindowOffset.Offset -= 0.025;
                                                        InstructionManualTitleTextBlockOffset.Offset -= 0.025;
                                                        AboutEvaTextBlockOffset.Offset -= 0.025;
                                                        AboutEvaContentTextBlockOffset.Offset -= 0.025;
                                                        UserInstructionsTextBlockOffset.Offset -= 0.025;
                                                        UserInstructionsContentTextBlockOffset.Offset -= 0.025;
                                                        CommandsTextBlockOffset.Offset -= 0.025;
                                                        CommandsContentTextBlockOffset.Offset -= 0.025;
                                                        TroubleshootingTextBlockOffset.Offset -= 0.025;
                                                        TroubleshootingContentTextBlockOffset1.Offset -= 0.025;
                                                        ExtraFeaturesTextBlockOffset.Offset -= 0.025;
                                                        ExtraFeaturesContentTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchSecondaryOffsets = false;
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

        private void AnimationTimer_Disposed(object sender, EventArgs e)
        {
            TimerDisposed = true;
        }

        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            if (App.InstructionManualOpen == true)
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

        private void MinimiseTheWindow(object sender, RoutedEventArgs e)
        {
            if (App.InstructionManualOpen == true)
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


        private void NormaliseOrMaximiseTheWindow(object sender, RoutedEventArgs e)
        {
            if (App.InstructionManualOpen == true)
            {

                    if (Application.Current.Dispatcher.HasShutdownStarted == false)
                    {

                            if (Application.Current.MainWindow != null)
                            {

                                    NormalisedOrMaximised++;

                                    switch (NormalisedOrMaximised)
                                    {
                                        case 1:
                                            NormaliseOrMaximiseTheWindowButton.Content = "\xEF2F";
                                            this.WindowState = WindowState.Maximized;
                                            break;

                                        case 2:
                                            NormaliseOrMaximiseTheWindowButton.Content = "\xEF2E";
                                            this.WindowState = WindowState.Normal;
                                            NormalisedOrMaximised = 0;
                                            break;
                                    }

                            }

                    }

            }
        }

        private void CloseTheWindow(object sender, RoutedEventArgs e)
        {
            if (App.InstructionManualOpen == true)
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

        private void Navigate_To_Eva_Project(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void Navigate_To_Commands_Customisation(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private async void Open_Settings_At_Speech_Recognition(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:speech"));
        }


        private void Window_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            if (App.InstructionManualOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        Rect geometry = new Rect();

                        geometry.Height = this.Height;
                        geometry.Width = this.Width;

                        Instruction_Manual_Geometry.Rect = geometry;

                    }

                }

            }
        }



        ~InstructionManual()
        {
            if (TimerDisposed)
            {
                if (AnimationTimer != null)
                {
                    AnimationTimer.Dispose();
                }
            }
        }
    }
}

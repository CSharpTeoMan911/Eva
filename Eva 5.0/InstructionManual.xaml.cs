using System;
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
    /// Interaction logic for InstructionManual.xaml
    /// </summary>
    public partial class InstructionManual : Window
    {

        private System.Timers.Timer AnimationTimer;
        private byte NormalisedOrMaximised;
        private bool TimerDisposed;


        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;

        private bool SwitchOffsetCloseButtonOffset;
        private double GradientArithmeticCloseButtonOffset;

        private bool SwitchOffsetNormaliseOrMaximiseButtonOffset;
        private double GradientArithmeticNormaliseOrMaximiseButtonOffset;

        private bool SwitchOffsetMinimiseTheWindowOffset;
        private double GradientArithmeticMinimiseTheWindowOffset;

        private bool SwitchOffsetInstructionManualTitleTextBlockOffset;
        private double GradientArithmeticInstructionManualTitleTextBlockOffset;

        private bool SwitchOffsetAboutEvaTextBlockOffset;
        private double GradientArithmeticAboutEvaTextBlockOffset;

        private bool SwitchOffsetAboutEvaContentTextBlockOffset;
        private double GradientArithmeticAboutEvaContentTextBlockOffset;

        private bool SwitchOffsetUserInstructionsTextBlockOffset;
        private double GradientArithmeticUserInstructionsTextBlockOffset;

        private bool SwitchOffsetUserInstructionsContentTextBlockOffset;
        private double GradientArithmeticUserInstructionsContentTextBlockOffset;

        private bool SwitchOffsetCommandsTextBlockOffset;
        private double GradientArithmeticCommandsTextBlockOffset;

        private bool SwitchOffsetCommandsContentTextBlockOffset;
        private double GradientArithmeticCommandsContentTextBlockOffset;

        private bool SwitchOffsetTroubleshootingTextBlockOffset;
        private double GradientArithmeticTroubleshootingTextBlockOffset;

        private bool SwitchOffsetTroubleshootingContentTextBlockOffset1;
        private double GradientArithmeticTroubleshootingContentTextBlockOffset1;

        private bool SwitchOffsetTroubleshootingContentTextBlockOffset2;
        private double GradientArithmeticTroubleshootingContentTextBlockOffset2;


        private bool SwitchOffsetTroubleshootingContentTextBlockOffset3;
        private double GradientArithmeticTroubleshootingContentTextBlockOffset3;

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

                                        this.Close();

                                        break;

                                    case false:

                                        switch (App.ErrorAppShutdown)
                                        {
                                            case true:
                                                try
                                                {
                                                    AnimationTimer.Stop();
                                                }
                                                catch { }

                                                this.Close();
                                                break;
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


                                        switch (SwitchOffsetCloseButtonOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticCloseButtonOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticCloseButtonOffset++;
                                                        CloseButtonOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCloseButtonOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticCloseButtonOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticCloseButtonOffset--;
                                                        CloseButtonOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCloseButtonOffset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetNormaliseOrMaximiseButtonOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticNormaliseOrMaximiseButtonOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticNormaliseOrMaximiseButtonOffset++;
                                                        NormaliseOrMaximiseButtonOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetNormaliseOrMaximiseButtonOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticNormaliseOrMaximiseButtonOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticNormaliseOrMaximiseButtonOffset--;
                                                        NormaliseOrMaximiseButtonOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetNormaliseOrMaximiseButtonOffset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        switch (SwitchOffsetMinimiseTheWindowOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticMinimiseTheWindowOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticMinimiseTheWindowOffset++;
                                                        MinimiseTheWindowOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMinimiseTheWindowOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticMinimiseTheWindowOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticMinimiseTheWindowOffset--;
                                                        MinimiseTheWindowOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMinimiseTheWindowOffset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        switch (SwitchOffsetInstructionManualTitleTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticInstructionManualTitleTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticInstructionManualTitleTextBlockOffset++;
                                                        InstructionManualTitleTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetInstructionManualTitleTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticInstructionManualTitleTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticInstructionManualTitleTextBlockOffset--;
                                                        InstructionManualTitleTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetInstructionManualTitleTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        switch (SwitchOffsetAboutEvaTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticAboutEvaTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticAboutEvaTextBlockOffset++;
                                                        AboutEvaTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetAboutEvaTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticAboutEvaTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticAboutEvaTextBlockOffset--;
                                                        AboutEvaTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetAboutEvaTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }




                                        switch (SwitchOffsetAboutEvaContentTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticAboutEvaContentTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticAboutEvaContentTextBlockOffset++;
                                                        AboutEvaContentTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetAboutEvaContentTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticAboutEvaContentTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticAboutEvaContentTextBlockOffset--;
                                                        AboutEvaContentTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetAboutEvaContentTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetUserInstructionsTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticUserInstructionsTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticUserInstructionsTextBlockOffset++;
                                                        UserInstructionsTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetUserInstructionsTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticUserInstructionsTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticUserInstructionsTextBlockOffset--;
                                                        UserInstructionsTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetUserInstructionsTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }



                                        switch (SwitchOffsetUserInstructionsContentTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticUserInstructionsContentTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticUserInstructionsContentTextBlockOffset++;
                                                        UserInstructionsContentTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetUserInstructionsContentTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticUserInstructionsContentTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticUserInstructionsContentTextBlockOffset--;
                                                        UserInstructionsContentTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetUserInstructionsContentTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }



                                        switch (SwitchOffsetCommandsTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticCommandsTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticCommandsTextBlockOffset++;
                                                        CommandsTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCommandsTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticCommandsTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticCommandsTextBlockOffset--;
                                                        CommandsTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCommandsTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }



                                        switch (SwitchOffsetCommandsContentTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticCommandsContentTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticCommandsContentTextBlockOffset++;
                                                        CommandsContentTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCommandsContentTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticCommandsContentTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticCommandsContentTextBlockOffset--;
                                                        CommandsContentTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetCommandsContentTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }



                                        switch (SwitchOffsetTroubleshootingTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticTroubleshootingTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingTextBlockOffset++;
                                                        TroubleshootingTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticTroubleshootingTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingTextBlockOffset--;
                                                        TroubleshootingTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingTextBlockOffset = false;
                                                        break;
                                                }
                                                break;
                                        }



                                        switch (SwitchOffsetTroubleshootingContentTextBlockOffset1)
                                        {
                                            case false:

                                                switch (GradientArithmeticTroubleshootingContentTextBlockOffset1 <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingContentTextBlockOffset1++;
                                                        TroubleshootingContentTextBlockOffset1.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingContentTextBlockOffset1 = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticTroubleshootingContentTextBlockOffset1 > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingContentTextBlockOffset1--;
                                                        TroubleshootingContentTextBlockOffset1.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingContentTextBlockOffset1 = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetTroubleshootingContentTextBlockOffset2)
                                        {
                                            case false:

                                                switch (GradientArithmeticTroubleshootingContentTextBlockOffset2 <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingContentTextBlockOffset2++;
                                                        TroubleshootingContentTextBlockOffset2.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingContentTextBlockOffset2 = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticTroubleshootingContentTextBlockOffset2 > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingContentTextBlockOffset2--;
                                                        TroubleshootingContentTextBlockOffset2.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingContentTextBlockOffset2 = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetTroubleshootingContentTextBlockOffset3)
                                        {
                                            case false:

                                                switch (GradientArithmeticTroubleshootingContentTextBlockOffset3 <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingContentTextBlockOffset3++;
                                                        TroubleshootingContentTextBlockOffset3.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingContentTextBlockOffset3 = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticTroubleshootingContentTextBlockOffset3 > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticTroubleshootingContentTextBlockOffset3--;
                                                        TroubleshootingContentTextBlockOffset3.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetTroubleshootingContentTextBlockOffset3 = false;
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
            switch (App.InstructionManualOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {

                                case false:

                                    this.DragMove();
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void MinimiseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (App.InstructionManualOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {

                                case false:

                                    this.WindowState = WindowState.Minimized;
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void NormaliseOrMaximiseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (App.InstructionManualOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {

                                case false:

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
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void CloseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (App.InstructionManualOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {

                                case false:

                                    this.Close();
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        ~InstructionManual()
        {
            switch (TimerDisposed)
            {
                case false:

                    switch (AnimationTimer == null)
                    {
                        case false:

                            AnimationTimer.Dispose();
                            break;
                    }
                    break;
            }
        }

        private void Navigate_To_Eva_Project(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void Open_Control_Panel_At_Speech_Recognition(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            using (var ControlPanel = new System.Diagnostics.Process())
            {
                ControlPanel.StartInfo.FileName = "control.exe";
                ControlPanel.StartInfo.WorkingDirectory = Environment.SystemDirectory + "control.exe";
                ControlPanel.StartInfo.Arguments = "/name Microsoft.SpeechRecognition";
                ControlPanel.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                ControlPanel.StartInfo.UseShellExecute = true;
                ControlPanel.Start();
            }

        }
    }
}

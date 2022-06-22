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

        private bool SwitchOffsetInstructionManualContentTextBlockOffset;
        private double GradientArithmeticInstructionManualContentTextBlockOffset;

        private bool SwitchOffsetInstructionManualTitleTextBlockOffset;
        private double GradientArithmeticInstructionManualTitleTextBlockOffset;

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

                                        switch (SwitchOffsetInstructionManualContentTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticInstructionManualContentTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticInstructionManualContentTextBlockOffset++;
                                                        InstructionManualContentTextBlockOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetInstructionManualContentTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticInstructionManualContentTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticInstructionManualContentTextBlockOffset--;
                                                        InstructionManualContentTextBlockOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetInstructionManualContentTextBlockOffset = false;
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
    }
}

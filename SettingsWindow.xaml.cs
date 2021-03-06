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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private bool WindowIsClosing;
        private System.Timers.Timer AnimationAndFunctionalityTimer;
        private double Wheel1Angle;
        private double Wheel2Angle;

        private bool SwitchOffsetCloseButtonOffset;
        private double GradientArithmeticCloseButtonOffset;

        private bool SwitchOffsetMinimiseTheWindowOffset;
        private double GradientArithmeticMinimiseTheWindowOffset;

        private bool SwitchOffsetMenuGear1Offset;
        private double GradientArithmeticMenuGear1Offset;

        private bool SwitchOffsetMenuGear2Offset;
        private double GradientArithmeticMenuGear2Offset;

        private bool SwitchOffsetSettingTitleOffset;
        private double GradientArithmeticSettingTitleOffset;

        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;

        private bool SwitchOffsetSoundButtonOffset;
        private double GradientArithmeticSoundButtonOffset;

        private bool SwitchOffsetMuteButtonOffset;
        private double GradientArithmeticMuteButtonOffset;

        private bool SwitchInstructionManualTextBlockOffset;
        private double GradientArithmeticInstructionManualTextBlockOffset;

        private bool AnimationAndFunctionalityTimerDisposed;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void MinimiseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (WindowIsClosing)
            {
                case false:

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


        private void CloseTheWindow(object sender, RoutedEventArgs e)
        {
            switch (WindowIsClosing)
            {
                case false:

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

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowIsClosing = true;
            App.SettingsWindowOpen = false;
        }

        private void MoveTheWindow(object sender, MouseButtonEventArgs e)
        {
            switch (WindowIsClosing)
            {
                case false:

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

        private void SettingsWindowLoaded(object sender, RoutedEventArgs e)
        {
            AnimationAndFunctionalityTimer = new System.Timers.Timer();
            AnimationAndFunctionalityTimer.Disposed += AnimationAndFunctionalityTimer_Disposed;
            AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
            AnimationAndFunctionalityTimer.Interval = 10;
            AnimationAndFunctionalityTimer.Start();



            var settings = new Settings();
            var SoundOrOff = settings.Get_Settings().Result;

            switch (SoundOrOff)
            {
                case "Enabled":
                    SoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                    SoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                    MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                    SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");


                    break;

                case "Disabled":
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

                                        switch (App.ErrorAppShutdown)
                                        {
                                            case true:
                                                try
                                                {
                                                    AnimationAndFunctionalityTimer.Stop();
                                                }
                                                catch { }

                                                this.Close();
                                                break;
                                        }

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


                                        RotateTransform Wheel1Rotate = new RotateTransform();
                                        Wheel1Rotate.Angle = Wheel1Angle;

                                        RotateTransform Wheel2Rotate = new RotateTransform();
                                        Wheel2Rotate.Angle = Wheel2Angle;

                                        Wheel1.RenderTransform = Wheel1Rotate;

                                        Wheel2.RenderTransform = Wheel2Rotate;

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

                                        switch (SwitchOffsetMenuGear1Offset)
                                        {
                                            case false:

                                                switch (GradientArithmeticMenuGear1Offset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticMenuGear1Offset++;
                                                        MenuGear1Offset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMenuGear1Offset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticMenuGear1Offset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticMenuGear1Offset--;
                                                        MenuGear1Offset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMenuGear1Offset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchOffsetMenuGear2Offset)
                                        {
                                            case false:

                                                switch (GradientArithmeticMenuGear2Offset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticMenuGear2Offset++;
                                                        MenuGear2Offset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMenuGear2Offset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticMenuGear2Offset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticMenuGear2Offset--;
                                                        MenuGear2Offset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMenuGear2Offset = false;
                                                        break;
                                                }
                                                break;
                                        }

                                        switch (SwitchOffsetSettingTitleOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticSettingTitleOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticSettingTitleOffset++;
                                                        SettingTitleOffset.Offset += 0.01;
                                                        break;

                                                    case false:
                                                        SwitchOffsetSettingTitleOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticSettingTitleOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticSettingTitleOffset--;
                                                        SettingTitleOffset.Offset -= 0.01;
                                                        break;

                                                    case false:
                                                        SwitchOffsetSettingTitleOffset = false;
                                                        break;
                                                }
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


                                        switch (SwitchOffsetSoundButtonOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticSoundButtonOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticSoundButtonOffset++;
                                                        SoundButtonOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetSoundButtonOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticSoundButtonOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticSoundButtonOffset--;
                                                        SoundButtonOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetSoundButtonOffset = false;
                                                        break;
                                                }
                                                break;
                                        }



                                        switch (SwitchOffsetMuteButtonOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticMuteButtonOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticMuteButtonOffset++;
                                                        MuteButtonOffset.Offset -= 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMuteButtonOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticMuteButtonOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticMuteButtonOffset--;
                                                        MuteButtonOffset.Offset += 0.025;
                                                        break;

                                                    case false:
                                                        SwitchOffsetMuteButtonOffset = false;
                                                        break;
                                                }
                                                break;
                                        }


                                        switch (SwitchInstructionManualTextBlockOffset)
                                        {
                                            case false:

                                                switch (GradientArithmeticInstructionManualTextBlockOffset <= 65)
                                                {
                                                    case true:
                                                        GradientArithmeticInstructionManualTextBlockOffset++;
                                                        InstructionManualTextBlockOffset.Offset -= 0.01;
                                                        break;

                                                    case false:
                                                        SwitchInstructionManualTextBlockOffset = true;
                                                        break;
                                                }
                                                break;

                                            case true:

                                                switch (GradientArithmeticInstructionManualTextBlockOffset > 0)
                                                {
                                                    case true:
                                                        GradientArithmeticInstructionManualTextBlockOffset--;
                                                        InstructionManualTextBlockOffset.Offset += 0.01;
                                                        break;

                                                    case false:
                                                        SwitchInstructionManualTextBlockOffset = false;
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

        private void SoundOn(object sender, RoutedEventArgs e)
        {
            switch (App.SettingsWindowOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {

                                case false:
                                    var settings = new Settings();
                                    System.Threading.Tasks.Task.Run(() => { settings.Set_Settings("Enabled"); });

                                    SoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                                    SoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                                    MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                                    SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");

                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void SoundOff(object sender, RoutedEventArgs e)
        {
            switch (App.SettingsWindowOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {

                                case false:
                                    var settings = new Settings();
                                    System.Threading.Tasks.Task.Run(() => { settings.Set_Settings("Disabled"); });

                                    SoundOffButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                                    SoundOnButton.Background = new SolidColorBrush(Colors.Transparent);

                                    SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                                    MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");

                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void OpenInstructionManual(object sender, RoutedEventArgs e)
        {
            switch (App.SettingsWindowOpen)
            {
                case true:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case false:

                            switch (Application.Current.MainWindow == null)
                            {

                                case false:

                                    switch (App.InstructionManualOpen)
                                    {
                                        case false:

                                            App.InstructionManualOpen = true;
                                            InstructionManual instructionManual = new InstructionManual();
                                            instructionManual.Show();
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }

        }

        ~SettingsWindow()
        {
            switch (AnimationAndFunctionalityTimerDisposed)
            {
                case false:

                    switch (AnimationAndFunctionalityTimer == null)
                    {

                        case false:

                            AnimationAndFunctionalityTimer.Dispose();
                            break;
                    }
                    break;
            }

            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(0, GCCollectionMode.Forced);
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(1, GCCollectionMode.Forced);
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced);
        }
    }
}

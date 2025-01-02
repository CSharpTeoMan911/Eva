using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Threading.Tasks;
using static Eva_5._0.SettingsWindow;
using System.Speech.Synthesis;

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
        private System.Timers.Timer ValuesChangedTimer;

        private static int current_model_index;

        private bool WindowIsClosing;

        private double Wheel1Angle;
        private double Wheel2Angle;


        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;
        

        private bool SwitchSecodaryOffsets;
        private double GradientArithmeticSecondaryOffsets;

        private bool TempChanged;
        private int Temp;

        private bool VoskSensitivityChanged;
        private float Sensitivity;

        private static SettingsWindow CurrentInstance;

        public delegate Task<bool> OpenSpeech();
        private OpenSpeech openSpeech;

        public SettingsWindow()
        {
            CurrentInstance = this;
            InitializeComponent();
        }

        public SettingsWindow(OpenSpeech openSpeech_)
        {
            openSpeech = openSpeech_;
            CurrentInstance = this;
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
            LoadCurrentModel();
            LoadTemperature();
            LoadSensitivity();

            AnimationAndFunctionalityTimer = new System.Timers.Timer();
            AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
            AnimationAndFunctionalityTimer.Interval = 10;
            AnimationAndFunctionalityTimer.Start();

            ValuesChangedTimer = new System.Timers.Timer();
            ValuesChangedTimer.Elapsed += ValuesChangedTimer_Elapsed; ;
            ValuesChangedTimer.Interval = 100;
            ValuesChangedTimer.Start();

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



            bool SythesisOnOrOff = await Settings.Get_Synthesis_Settings();


            switch (SythesisOnOrOff)
            {
                case true:
                    SynthesisSoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                    SynthesisSoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                    SynthesisMuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                    SynthesisSoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");


                    break;

                case false:
                    SynthesisSoundOffButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                    SynthesisSoundOnButton.Background = new SolidColorBrush(Colors.Transparent);

                    SynthesisSoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                    SynthesisMuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                    break;
            }


            string SpeechLanguage = await Settings.Get_Speech_Language_Settings();
            SpeechLanguageDisplay.Text = SpeechLanguage;
        }

        private void ValuesChangedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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

                            Application.Current.Dispatcher.Invoke(async () =>
                            {
                                switch (Application.Current.MainWindow == null)
                                {

                                    case true:
                                        try
                                        {
                                            AnimationAndFunctionalityTimer.Stop();
                                        }
                                        catch { }
                                        break;

                                    case false:

                                        if (App.Application_Error_Shutdown)
                                        {
                                            try
                                            {
                                                if (AnimationAndFunctionalityTimer != null)
                                                {
                                                    AnimationAndFunctionalityTimer.Stop();
                                                    this.Close();
                                                }
                                            }
                                            catch { }
                                        }



                                        if (TempChanged == true)
                                        {
                                            TempChanged = false;
                                            await Settings.Set_Current_Model_Temperature(Temp);
                                        }

                                        if(VoskSensitivityChanged == true)
                                        {
                                            VoskSensitivityChanged = false;
                                            await Settings.Set_Vosk_Sensitivity_Settings(Sensitivity);
                                        }
                                        break;

                                }
                            });
                            break;
                    }
                    break;
            }
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
                                        break;

                                    case false:

                                        if (App.Application_Error_Shutdown)
                                        {
                                            try
                                            {
                                                if (AnimationAndFunctionalityTimer != null)
                                                {
                                                    AnimationAndFunctionalityTimer.Stop();
                                                    this.Close();
                                                }
                                            }
                                            catch { }
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
                                                        SettingTitleOffset.Offset -= 0.01;
                                                        SynthesisSettingTitleOffset.Offset -= 0.01;
                                                        SoundButtonOffset.Offset -= 0.025;
                                                        MuteButtonOffset.Offset -= 0.025;
                                                        SetCommandsButtonOffset.Offset -= 0.025;
                                                        InstructionManualButtonOffset.Offset -= 0.025;
                                                        SetChatGptApiButtonOffset.Offset -= 0.025;
                                                        SynthesisSoundButtonOffset.Offset -= 0.025;
                                                        SynthesisMuteButtonOffset.Offset -= 0.025;
                                                        SetCommandsTitleOffset.Offset -= 0.01;
                                                        InstructionManualTextBlockOffset.Offset -= 0.01;
                                                        ChatGPTApiKeyOffset.Offset -= 0.01;
                                                        GPTModelOffset.Offset -= 0.01;
                                                        PreviousModelButtonOffset.Offset -= 0.025;
                                                        NextModelButtonOffset.Offset -= 0.025;
                                                        ModelTempLabelOffset.Offset -= 0.01;
                                                        SensitivityTitleOffset.Offset -= 0.01;
                                                        ModelTempOffset.Offset -= 0.01;
                                                        SensitivityOffset.Offset -= 0.01;
                                                        SpeechLanguagelOffset.Offset -= 0.01;
                                                        PreviousSpeechLanguageButtonOffset.Offset -= 0.025;
                                                        NextSpeechLanguageButtonOffset.Offset -= 0.025;
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
                                                        SettingTitleOffset.Offset += 0.01;
                                                        SynthesisSettingTitleOffset.Offset += 0.01;
                                                        SoundButtonOffset.Offset += 0.025;
                                                        MuteButtonOffset.Offset += 0.025;
                                                        SetCommandsButtonOffset.Offset += 0.025;
                                                        InstructionManualButtonOffset.Offset += 0.025;
                                                        SynthesisSoundButtonOffset.Offset += 0.025;
                                                        SetChatGptApiButtonOffset.Offset += 0.025;
                                                        SynthesisMuteButtonOffset.Offset += 0.025;
                                                        SetCommandsTitleOffset.Offset += 0.01;
                                                        InstructionManualTextBlockOffset.Offset += 0.01;
                                                        ChatGPTApiKeyOffset.Offset += 0.01;
                                                        GPTModelOffset.Offset += 0.01;
                                                        PreviousModelButtonOffset.Offset += 0.025;
                                                        NextModelButtonOffset.Offset += 0.025;
                                                        ModelTempLabelOffset.Offset += 0.01;
                                                        SensitivityTitleOffset.Offset += 0.01;
                                                        ModelTempOffset.Offset += 0.01;
                                                        SensitivityOffset.Offset += 0.01;
                                                        SpeechLanguagelOffset.Offset += 0.01;
                                                        PreviousSpeechLanguageButtonOffset.Offset += 0.025;
                                                        NextSpeechLanguageButtonOffset.Offset += 0.025;
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

        private void SensitivityChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float value = (float)(e.NewValue * 10);
            SensitivityeDisplay.Text = value + "%";

            Sensitivity = (float)e.NewValue;
            VoskSensitivityChanged = true;
        }

        private async void SynthesisSoundOn(object sender, RoutedEventArgs e)
        {
            if (App.SettingsWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {
                        await Settings.Set_Synthesis_Settings(true);

                        SynthesisSoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                        SynthesisSoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                        SynthesisMuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                        SynthesisSoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                    }

                }

            }
        }

        private async void SynthesisSoundOff(object sender, RoutedEventArgs e)
        {
            if (App.SettingsWindowOpen == true)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    if (Application.Current.MainWindow != null)
                    {

                        await Settings.Set_Synthesis_Settings(false);

                        SynthesisSoundOffButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                        SynthesisSoundOnButton.Background = new SolidColorBrush(Colors.Transparent);

                        SynthesisSoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                        SynthesisMuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");

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

        private async void PreviousModel(object sender, RoutedEventArgs e)
        {
            if (ChatGPT_API.gpt_models.Count == 0)
                await ChatGPT_API.Get_Available_Gpt_Models();

            if (ChatGPT_API.gpt_models.Count > 0)
            {
                if (current_model_index > 0)
                {
                    current_model_index--;
                    string current_gpt_model = ChatGPT_API.gpt_models.ElementAt(current_model_index);
                    GptModelDisplay.Text = current_gpt_model;
                    await Settings.Set_Current_Chat_GPT__Model(current_gpt_model);
                }
            }
        }

        private async void LoadCurrentModel()
        {
            await Application.Current.Dispatcher.InvokeAsync(async() =>
            {
                if (ChatGPT_API.gpt_models.Count == 0)
                    await ChatGPT_API.Get_Available_Gpt_Models();

                if (ChatGPT_API.gpt_models.Count > 0)
                {
                    string current_model = await Settings.Get_Current_Chat_GPT__Model();
                    int model_index = ChatGPT_API.gpt_models.IndexOf(current_model);

                    if (model_index != -1)
                    {
                        current_model_index = model_index;
                        GptModelDisplay.Text = ChatGPT_API.gpt_models.ElementAt(current_model_index);
                    }
                    else
                    {
                        GptModelDisplay.Text = ChatGPT_API.gpt_models.ElementAt(0);
                        await Settings.Set_Current_Chat_GPT__Model(ChatGPT_API.gpt_models.First());
                    }
                }
            });
        }

        public static void ReloadCurrentModel()
        {
            CurrentInstance.LoadCurrentModel();
        }

        private async void NextModel(object sender, RoutedEventArgs e)
        {
            if(ChatGPT_API.gpt_models.Count == 0)
                await ChatGPT_API.Get_Available_Gpt_Models();

            if (ChatGPT_API.gpt_models.Count > 0)
            {
                if (current_model_index < ChatGPT_API.gpt_models.Count - 1)
                {
                    current_model_index++;
                    string current_gpt_model = ChatGPT_API.gpt_models.ElementAt(current_model_index);
                    GptModelDisplay.Text = current_gpt_model;
                    await Settings.Set_Current_Chat_GPT__Model(current_gpt_model);
                }
            }
        }

        private void TemperatureChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = (int)(e.NewValue * 10);
            TemperatureDisplay.Text = value + "%";

            Temp = (int)e.NewValue;
            TempChanged = true;
        }

        private async void LoadTemperature()
        {
            int temp = await Settings.Get_Current_Model_Temperature();
            TemperatureSelector.Value = temp;
            TemperatureDisplay.Text = (temp * 10) + "%";
        }

        private async void LoadSensitivity()
        {
            float sensitivity = await Settings.Get_Vosk_Sensitivity_Settings();
            SensitivitySelector.Value = sensitivity;
            SensitivityeDisplay.Text = (sensitivity * 10) + "%";
        }

        private void SetCommands(object sender, RoutedEventArgs e)
        {
            Commands_Main_Window commands = new Commands_Main_Window(openSpeech);
            commands.ShowDialog();
        }

        private async void PreviousLanguage(object sender, RoutedEventArgs e)
        {
            if (SpeechLanguageDisplay.Text == "en-US")
            {
                SpeechLanguageDisplay.Text = "en-GB";
                await Settings.Set_Speech_Language_Settings(Settings.SpeechLanguage.en_GB);
            }
        }

        private async void NextLanguage(object sender, RoutedEventArgs e)
        {
            if (SpeechLanguageDisplay.Text == "en-GB")
            {
                SpeechLanguageDisplay.Text = "en-US";
                await Settings.Set_Speech_Language_Settings(Settings.SpeechLanguage.en_US);
            }
        }

        ~SettingsWindow()
        {
            AnimationAndFunctionalityTimer?.Dispose();
            CurrentInstance = null;
            System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced);
        }
    }
}

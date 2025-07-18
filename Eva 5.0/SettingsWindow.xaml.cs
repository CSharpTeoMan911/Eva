﻿using System;
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

        public delegate void OpenSpeech();
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

        private void SettingsWindowLoaded(object sender, RoutedEventArgs e)
        {
            Task.Run(async() =>
            {
                try
                {
                    LoadCurrentModel();
                    LoadTemperature();
                    LoadSensitivity();

                    AnimationAndFunctionalityTimer = new System.Timers.Timer();
                    AnimationAndFunctionalityTimer.Elapsed += AnimationAndFunctionalityTimer_Elapsed;
                    AnimationAndFunctionalityTimer.Interval = 10;
                    AnimationAndFunctionalityTimer.Start();

                    ValuesChangedTimer = new System.Timers.Timer();
                    ValuesChangedTimer.Elapsed += ValuesChangedTimer_Elapsed;
                    ValuesChangedTimer.Interval = 100;
                    ValuesChangedTimer.Start();


                    bool SoundOrOff = await Settings.Get_Sound_Settings();
                    bool SythesisOnOrOff = await Settings.Get_Synthesis_Settings();
                    string SpeechLanguage = await Settings.Get_Speech_Language_Settings();
                    int Timeout = await Settings.Get_Speech_Timeout_Settings();

                    A_p_l____And____P_r_o_c.SpeechRecognitionOperation operation = await Settings.Get_Speech_Operation_Settings();

                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        if (SoundOrOff == true)
                        {
                            SoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                            SoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                            MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                            SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                        }
                        else
                        {
                            SoundOffButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                            SoundOnButton.Background = new SolidColorBrush(Colors.Transparent);

                            SoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                            MuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                        }

                        if (SythesisOnOrOff == true)
                        {
                            SynthesisSoundOnButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                            SynthesisSoundOffButton.Background = new SolidColorBrush(Colors.Transparent);

                            SynthesisMuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                            SynthesisSoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                        }
                        else
                        {
                            SynthesisSoundOffButton.Background = (Brush)new BrushConverter().ConvertFromString("#FF081725");
                            SynthesisSoundOnButton.Background = new SolidColorBrush(Colors.Transparent);

                            SynthesisSoundButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF1B70C3");
                            SynthesisMuteButtonOffset.Color = (Color)ColorConverter.ConvertFromString("#FF7BBFD8");
                        }

                        switch (operation)
                        {
                            case A_p_l____And____P_r_o_c.SpeechRecognitionOperation.FormFilling:
                                SpeechOperationDisplay.Text = "Form filling";
                                break;
                            case A_p_l____And____P_r_o_c.SpeechRecognitionOperation.Dictation:
                                SpeechOperationDisplay.Text = "Dictation";
                                break;
                            case A_p_l____And____P_r_o_c.SpeechRecognitionOperation.WebSearch:
                                SpeechOperationDisplay.Text = "Web search";
                                break;
                        }

                        SpeechLanguageDisplay.Text = SpeechLanguage;
                        SpeechOperationTimeout.Text = Timeout.ToString();
                    });
                }
                catch { }
            });
        }

        private void ValuesChangedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (WindowIsClosing == true)
                {
                    AnimationAndFunctionalityTimer?.Stop();
                }
                else
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == true)
                    {
                        AnimationAndFunctionalityTimer?.Stop();
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (Application.Current.MainWindow == null)
                            {
                                AnimationAndFunctionalityTimer?.Stop();
                            }
                            else
                            {
                                if (App.Application_Error_Shutdown)
                                {
                                    if (AnimationAndFunctionalityTimer != null)
                                    {
                                        AnimationAndFunctionalityTimer.Stop();
                                        this.Close();
                                    }
                                }

                                if (TempChanged == true)
                                {
                                    TempChanged = false;
                                    await Settings.Set_Current_Model_Temperature(Temp);
                                }

                                if (VoskSensitivityChanged == true)
                                {
                                    VoskSensitivityChanged = false;
                                    await Settings.Set_Vosk_Sensitivity_Settings(Sensitivity);
                                }
                            }
                        });
                    }
                }
            }
            catch { }
        }

        private void AnimationAndFunctionalityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (WindowIsClosing == true)
                {
                    AnimationAndFunctionalityTimer?.Stop();
                }
                else
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == true)
                    {
                        AnimationAndFunctionalityTimer?.Stop();
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (Application.Current.MainWindow == null)
                            {
                                AnimationAndFunctionalityTimer?.Stop();
                            }
                            else
                            {
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

                                if (Wheel1Angle <= 360)
                                {
                                    Wheel1Angle++;
                                }
                                else
                                {
                                    Wheel1Angle = 0;
                                }

                                if (Wheel2Angle <= 360)
                                {
                                    Wheel2Angle++;
                                }
                                else
                                {
                                    Wheel2Angle = 0;
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


                                if (SwitchWindowOffset == false)
                                {
                                    if (GradientArithmeticWindowOffset <= 300)
                                    {
                                        GradientArithmeticWindowOffset++;
                                        WindowOffset.Offset -= 0.0025;
                                    }
                                    else
                                    {
                                        SwitchWindowOffset = true;
                                    }
                                }
                                else
                                {
                                    if (GradientArithmeticWindowOffset > 0)
                                    {
                                        GradientArithmeticWindowOffset--;
                                        WindowOffset.Offset += 0.0025;
                                    }
                                    else
                                    {
                                        SwitchWindowOffset = false;
                                    }
                                }

                                if (SwitchSecodaryOffsets == false)
                                {
                                    if (GradientArithmeticSecondaryOffsets <= 65)
                                    {
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
                                        SpeechOperationOffset.Offset -= 0.01;
                                        PreviousSpeechOperationButtonOffset.Offset -= 0.025;
                                        CurrentSpeechOperationOffset.Offset -= 0.01;
                                        NextSpeechOperationButtonOffset.Offset -= 0.025;
                                        SpeechTimeoutOffset.Offset -= 0.01;
                                        CurrentSpeechTimeoutOffset.Offset -= 0.01;
                                        NextSpeechTimeoutButtonOffset.Offset -= 0.025;
                                        PreviousSpeechTimeoutButtonOffset.Offset -= 0.025;
                                    }
                                    else
                                    {
                                        SwitchSecodaryOffsets = true;
                                    }
                                }
                                else
                                {
                                    if (GradientArithmeticSecondaryOffsets > 0)
                                    {
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
                                        SpeechOperationOffset.Offset += 0.01;
                                        PreviousSpeechOperationButtonOffset.Offset += 0.025;
                                        CurrentSpeechOperationOffset.Offset += 0.01;
                                        NextSpeechOperationButtonOffset.Offset += 0.025;
                                        SpeechTimeoutOffset.Offset += 0.01;
                                        CurrentSpeechTimeoutOffset.Offset += 0.01;
                                        NextSpeechTimeoutButtonOffset.Offset += 0.025;
                                        PreviousSpeechTimeoutButtonOffset.Offset += 0.025;
                                    }
                                    else
                                    {
                                        SwitchSecodaryOffsets = false;
                                    }
                                }
                            }
                        });
                    }
                }
            }
            catch { }
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
            if (ChatGPT_API.gpt_models.Count == 0)
                await ChatGPT_API.Get_Available_Gpt_Models();

            if (ChatGPT_API.gpt_models.Count > 0)
            {
                string current_model = await Settings.Get_Current_Chat_GPT__Model();
                int model_index = ChatGPT_API.gpt_models.IndexOf(current_model);

                if (model_index != -1)
                {
                    current_model_index = model_index;
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        GptModelDisplay.Text = ChatGPT_API.gpt_models.ElementAt(current_model_index);
                    });
                }
                else
                {
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        GptModelDisplay.Text = ChatGPT_API.gpt_models.ElementAt(0);
                    });
                    await Settings.Set_Current_Chat_GPT__Model(ChatGPT_API.gpt_models.First());
                }
            }
        }

        public static void ReloadCurrentModel()
        {
            Application.Current?.Dispatcher?.InvokeAsync(() => {
                CurrentInstance.LoadCurrentModel();
            });
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
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                TemperatureSelector.Value = temp;
                TemperatureDisplay.Text = (temp * 10) + "%";
            });
        }

        private async void LoadSensitivity()
        {
            float sensitivity = await Settings.Get_Vosk_Sensitivity_Settings();
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                SensitivitySelector.Value = sensitivity;
                SensitivityeDisplay.Text = (sensitivity * 10) + "%";
            });
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

        private async void NextOperation(object sender, RoutedEventArgs e)
        {
            A_p_l____And____P_r_o_c.SpeechRecognitionOperation operation = A_p_l____And____P_r_o_c.SpeechRecognitionOperation.FormFilling;

            if (SpeechOperationDisplay.Text == "Form filling")
            {
                SpeechOperationDisplay.Text = "Web search";
                operation = A_p_l____And____P_r_o_c.SpeechRecognitionOperation.WebSearch;
            }
            else if (SpeechOperationDisplay.Text == "Web search")
            {
                SpeechOperationDisplay.Text = "Dictation";
                operation = A_p_l____And____P_r_o_c.SpeechRecognitionOperation.Dictation;
            }

            await Settings.Set_Speech_Operation_Settings(operation);
        }

        private async void PreviousOperation(object sender, RoutedEventArgs e)
        {
            A_p_l____And____P_r_o_c.SpeechRecognitionOperation operation = A_p_l____And____P_r_o_c.SpeechRecognitionOperation.FormFilling;

            if (SpeechOperationDisplay.Text == "Dictation")
            {
                SpeechOperationDisplay.Text = "Web search";
                operation = A_p_l____And____P_r_o_c.SpeechRecognitionOperation.WebSearch;
            }
            else if (SpeechOperationDisplay.Text == "Web search")
            {
                SpeechOperationDisplay.Text = "Form filling";
                operation = A_p_l____And____P_r_o_c.SpeechRecognitionOperation.FormFilling;
            }

            await Settings.Set_Speech_Operation_Settings(operation);
        }

        private async void PreviousTimeout(object sender, RoutedEventArgs e)
        {
            int Timeout = Convert.ToInt32(SpeechOperationTimeout.Text);
            if (Timeout > 1)
            {
                Timeout--;
                SpeechOperationTimeout.Text = Timeout.ToString();
                await Settings.Set_Speech_Timeout_Settings(Timeout);
            }
        }

        private async void NextTimeout(object sender, RoutedEventArgs e)
        {
            int Timeout = Convert.ToInt32(SpeechOperationTimeout.Text);
            if (Timeout < 7)
            {
                Timeout++;
                SpeechOperationTimeout.Text = Timeout.ToString();
                await Settings.Set_Speech_Timeout_Settings(Timeout);
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

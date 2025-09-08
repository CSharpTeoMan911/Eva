using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Eva_5._0
{
    /// <summary>
    /// Interaction logic for Timer_Window.xaml
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



    public partial class Timer_Window : Window
    {
        private System.Timers.Timer Animation_And_Functionality_Timer;

        private static StringBuilder total_time = new StringBuilder();

        private static StringBuilder hours_interval = new StringBuilder();

        private static StringBuilder minutes_interval = new StringBuilder();

        private static StringBuilder seconds_interval = new StringBuilder();



        private bool WindowClosing;


        public static bool Ring_Timer;
        private bool Alarm_Started;


        private double Initial_Timer_Cancel_Button_Width;


        private short AnimationAngleCounter;
        private bool DirectionPointer;


        private bool SwitchWindowOffset;
        private double GradientArithmeticWindowOffset;




        public Timer_Window()
        {
            InitializeComponent();
        }

        private void Move_The_Window(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Application.Current != null)
                {
                    if (Application.Current.Dispatcher != null)
                    {
                        if (!Application.Current.Dispatcher.HasShutdownStarted)
                        {
                            this?.DragMove();
                        }
                    }
                }
            }
            catch { }
        }

        private void Close_The_Window(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            if (Ring_Timer == true)
                            {
                                Ring_Timer = false;

                                if (Alarm_Started == true)
                                {
                                    A_p_l____And____P_r_o_c.sound_player.Stop_Alarm();
                                }
                            }
                            this.Close();
                        }
                    });

                }

            }
        }

        private void Timer_Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initial_Timer_Cancel_Button_Width = Cancel_The_Timer_Button.ActualWidth;

            Animation_And_Functionality_Timer = new System.Timers.Timer();
            Animation_And_Functionality_Timer.Interval = 10;
            Animation_And_Functionality_Timer.Elapsed += Animation_And_Functionality_Timer_Elapsed;
            Animation_And_Functionality_Timer.Start();

            this.Topmost = true;
        }

        private void Animation_And_Functionality_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (WindowClosing == true)
                {
                    Animation_And_Functionality_Timer.Stop();
                }
                else
                {
                    if (Application.Current.Dispatcher.HasShutdownStarted == true)
                    {
                        Animation_And_Functionality_Timer.Stop();
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(async () =>
                        {

                            if (Application.Current.MainWindow == null)
                            {
                                Animation_And_Functionality_Timer.Stop();
                            }
                            else
                            {

                                if (App.Application_Error_Shutdown)
                                {
                                    if (Animation_And_Functionality_Timer != null)
                                    {
                                        Animation_And_Functionality_Timer.Stop();
                                        this.Close();
                                    }
                                }


                                if (SwitchWindowOffset == true)
                                {
                                    if (GradientArithmeticWindowOffset > 0)
                                    {
                                        GradientArithmeticWindowOffset--;
                                        Close_The_Window_Button_Foreground_Offset.Offset += 0.02;
                                        Timer_Clock_Textblock_Foreground_Offset.Offset += 0.02;
                                        Timer_Time_Interval_Foreground_Offset.Offset += 0.02;
                                        Cancel_The_Timer_Button_Foreground_Offset.Offset += 0.02;
                                        Cancel_The_Timer_Button_Background_Offset.Offset += 0.02;
                                        Window_Gradient_Offset.Offset += 0.02;
                                    }
                                    else
                                    {
                                        SwitchWindowOffset = false;
                                    }
                                }
                                else
                                {
                                    if (GradientArithmeticWindowOffset < 45)
                                    {
                                        GradientArithmeticWindowOffset++;
                                        Close_The_Window_Button_Foreground_Offset.Offset -= 0.02;
                                        Timer_Clock_Textblock_Foreground_Offset.Offset -= 0.02;
                                        Timer_Time_Interval_Foreground_Offset.Offset -= 0.02;
                                        Cancel_The_Timer_Button_Foreground_Offset.Offset -= 0.02;
                                        Cancel_The_Timer_Button_Background_Offset.Offset -= 0.02;
                                        Window_Gradient_Offset.Offset -= 0.02;
                                    }
                                    else
                                    {
                                        SwitchWindowOffset = true;
                                    }
                                }


                                if (Timer_Interval.IsTimer() == true)
                                {

                                    Tuple<int, int, int> received_timer_interval = Timer_Interval.Get_Time_Interval();

                                    total_time.Clear();

                                    hours_interval.Clear();

                                    minutes_interval.Clear();

                                    seconds_interval.Clear();




                                    if (received_timer_interval.Item1 < 10)
                                    {
                                        hours_interval.Append("0");
                                        hours_interval.Append(received_timer_interval.Item1.ToString());
                                    }
                                    else
                                    {
                                        hours_interval.Append(received_timer_interval.Item1.ToString());
                                    }


                                    if (received_timer_interval.Item2 < 10)
                                    {
                                        minutes_interval.Append("0");
                                        minutes_interval.Append(received_timer_interval.Item2.ToString());
                                    }
                                    else
                                    {
                                        minutes_interval.Append(received_timer_interval.Item2.ToString());
                                    }


                                    if (received_timer_interval.Item3 < 10)
                                    {
                                        seconds_interval.Append("0");
                                        seconds_interval.Append(received_timer_interval.Item3.ToString());
                                    }
                                    else
                                    {
                                        seconds_interval.Append(received_timer_interval.Item3.ToString());
                                    }


                                    Thickness margin = new Thickness();

                                    margin.Top = 0;

                                    margin.Bottom = 0;

                                    margin.Left = 20;

                                    margin.Right = 0;




                                    Cancel_The_Timer_Button.Margin = margin;

                                    Cancel_The_Timer_Button.Width = Initial_Timer_Cancel_Button_Width;



                                    Cancel_The_Timer_Button.Visibility = Visibility.Visible;

                                    total_time.Append(hours_interval.ToString());
                                    total_time.Append(" : ");
                                    total_time.Append(minutes_interval.ToString());
                                    total_time.Append(" : ");
                                    total_time.Append(seconds_interval.ToString());

                                    Timer_Time_Interval.Text = total_time.ToString();




                                    total_time.Clear();

                                    hours_interval.Clear();

                                    minutes_interval.Clear();

                                    seconds_interval.Clear();

                                }
                                else
                                {
                                    Timer_Time_Interval.Text = "-- : -- : --";

                                    Thickness margin = new Thickness();

                                    margin.Top = 0;

                                    margin.Bottom = 0;

                                    margin.Left = 0;

                                    margin.Right = 0;




                                    Cancel_The_Timer_Button.Margin = margin;

                                    Cancel_The_Timer_Button.Width = 0;



                                    Cancel_The_Timer_Button.Visibility = Visibility.Hidden;
                                }


                                if (Ring_Timer == true)
                                {
                                    Cancel_The_Timer_Button.Visibility = Visibility.Visible;

                                    Cancel_The_Timer_Button.Width = Initial_Timer_Cancel_Button_Width;


                                    Timer_Time_Interval.Width = 0;

                                    Timer_Time_Interval.Visibility = Visibility.Hidden;

                                    if (DirectionPointer == false)
                                    {
                                        AnimationAngleCounter++;
                                        AnimationAngleCounter++;
                                        AnimationAngleCounter++;
                                        AnimationAngleCounter++;
                                        AnimationAngleCounter++;
                                    }
                                    else
                                    {
                                        AnimationAngleCounter--;
                                        AnimationAngleCounter--;
                                        AnimationAngleCounter--;
                                        AnimationAngleCounter--;
                                        AnimationAngleCounter--;
                                    }

                                    if (AnimationAngleCounter == 30)
                                    {
                                        DirectionPointer = true;
                                    }
                                    else if (AnimationAngleCounter == -30)
                                    {
                                        DirectionPointer = false;
                                    }

                                    RotateTransform rotate = new RotateTransform();

                                    rotate.Angle = AnimationAngleCounter;
                                    Timer_Clock_Textblock.RenderTransform = rotate;



                                    if (Alarm_Started == false)
                                    {
                                        await A_p_l____And____P_r_o_c.sound_player.Play_Sound(Properties.Sound_Player.Sounds.Alarm_Sound_Effect);
                                        Alarm_Started = true;
                                    }
                                }
                            }
                        });
                    }
                }
            }
            catch { }
        }


        private void Cancel_The_Timer(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {

                            if (Ring_Timer == true)
                            {
                                Ring_Timer = false;

                                if (Alarm_Started == true)
                                {
                                    A_p_l____And____P_r_o_c.sound_player.Stop_Alarm();
                                }

                                this.Close();
                            }
                            else
                            {
                                Timer_Interval.Cancel_Time_Interval();
                            }
                        }
                    });

                }

            }
        }


        private void Timer_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.TimerWindowOpen = false;
            WindowClosing = true;


            if (Alarm_Started == true)
            {
                A_p_l____And____P_r_o_c.sound_player.Stop_Alarm();
            }
            Animation_And_Functionality_Timer?.Dispose();
        }
    }
}

using System;
using System.Text;
using System.Threading.Tasks;
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

        private static System.Media.SoundPlayer Alarm_Sound_Effect = new System.Media.SoundPlayer("AlarmSound.wav");


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




        private bool Switch_Close_The_Window_Button_Foreground_Offset;

        private int Gradient_Arithmetic_Close_The_Window_Button_Foreground_Offset;




        private bool Switch_Timer_Clock_Textblock_Foreground_Offset;

        private int Gradient_Arithmetic_Timer_Clock_Textblock_Foreground_Offset;



        private bool Switch_Timer_Time_Interval_Foreground_Offset;

        private int Gradient_Arithmetic_Timer_Time_Interval_Foreground_Offset;



        private bool Switch_Cancel_The_Timer_Button_Foreground_Offset;

        private int Gradient_Arithmetic_Cancel_The_Timer_Button_Foreground_Offset;



        private bool Switch_Cancel_The_Timer_Button_Background_Offset;

        private int Gradient_Arithmetic_Cancel_The_Timer_Button_Background_Offset;



        private bool Switch_Window_Gradient_Offset;

        private int Gradient_Arithmetic_Window_Gradient_Offset;




        public Timer_Window()
        {
            InitializeComponent();
        }

        private void Move_The_Window(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current != null)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {

                            this.DragMove();

                        }
                    });

                }

            }
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
            switch (WindowClosing)
            {
                case true:
                    try
                    {
                        Animation_And_Functionality_Timer.Stop();
                    }
                    catch { }
                    break;

                case false:

                    switch (Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        case true:
                            try
                            {
                                Animation_And_Functionality_Timer.Stop();
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
                                            Animation_And_Functionality_Timer.Stop();
                                        }
                                        catch { }
                                        break;



                                    case false:




                                        switch (Switch_Close_The_Window_Button_Foreground_Offset)
                                        {
                                            case true:

                                                switch (Gradient_Arithmetic_Close_The_Window_Button_Foreground_Offset > 0)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Close_The_Window_Button_Foreground_Offset--;
                                                        Close_The_Window_Button_Foreground_Offset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Close_The_Window_Button_Foreground_Offset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (Gradient_Arithmetic_Close_The_Window_Button_Foreground_Offset < 45)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Close_The_Window_Button_Foreground_Offset++;
                                                        Close_The_Window_Button_Foreground_Offset.Offset -= 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Close_The_Window_Button_Foreground_Offset = true;
                                                        break;
                                                }

                                                break;
                                        }




                                        switch (Switch_Timer_Clock_Textblock_Foreground_Offset)
                                        {
                                            case true:

                                                switch (Gradient_Arithmetic_Timer_Clock_Textblock_Foreground_Offset > 0)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Timer_Clock_Textblock_Foreground_Offset--;
                                                        Timer_Clock_Textblock_Foreground_Offset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Timer_Clock_Textblock_Foreground_Offset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (Gradient_Arithmetic_Timer_Clock_Textblock_Foreground_Offset < 45)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Timer_Clock_Textblock_Foreground_Offset++;
                                                        Timer_Clock_Textblock_Foreground_Offset.Offset -= 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Timer_Clock_Textblock_Foreground_Offset = true;
                                                        break;
                                                }

                                                break;
                                        }




                                        switch (Switch_Timer_Time_Interval_Foreground_Offset)
                                        {
                                            case true:

                                                switch (Gradient_Arithmetic_Timer_Time_Interval_Foreground_Offset > 0)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Timer_Time_Interval_Foreground_Offset--;
                                                        Timer_Time_Interval_Foreground_Offset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Timer_Time_Interval_Foreground_Offset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (Gradient_Arithmetic_Timer_Time_Interval_Foreground_Offset < 45)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Timer_Time_Interval_Foreground_Offset++;
                                                        Timer_Time_Interval_Foreground_Offset.Offset -= 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Timer_Time_Interval_Foreground_Offset = true;
                                                        break;
                                                }

                                                break;
                                        }



                                        switch (Switch_Cancel_The_Timer_Button_Foreground_Offset)
                                        {
                                            case true:

                                                switch (Gradient_Arithmetic_Cancel_The_Timer_Button_Foreground_Offset > 0)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Cancel_The_Timer_Button_Foreground_Offset--;
                                                        Cancel_The_Timer_Button_Foreground_Offset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Cancel_The_Timer_Button_Foreground_Offset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (Gradient_Arithmetic_Cancel_The_Timer_Button_Foreground_Offset < 45)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Cancel_The_Timer_Button_Foreground_Offset++;
                                                        Cancel_The_Timer_Button_Foreground_Offset.Offset -= 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Cancel_The_Timer_Button_Foreground_Offset = true;
                                                        break;
                                                }

                                                break;
                                        }



                                        switch (Switch_Cancel_The_Timer_Button_Background_Offset)
                                        {
                                            case true:

                                                switch (Gradient_Arithmetic_Cancel_The_Timer_Button_Background_Offset > 0)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Cancel_The_Timer_Button_Background_Offset--;
                                                        Cancel_The_Timer_Button_Background_Offset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Cancel_The_Timer_Button_Background_Offset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (Gradient_Arithmetic_Cancel_The_Timer_Button_Background_Offset < 45)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Cancel_The_Timer_Button_Background_Offset++;
                                                        Cancel_The_Timer_Button_Background_Offset.Offset -= 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Cancel_The_Timer_Button_Background_Offset = true;
                                                        break;
                                                }

                                                break;
                                        }



                                        switch (Switch_Window_Gradient_Offset)
                                        {
                                            case true:

                                                switch (Gradient_Arithmetic_Window_Gradient_Offset > 0)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Window_Gradient_Offset--;
                                                        Window_Gradient_Offset.Offset += 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Window_Gradient_Offset = false;
                                                        break;
                                                }

                                                break;

                                            case false:

                                                switch (Gradient_Arithmetic_Window_Gradient_Offset < 45)
                                                {
                                                    case true:
                                                        Gradient_Arithmetic_Window_Gradient_Offset++;
                                                        Window_Gradient_Offset.Offset -= 0.02;
                                                        break;

                                                    case false:
                                                        Switch_Window_Gradient_Offset = true;
                                                        break;
                                                }

                                                break;
                                        }







                                        if (Timer_Interval._isTimer == true)
                                        {

                                            Tuple<int, int, int> received_timer_interval = await Timer_Interval.Get_Time_Interval();

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

                                            switch (DirectionPointer)
                                            {
                                                case false:
                                                    AnimationAngleCounter++;
                                                    AnimationAngleCounter++;
                                                    AnimationAngleCounter++;
                                                    AnimationAngleCounter++;
                                                    AnimationAngleCounter++;
                                                    break;

                                                case true:
                                                    AnimationAngleCounter--;
                                                    AnimationAngleCounter--;
                                                    AnimationAngleCounter--;
                                                    AnimationAngleCounter--;
                                                    AnimationAngleCounter--;
                                                    break;
                                            }

                                            switch (AnimationAngleCounter)
                                            {
                                                case 30:
                                                    DirectionPointer = true;
                                                    break;

                                                case -30:
                                                    DirectionPointer = false;
                                                    break;
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
                                      


                                        break;
                                }
                            });
                            break;

                    }
                    break;
            }
        }


        private void Cancel_The_Timer(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null)
            {

                if (Application.Current.Dispatcher.HasShutdownStarted == false)
                {

                    Application.Current.Dispatcher.Invoke(async () =>
                    {
                        if (Application.Current.MainWindow != null)
                        {

                            switch (Ring_Timer)
                            {
                                case true:

                                    Ring_Timer = false;

                                    if (Alarm_Started == true)
                                    {
                                        await A_p_l____And____P_r_o_c.sound_player.Stop_Alarm();
                                    }

                                    this.Close();

                                    break;


                                case false:

                                    await Timer_Interval.Cancel_Time_Interval();
                                    break;
                            }


                        }
                    });

                }

            }
        }


        private async void Timer_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.TimerWindowOpen = false;
            WindowClosing = true;


            if (Alarm_Started == true)
            {
                await A_p_l____And____P_r_o_c.sound_player.Stop_Alarm();
            }
        }

        ~Timer_Window()
        {
            if(Animation_And_Functionality_Timer != null)
            {
                try
                {
                    Animation_And_Functionality_Timer.Dispose();
                }
                catch { }
            }
        }

    }
}

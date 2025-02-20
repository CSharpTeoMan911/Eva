using System;
using System.Threading.Tasks;

namespace Eva_5._0
{


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


    internal class Timer_Interval
    {

        private static bool _isTimer = false;

        private static DateTime hh_mm_ss_mls___Event;



        public static bool Calculate_Time_Interval_Left()
        {
            try
            {
                TimeSpan calculated_remaining_time = hh_mm_ss_mls___Event - DateTime.UtcNow;
                if ((int)calculated_remaining_time.TotalMilliseconds <= 0)
                {
                    _isTimer = false;
                    return true;
                }
            }
            catch { }

            return false;
        }





        public static void Set_Time_Interval<Hours, Minutes, Seconds>(Hours hours, Minutes minutes, Seconds seconds)
        {
            try
            {
                int Hours_Parameter = Convert.ToInt32(hours);

                int Minutes_Parameter = Convert.ToInt32(minutes);

                int Seconds_Parameter = Convert.ToInt32(seconds);



                int total_interval_seconds_pool = 0;


                if (Hours_Parameter != 0)
                {
                    total_interval_seconds_pool += Hours_Parameter * 3600;
                }

                if (Minutes_Parameter != 0)
                {
                    total_interval_seconds_pool += Minutes_Parameter * 60;
                }

                if (Seconds_Parameter != 0)
                {
                    total_interval_seconds_pool += Seconds_Parameter;
                }

                if (total_interval_seconds_pool != 0)
                {
                    int total_milliseconds_interval = total_interval_seconds_pool * 1000;
                    hh_mm_ss_mls___Event = DateTime.UtcNow.AddMilliseconds(total_milliseconds_interval);
                    _isTimer = true;
                }
            }
            catch { }
        }

        public static Tuple<int, int, int> Get_Time_Interval()
        {
            TimeSpan calculated_remaining_time = hh_mm_ss_mls___Event - DateTime.UtcNow;
            return new Tuple<int, int, int>((int)calculated_remaining_time.Hours, (int)calculated_remaining_time.Minutes, (int)calculated_remaining_time.Seconds);
        }

        public static bool IsTimer() => _isTimer;

        public static void Cancel_Time_Interval() => _isTimer = false;
    }
}

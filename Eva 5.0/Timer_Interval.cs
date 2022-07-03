using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_5._0
{
    internal class Timer_Interval
    {

        public static bool _isTimer = false;

        public static DateTime hh_mm_ss_mls___Event;



        public static Task<bool> Calculate_Time_Interval_Left()
        {

            try
            {
                TimeSpan calculated_remaining_time = hh_mm_ss_mls___Event - DateTime.Now;


                if ((int)calculated_remaining_time.TotalMilliseconds <= 0)
                {
                    System.Diagnostics.Debug.WriteLine(" [ Time Elapsed ] ");

                    _isTimer = false;

                    return Task.FromResult(true);
                }

            }
            catch { }



            return Task.FromResult(false);
        }





        public static Task<bool> Set_Time_Interval<Hours, Minutes, Seconds>(Hours hours, Minutes minutes, Seconds seconds)
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

                    hh_mm_ss_mls___Event = DateTime.Now.AddMilliseconds(total_milliseconds_interval);

                    _isTimer = true;
                }


            }
            catch
            {
                
            }

            return Task.FromResult(true);
        }




        public static Task<Tuple<int, int, int>> Get_Time_Interval()
        {
            TimeSpan calculated_remaining_time = hh_mm_ss_mls___Event - DateTime.Now;

            return Task.FromResult(new Tuple<int, int, int>((int)calculated_remaining_time.Hours, (int)calculated_remaining_time.Minutes, (int)calculated_remaining_time.Seconds));
        }


        public static Task<bool> Cancel_Time_Interval()
        {
            _isTimer = false;

            return Task.FromResult(true);
        }

    }
}

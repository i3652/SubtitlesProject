using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;
using System.Timers;

namespace Abdallah
{
    class Program
    {
        public static Dictionary<string, string> dc = new Dictionary<string, string>();
        public static System.Timers.Timer aTimer = new System.Timers.Timer();
        public static TimeSpan ts = new TimeSpan();
        static void Main(string[] args)
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Start();
            ts = TimeSpan.Parse("00:00:00");

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            
            StreamReader sr = new StreamReader(mydocpath + @"/FilmFile.srt");
        
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            bool bolFlage = true;
            int intCount = 1;
            while (bolFlage)
            {
                string line = sr.ReadLine();
                if (line != null)
                {
                    if (line != "")
                    {
                        if (intCount == 1)
                        {
                            list1.Add(line);
                            intCount += 1;
                        }
                        else if (intCount == 2)
                        {
                            list2.Add(line);
                            intCount += 1;
                        }
                        else if (intCount == 3)
                        {
                            list3.Add(line);
                            intCount += 1;
                        }
                        else if (intCount == 4)
                        {
                            string xx = list3[list3.Count - 1].ToString();
                            list3.Remove(xx);
                            list3.Add(xx + " " + line);
                            
                            intCount += 1;
                        }
                    }
                    else
                    {
                        intCount = 1;
                    }
                }
                else
                {
                    bolFlage = false;
                }
            }
            for (int i = 0; i <= list1.Count - 1; i++)
            {
                try
                {
                    dc.Add(list2[i].Substring(0, 8), list3[i].ToString());
                }

                catch(Exception ex) { }
                
            }

            Console.ReadLine();
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string strValue = "";
            if (dc.TryGetValue(ts.ToString(), out strValue))
            {
                Console.Clear();
                Console.WriteLine(strValue);
            }
            ts = ts + TimeSpan.Parse("00:00:01");
        }
    }
}

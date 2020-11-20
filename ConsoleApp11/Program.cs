using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SubtitlesProject
{
    class Program
    {
        static string text = "";
        static List<string> ls = new List<string>();
        static int m = 0;
        static int s = 0;
        static int h = 0;
        static void Main(string[] args)
        {
            //read SRT file path
            Console.WriteLine("Enter SRT file path:");
            string p = Console.ReadLine();
            //get text from File
            text = File.ReadAllText(p);
            //insert new line before any text 

            text = "\n" + text;


            Timer timer = new Timer(callback, null, 0, 1000);
            Console.ReadKey();
        }
        static int i = 2;
        static TimeSpan from = new TimeSpan();
        static TimeSpan To = new TimeSpan();
        static TimeSpan now;
        static void callback(object o)
        {
            // Seconds increase
            s++;
            
            // if 0 second begin new mint
            if (s == 60)
            {
                s = 0;
                m++;
            }
            //if 60 mint begin new hour
            if (m == 60)
            {
                m = 0;
                h++;
            }
            //cuurent time in prgram (like current time in the film)
            now = new TimeSpan(h, m, s);
            // remove all text else current number of subtite
            string S = text.Remove(text.IndexOf("\n" + i.ToString()));

            // make lines in array
            string[] l = S.Split('\n');
            // cut FROM subtitle time and TO subtitle time
            string[] ft = l[2].Split("-->");
            from = TimeSpan.Parse(ft[0].Remove(ft[0].IndexOf(',')));
            To = TimeSpan.Parse(ft[1].Remove(ft[1].IndexOf(',')));

            //comare with current time if equal will show subtitle
            if (TimeSpan.Compare(from, now) == 0)
            {
                for (int i = 3; i < l.Length; i++)
                {
                    Console.WriteLine(l[i]);
                }
            }
            //if to equal now will clear screen
            if (TimeSpan.Compare(To, now) == 0)
            {
                text = text.Replace(S, "");
                Console.Clear();
                i++;
            }
        }
    }
}

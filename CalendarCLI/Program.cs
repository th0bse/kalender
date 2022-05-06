using System;
using CalendarLib;

namespace CalendarCLI
{
    public class CalendarCLI
    {

        public static void Main(string[] args)
        {

            Console.WriteLine("To terminate the program, enter \"exit\" at any time");
            while (true)
            {
                int month;
                string s;
                do
                {
                    Console.Write("Please enter the month you want to display:");
                    s = Console.ReadLine();
                    if (s.Equals("exit")) Environment.Exit(0);
                } while (!int.TryParse(s, out month));

                Month monthToDisplay = month.ToMonth();

                int yearToDisplay;
                do
                {
                    Console.Write("Please enter the year you want to display:");
                    s = Console.ReadLine();
                    if (s.Equals("exit")) Environment.Exit(0);
                } while (!int.TryParse(s, out yearToDisplay));

                DisplayMonth(monthToDisplay, yearToDisplay);
            }
        }

        private static void DisplayMonth(Month month, int year)
        {
            int days = month.DaysInMonth(year);
            int dayNum = 1;
            Weekday startDay = CalendarLib.CalendarLib.GetWeekdayForFirstOfMonth(month, year);
            int skipAtStart = startDay.IntLiteral();
            Console.WriteLine("SU MO TU WE TH FR SA");
            for (int i = 1; i < 7; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (skipAtStart > 0)
                    {
                        Console.Write("  ");
                        skipAtStart--;
                    }
                    else if (dayNum <= days)
                    {
                        Console.Write(dayNum < 10 ? $"0{dayNum}" : dayNum);
                        dayNum++;
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
    }
}
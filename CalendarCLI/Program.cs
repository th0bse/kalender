using CalendarLib;
using static CalendarLib.CalendarLib;

namespace CalendarCLI
{
    public static class CalendarCLI
    {
        /// <summary>
        /// Main method, reads user input in a loop to display the month the user wants to
        /// display. Can be terminated by entering 'exit' on the command line.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("To terminate the program, enter \"exit\" at any time");
            // continue until the user enters 'exit' on the command line
            while (true)
            {
                int month;
                string s;
                do
                {
                    Console.Write("Please enter the month you want to display: ");
                    s = Console.ReadLine()!;
                    if (s.Equals("exit")) Environment.Exit(0);
                } while (!int.TryParse(s, out month));

                var monthToDisplay = month.ToMonth();

                var yearToDisplay = 0;
                var success = false;
                do
                {
                    Console.Write("Please enter the year you want to display:");
                    s = Console.ReadLine()!;
                    if (s.Equals("exit")) Environment.Exit(0);
                    success = int.TryParse(s, out yearToDisplay);
                    if (yearToDisplay is < 1528 or > 3000) success = false;
                } while (!success);

                DisplayMonth(monthToDisplay, yearToDisplay);
            }
        }

        private static void DisplayMonth(Month month, int year)
        {
            var days = month.DaysInMonth(year);
            var dayNum = 1;
            var startDay = GetWeekdayForFirstOfMonth(month, year);
            var skipAtStart = startDay.IntLiteral();
            Console.WriteLine("SU MO TU WE TH FR SA");
            for (var i = 1; i < 7; i++)
            {
                for (var j = 1; j < 8; j++)
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

            Console.WriteLine("Holidays: ");
            month.GetHolidaysForMonth(year).ForEach(h =>
            {
                h.GetDate(year, out _, out var day);
                if (day > 0) Console.WriteLine($"{h.StringLiteral()} : {day} {month.StringLiteral()}");
            });
        }
    }
}
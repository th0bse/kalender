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
                    Console.Write("Please enter the year you want to display: ");
                    s = Console.ReadLine()!;
                    if (s.Equals("exit")) Environment.Exit(0);
                    success = int.TryParse(s, out yearToDisplay);
                    if (yearToDisplay is < 1528 or > 3000) success = false;
                } while (!success);

                DisplayMonth(monthToDisplay, yearToDisplay);
            }
        }

        /// <summary>
        /// Method for displaying a given month in a given year.
        ///
        /// First prints a calendar sheet (beginning with Sunday), then lists all
        /// the holidays in the given month (if there are any).
        /// To do that, it uses the methods from the CalendarLib library.
        /// </summary>
        /// <param name="month">the month to print</param>
        /// <param name="year">the year the month to print is in</param>
        private static void DisplayMonth(Month month, int year)
        {
            // how many days are there in the month
            var days = month.DaysInMonth(year);
            // start numbering days at 1
            var dayNum = 1;
            // get the start day from the library
            var startDay = GetWeekdayForFirstOfMonth(month, year);
            // skip n days, e.g. 3 if the start day is wednesday (sunday, monday, tuesday)
            var skipAtStart = startDay.IntLiteral();
            Console.WriteLine("SU MO TU WE TH FR SA");
            for (var i = 1; i < 7; i++)
            {
                for (var j = 1; j < 8; j++)
                {
                    if (skipAtStart > 0)
                    {
                        // no number if the day is skipped
                        Console.Write("  ");
                        skipAtStart--;
                    }
                    else if (dayNum <= days)
                    {
                        // prepend the day number with a 0 if it only is a single digit, to make things look nicer
                        Console.Write(dayNum < 10 ? $"0{dayNum}" : dayNum);
                        dayNum++;
                    }
                    else
                    {
                        // no number if we already printed all days
                        Console.Write("  ");
                    }

                    Console.Write(" ");
                }

                Console.Write("\n");
            }

            // get all holidays for the given month
            var holidays = month.GetHolidaysForMonth(year);
            // only print holidays if there actually is at least one
            if (holidays.Count > 0) Console.WriteLine("Holidays: ");
            holidays.ForEach(h =>
            {
                h.GetDate(year, out _, out var day);
                // if the holiday didn't already exist, day is 0
                // only print holidays that have a day > 0
                if (day > 0) Console.WriteLine($"{h.StringLiteral()} : {day} {month.StringLiteral()}");
            });
        }
    }
}
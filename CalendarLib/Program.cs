namespace CalendarLib
{
    /// <summary>
    /// Library class for calendar-related calculations.
    ///
    /// This library is used by the frontend(s) to e.g. calculate the
    /// weekday for a specific date, or the dates for certain holidays.
    /// </summary>
    public static class CalendarLib
    {

        /// <summary>
        /// Calculate the weekday for a given date.
        /// 
        /// Abridged version of Zeller's Congruence, returns 0 for Sunday,
        /// 1 for Monday etc. See RFC3339 Appendix B for more information.
        /// </summary>
        /// <param name="day">Day of the Month</param>
        /// <param name="month">Month of the Year</param>
        /// <param name="year">Year</param>
        /// <returns>Day of the Week, where 0 = Sunday</returns>
        private static int ZellerCongruence(int day, int month, int year)
        {
            month -= 2;
            if (month < 1)
            {
                month += 12;
                --year;
            }
            var c = year / 100;
            year %= 100;
            return ((26 * month - 1) / 10 + day + year + year / 4 + c / 4 + 5 * c) % 7;
        }
        
        /// <summary>
        /// Get the weekday (as instance of the Weekday enum) for a specific
        /// date.
        /// </summary>
        /// <param name="day">the day</param>
        /// <param name="month">the month</param>
        /// <param name="year">the year</param>
        /// <returns>an instance of Weekday for the weekday corresponding
        /// to the date given</returns>
        public static Weekday GetWeekdayForDate(int day, int month, int year)
        {
            return ZellerCongruence(day, month, year).ToWeekday();
        }

        /// <summary>
        /// Get the weekday for the first of a month.
        /// </summary>
        /// <param name="month">the month</param>
        /// <param name="year">the year</param>
        /// <returns>an instance of Weekday for the weekday corresponding
        /// to the date given</returns>
        public static Weekday GetWeekdayForFirstOfMonth(Month month, int year)
        {
            return ZellerCongruence(1, month.IntLiteral(), year).ToWeekday();
        }

        /// <summary>
        /// Returns true if the year given is a leap year, false if not.
        /// This is determined as follows:
        /// If a year can be divided by 4, it is a leap year, except when
        /// it is the start of a new century (can be divided by 100). But,
        /// if it would be a leap year (but also the start of a new century),
        /// it still is a leap year if it can be divided by 400.
        /// </summary>
        /// <param name="year">the year</param>
        /// <returns>true if the year is a leap year, false if not</returns>
        public static bool IsLeapYear(int year)
        {
            var isLeapYear = false;
            if (year % 4 == 0)
            {
                isLeapYear = true;
                if (year % 100 == 0)
                {
                    isLeapYear = false || year % 400 == 0;
                }
            }

            return isLeapYear;
        }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;

namespace CalendarLib
{
    /// <summary>
    /// Library class for calendar-related calculations.
    ///
    /// This library is used by the frontend(s) to e.g. calculate the
    /// weekday for a specific date, or the dates for certain holidays.
    /// </summary>
    public class CalendarLib
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
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Weekday
    {
        MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY
    }

    public static class WeekdayExtensions
    {
        /// <summary>
        /// Extension method to get the corresponding integer literal for
        /// a given day of the week. (e.g. 0 for sunday, 1 for monday, etc.)
        ///
        /// The numbering follows the conventions described in RFC3339.
        /// </summary>
        /// <param name="weekday">the day of the week</param>
        /// <returns>the corresponding integer literal</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int IntLiteral(this Weekday weekday) => weekday switch
        {
            Weekday.SUNDAY => 0,
            Weekday.MONDAY => 1,
            Weekday.TUESDAY => 2,
            Weekday.WEDNESDAY => 3,
            Weekday.THURSDAY => 4,
            Weekday.FRIDAY => 5,
            Weekday.SATURDAY => 6,
            _ => throw new ArgumentOutOfRangeException(nameof(weekday), weekday, null)
        };

        /// <summary>
        /// Extension method to get the corresponding string literal for
        /// a given day of the week.
        /// </summary>
        /// <param name="weekday">the day of the week</param>
        /// <returns>the corresponding string literal</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string StringLiteral(this Weekday weekday) => weekday switch
        {
            Weekday.SUNDAY => "Sunday",
            Weekday.MONDAY => "Monday",
            Weekday.TUESDAY => "Tuesday",
            Weekday.WEDNESDAY => "Wednesday",
            Weekday.THURSDAY => "Thursday",
            Weekday.FRIDAY => "Friday",
            Weekday.SATURDAY => "Saturday",
            _ => throw new ArgumentOutOfRangeException(nameof(weekday), weekday, null)
        };

        /// <summary>
        /// Extension method to get the corresponding Weekday instance for
        /// a given integer literal.
        /// </summary>
        /// <param name="intLiteral">the integer literal</param>
        /// <returns>the corresponding Weekday instance</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Weekday ToWeekday(this int intLiteral) => intLiteral switch
        {
            0 => Weekday.SUNDAY,
            1 => Weekday.MONDAY,
            2 => Weekday.TUESDAY,
            3 => Weekday.WEDNESDAY,
            4 => Weekday.THURSDAY,
            5 => Weekday.FRIDAY,
            6 => Weekday.SATURDAY,
            _ => throw new ArgumentOutOfRangeException(nameof(intLiteral), intLiteral, null)
        };

        /// <summary>
        /// Extension method to get the corresponding Weekday instance for
        /// a given string literal.
        /// </summary>
        /// <param name="stringLiteral">the string literal</param>
        /// <returns>the corresponding Weekday instance</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Weekday ToWeekday(this string stringLiteral) => stringLiteral.ToLower() switch
        {
            "sunday" => Weekday.SUNDAY,
            "monday" => Weekday.MONDAY,
            "tuesday" => Weekday.TUESDAY,
            "wednesday" => Weekday.WEDNESDAY,
            "thursday" => Weekday.THURSDAY,
            "friday" => Weekday.FRIDAY,
            "saturday" => Weekday.SATURDAY,
            _ => throw new ArgumentOutOfRangeException(nameof(stringLiteral), stringLiteral, null)
        };
    }
}
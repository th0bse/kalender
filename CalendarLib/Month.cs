using System;
using System.Diagnostics.CodeAnalysis;

namespace CalendarLib
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Month
    {
        JANUARY,
        FEBRUARY,
        MARCH,
        APRIL,
        MAY,
        JUNE,
        JULY,
        AUGUST,
        SEPTEMBER,
        OCTOBER,
        NOVEMBER,
        DECEMBER
    }

    public static class MonthExtensions
    {
        public static int IntLiteral(this Month month) => month switch
        {
            Month.JANUARY => 1,
            Month.FEBRUARY => 2,
            Month.MARCH => 3,
            Month.APRIL => 4,
            Month.MAY => 5,
            Month.JUNE => 6,
            Month.JULY => 7,
            Month.AUGUST => 8,
            Month.SEPTEMBER => 9,
            Month.OCTOBER => 10,
            Month.NOVEMBER => 11,
            Month.DECEMBER => 12,
            _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
        };

        public static string StringLiteral(this Month month) => month switch
        {
            Month.JANUARY => "January",
            Month.FEBRUARY => "February",
            Month.MARCH => "March",
            Month.APRIL => "April",
            Month.MAY => "May",
            Month.JUNE => "June",
            Month.JULY => "July",
            Month.AUGUST => "August",
            Month.SEPTEMBER => "September",
            Month.OCTOBER => "October",
            Month.NOVEMBER => "November",
            Month.DECEMBER => "December",
            _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
        };

        public static Month ToMonth(this int intLiteral) => intLiteral switch
        {
            1 => Month.JANUARY,
            2 => Month.FEBRUARY,
            3 => Month.MARCH,
            4 => Month.APRIL,
            5 => Month.MAY,
            6 => Month.JUNE,
            7 => Month.JULY,
            8 => Month.AUGUST,
            9 => Month.SEPTEMBER,
            10 => Month.OCTOBER,
            11 => Month.NOVEMBER,
            12 => Month.DECEMBER,
            _ => throw new ArgumentOutOfRangeException(nameof(intLiteral), intLiteral, null)
        };

        public static Month ToMonth(this string stringLiteral) => stringLiteral.ToLower() switch
        {
            "january" => Month.JANUARY,
            "february" => Month.FEBRUARY,
            "march" => Month.MARCH,
            "april" => Month.APRIL,
            "may" => Month.MAY,
            "june" => Month.JUNE,
            "july" => Month.JULY,
            "august" => Month.AUGUST,
            "september" => Month.SEPTEMBER,
            "october" => Month.OCTOBER,
            "november" => Month.NOVEMBER,
            "december" => Month.DECEMBER,
            _ => throw new ArgumentOutOfRangeException(nameof(stringLiteral), stringLiteral, null)
        };

        public static int DaysInMonth(this Month month, int year) => month switch
        {
            Month.JANUARY => 31,
            Month.FEBRUARY => (CalendarLib.IsLeapYear(year)) ? 29 : 28,
            Month.MARCH => 31,
            Month.APRIL => 30,
            Month.MAY => 31,
            Month.JUNE => 30,
            Month.JULY => 31,
            Month.AUGUST => 31,
            Month.SEPTEMBER => 30,
            Month.OCTOBER => 31,
            Month.NOVEMBER => 30,
            Month.DECEMBER => 31,
            _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
        };
    }
}
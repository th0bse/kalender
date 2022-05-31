namespace CalendarLib;

/// <summary>
/// Enum class representing the most important holidays in Lower Saxony.
/// </summary>
public enum Holiday
{
    NEW_YEAR,
    GOOD_FRIDAY,
    EASTER_SUNDAY,
    EASTER_MONDAY,
    LABOUR_DAY,
    ASCENSION_DAY,
    WHIT_SUNDAY,
    WHIT_MONDAY,
    GERMAN_UNITY_DAY,
    REFORMATION_DAY,
    CHRISTMAS_EVE,
    CHRISTMAS_DAY,
    SECOND_CHRISTMAS_DAY
}

/// <summary>
/// Extension and utility methods for the Holiday enum.
/// </summary>
public static class HolidayExtensions
{
    /// <summary>
    /// Get the date of a specific holiday for a given year. Calculates the date of easter sunday to be able to
    /// calculate the other holidays that are related to easter. The month and day are written to the respective
    /// out parameters.
    /// </summary>
    /// <param name="holiday">enum instance of the holiday to get the date for</param>
    /// <param name="year">year to calculate the holiday for</param>
    /// <param name="month">out parameter that the month is written to</param>
    /// <param name="day">out parameter that the day is written to</param>
    public static void GetDate(this Holiday holiday, int year, out Month month, out int day)
    {
        var easterDay = Month.MARCH.MonthOverUnderflow(year, CalculateEaster(year), out month);
        day = holiday switch
        {
            Holiday.NEW_YEAR => 1,
            Holiday.GOOD_FRIDAY => month.MonthOverUnderflow(year, easterDay - 2, out month),
            Holiday.EASTER_SUNDAY => easterDay,
            Holiday.EASTER_MONDAY => month.MonthOverUnderflow(year, easterDay + 1, out month),
            Holiday.LABOUR_DAY when (year is 1919 or > 1933) => 1,
            Holiday.ASCENSION_DAY => month.MonthOverUnderflow(year, easterDay + 39, out month),
            Holiday.WHIT_SUNDAY => month.MonthOverUnderflow(year, easterDay + 49, out month),
            Holiday.WHIT_MONDAY => month.MonthOverUnderflow(year, easterDay + 50, out month),
            Holiday.GERMAN_UNITY_DAY when year > 1990 => 3,
            Holiday.REFORMATION_DAY => 31,
            Holiday.CHRISTMAS_EVE => 24,
            Holiday.CHRISTMAS_DAY => 25,
            Holiday.SECOND_CHRISTMAS_DAY => 26,
            _ => 0
        };
        month = holiday switch
        {
            Holiday.NEW_YEAR => Month.JANUARY,
            Holiday.LABOUR_DAY => Month.MAY,
            Holiday.GERMAN_UNITY_DAY => Month.OCTOBER,
            Holiday.REFORMATION_DAY => Month.OCTOBER,
            Holiday.CHRISTMAS_EVE => Month.DECEMBER,
            Holiday.CHRISTMAS_DAY => Month.DECEMBER,
            Holiday.SECOND_CHRISTMAS_DAY => Month.DECEMBER,
            _ => month
        };
    }

    /// <summary>
    /// Get all holidays in a given month. Loops through all Holiday enum instances and returns a list of all
    /// of them that are in the given month.
    /// </summary>
    /// <param name="month">the month to get all holidays for</param>
    /// <param name="year">the year to calculate the holidays for</param>
    /// <returns>a list of all holidays in the given month</returns>
    public static List<Holiday> GetHolidaysForMonth(this Month month, int year) =>
        Enum.GetValues<Holiday>().Where(h =>
        {
            GetDate(h, year, out var m, out var d);
            return m == month;
        }).ToList();

    /// <summary>
    /// Easter Algorithm by Gauss, extended by Kinkelin And Zeller.
    /// (https://de.wikipedia.org/wiki/Gau%C3%9Fsche_Osterformel)
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    private static int CalculateEaster(int year)
    {
        var k = year / 100;
        var m = 15 + (3 * k + 3) / 4 - (8 * k + 13) / 25;
        var s = 2 - (3 * k + 3) / 4;
        var a = year % 19;
        var d = (19 * a + m) % 30;
        var r = (d + a / 11) / 29;
        var og = 21 + d - r;
        var sz = 7 - (year + year / 4 + s) % 7;
        var oe = 7 - (og - sz) % 7;
        return og + oe;
    }

    /// <summary>
    /// Return the string representation of the given enum instance.
    /// </summary>
    /// <param name="holiday">the enum instance to return the string representation for</param>
    /// <returns>the string representation of the given enum instance</returns>
    public static string StringLiteral(this Holiday holiday) => holiday switch
    {
        Holiday.NEW_YEAR => "New Year",
        Holiday.GOOD_FRIDAY => "Good Friday",
        Holiday.EASTER_SUNDAY => "Easter Sunday",
        Holiday.EASTER_MONDAY => "Easter Monday",
        Holiday.LABOUR_DAY => "Labour Day",
        Holiday.ASCENSION_DAY => "Ascension Day",
        Holiday.WHIT_SUNDAY => "Whit Sunday",
        Holiday.WHIT_MONDAY => "Whit Monday",
        Holiday.GERMAN_UNITY_DAY => "German Unity Day",
        Holiday.REFORMATION_DAY => "Reformation Day",
        Holiday.CHRISTMAS_EVE => "Christmas Eve",
        Holiday.CHRISTMAS_DAY => "1st Christmas Day",
        Holiday.SECOND_CHRISTMAS_DAY => "2nd Christmas Day"
    };
}
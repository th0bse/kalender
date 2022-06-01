using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CalendarUI.Components;

/// <summary>
/// Code-behind for the custom CalendarBlock control.
///
/// A CalendarBlock is the control representing a single day in the GUI.
/// </summary>
public partial class CalendarBlock : UserControl
{
    /// <summary>
    /// Whether the day is active (in the current month), or inactive (in the previous or next
    /// month.
    /// </summary>
    public bool IsActive
    {
        set => Border.Background = value ? new SolidColorBrush(_activeColor) : new SolidColorBrush(_inactiveColor);
    }

    /// <summary>
    /// Whether the day is a holiday or not.
    /// </summary>
    public bool IsHoliday
    {
        set => HolidayText.Visibility = value ? Visibility.Visible : Visibility.Hidden;
    }

    /// <summary>
    /// The day number that should be displayed in the upper left corner.
    /// </summary>
    public string DayNum
    {
        set => DayNumText.Text = value;
    }

    /// <summary>
    /// The name of the holiday, that should be displayed.
    /// </summary>
    public string HolidayName
    {
        set => HolidayText.Text = value;
    }

    private readonly Color _inactiveColor = Color.FromRgb(245, 245, 245);
    private readonly Color _activeColor = Color.FromRgb(235, 235, 235);

    public CalendarBlock()
    {
        InitializeComponent();
    }
}
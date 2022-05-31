using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CalendarUI.Components;

public partial class CalendarBlock : UserControl
{
    public bool IsActive
    {
        set => Border.Background = value ? new SolidColorBrush(_activeColor) : new SolidColorBrush(_inactiveColor);
    }

    public bool IsHoliday
    {
        set => HolidayText.Visibility = value ? Visibility.Visible : Visibility.Hidden;
    }

    public string DayNum
    {
        set => DayNumText.Text = value;
    }

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
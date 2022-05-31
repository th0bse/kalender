using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CalendarLib;
using CalendarUI.Components;
using static CalendarLib.CalendarLib;

namespace CalendarUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Month _displayedMonth;
        private int _displayedYear;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event Listener, gets called when the Window is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitMonthComboBox();
        }

        /// <summary>
        /// Initialize the MonthComboBox with the names of all months.
        /// </summary>
        private void InitMonthComboBox()
        {
            Enum.GetValues<Month>().ToList().ForEach(m => MonthComboBox.Items.Add(m.StringLiteral()));
        }

        /// <summary>
        /// Display the given month in the given year. Only months between February 1582 and December 3000 (including
        /// those months) will be displayed, since the Gregorian Calendar was instituted in February 1582, and the
        /// assignment states that the calendar should be able to display months up to the year 3000.
        ///
        /// For each month, the grid that holds all individual CalendarBlock instances gets cleared and then refilled.
        /// </summary>
        /// <param name="month">the month to be displayed</param>
        /// <param name="year">the year of the month to be displayed</param>
        private void DisplayMonth(Month month, int year)
        {
            // the gregorian calendar was instituted in February 1582, don't render stuff before that
            if (year < 1582 || year > 3000) return;
            if (year == 1582 && month is Month.JANUARY) return;
            
            var firstWeekday = GetWeekdayForFirstOfMonth(month, year);
            var holidays = month.GetHolidaysForMonth(year);
            
            CalendarGrid.Children.Clear();
            
            // variable to number the inactive days (from the next month) correctly, starting at 1
            var inactiveDayNum = 1;
            for (var i = 0; i < 6 * 7; i++)
            {
                var block = new CalendarBlock
                {
                    IsActive = false,
                    IsHoliday = false
                };
                // day is in the last month, keep inactive
                if (i < firstWeekday.IntLiteral())
                {
                    block.IsActive = false;
                    if (month != Month.JANUARY)
                        block.DayNum =
                            (month.PreviousMonth().DaysInMonth(year) - (firstWeekday.IntLiteral() - i))
                            .ToString();
                }
                // day is in the current month, make active, set correct number and mark as holiday if applicable
                else if (i < month.DaysInMonth(year) + firstWeekday.IntLiteral())
                {
                    block.IsActive = true;
                    var dayNum = (i - firstWeekday.IntLiteral() + 1);
                    block.DayNum = dayNum.ToString();
                    holidays.ForEach(h =>
                    {
                        h.GetDate(year, out _, out var d);
                        if (d != dayNum) return;
                        block.IsHoliday = true;
                        block.HolidayName = h.StringLiteral();
                    });
                }
                // day is not in the previous or current month, therefore it has to be in the next month
                else
                {
                    block.DayNum = inactiveDayNum.ToString();
                    inactiveDayNum++;
                }

                // position the CalendarBlock instance correctly in the grid
                Grid.SetRow(block, i / 7);
                Grid.SetColumn(block, i % 7);
                CalendarGrid.Children.Add(block);
            }
        }

        /// <summary>
        /// Listener that gets called when the user presses on the OK button, starts rendering of the month that should
        /// be displayed. Reads the month from the dropdown menu and the year from the text box. Returns without
        /// displaying anything, if one of the inputs is malformed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (MonthComboBox.SelectedItem == null || YearTextBox.Text == "") return;
            _displayedMonth = MonthComboBox.SelectedItem.ToString()!.ToMonth();
            if (!int.TryParse(YearTextBox.Text, out _displayedYear)) return;
            DisplayMonth(_displayedMonth, _displayedYear);
        }

        /// <summary>
        /// Listener that gets called when the user presses the back button.
        ///
        /// Determines the previous month (either just the one before the current one, or if the current one is January,
        /// December. Also decrements the year if necessary. Then starts rendering of the month to be displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (_displayedMonth != Month.JANUARY) _displayedMonth = (_displayedMonth.IntLiteral() - 1).ToMonth();
            else
            {
                _displayedMonth = Month.DECEMBER;
                _displayedYear--;
            }

            MonthComboBox.SelectedItem = _displayedMonth.StringLiteral();
            YearTextBox.Text = _displayedYear.ToString();
            DisplayMonth(_displayedMonth, _displayedYear);
        }

        /// <summary>
        /// Listener that gets called when the user presses the forward button. Functions analogous to the ButtonBack
        /// listener method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForward_OnClick(object sender, RoutedEventArgs e)
        {
            if (_displayedMonth != Month.DECEMBER)
            {
                _displayedMonth = (_displayedMonth.IntLiteral() + 1).ToMonth();
            }
            else
            {
                _displayedMonth = Month.JANUARY;
                _displayedYear++;
            }

            MonthComboBox.SelectedItem = _displayedMonth.StringLiteral();
            YearTextBox.Text = _displayedYear.ToString();
            DisplayMonth(_displayedMonth, _displayedYear);
        }
    }
}
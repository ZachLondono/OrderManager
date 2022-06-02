using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class MonthlyPlannerViewModel : ViewModelBase {

    private DateTime _date;

    private string _monthName = string.Empty;
    public string MonthName {
        get => _monthName;
        private set => this.RaiseAndSetIfChanged(ref _monthName, value);
    }

    public ObservableCollection<ScheduleDay> Days { get; set;  } = new();

    public MonthlyPlannerViewModel() {
        SetDate(DateTime.Today);

        OnSetCurrDate = ReactiveCommand.Create(() => SetDate(DateTime.Today));
        OnPrevMonth = ReactiveCommand.Create(() => SetDate(_date.AddMonths(-1)));
        OnNextMonth = ReactiveCommand.Create(() => SetDate(_date.AddMonths(1)));

    }

    public ICommand OnSetCurrDate { get; }
    public ICommand OnPrevMonth { get; }
    public ICommand OnNextMonth { get; }

    private void SetDate(DateTime date) {

        _date = date;
        MonthName = _date.ToString("MMMM yyy");
        
        var startDate = new DateTime(_date.Year, _date.Month, 1); // The first day of the current month
        int dayCount = 6 * 7; // A month will, at most, span 6 weeks.
        int dayOfWeek = (int) startDate.DayOfWeek;

        Days.Clear();

        for (int i = 0; i < dayOfWeek; i++) {

            ScheduleDay day = new() {
                WeekNo = 0,
                Items = $"{new Random().Next(0, 100)} boxes",
                Date = startDate.AddDays(-dayOfWeek + i),
                IsCurrentMonth = false
            };

            Days.Add(day);
        }

        for (int i = dayOfWeek; i < dayCount; i++) {

            var currDate = startDate.AddDays(i - dayOfWeek);
            ScheduleDay day = new() {
                WeekNo = i / 7,
                Items = $"{new Random().Next(0, 100)} boxes",
                Date = currDate,
                IsCurrentMonth = startDate.Month == currDate.Month
            };

            Days.Add(day);

        }
    }

}

public class ScheduleDay {

    public DateTime Date { get; set; }
    public string Items { get; set; } = string.Empty;
    public int WeekNo { get; set; }
    public bool IsCurrentMonth { get; set; }

    public int DayOfWeek => (int)Date.DayOfWeek;
    public int DayOfMonth => Date.Day;

    public string ToolTip =>
@"OT000 - 10
OT001 - 5
OT002 - 15
OT003 - 10
OT004 - 5
OT005 - 15";

    public override string ToString() {
        return Date.ToString("dd");
    }

}

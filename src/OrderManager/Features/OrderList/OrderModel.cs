using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;

namespace OrderManager.Features.OrderList;

public class OrderModel {

    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsPriority { get; set; }

    public string DateModifiedStr {
        get => DateTime.Today.IsSameWeek(DateModified) ? DateModified.ToString("ddd h:mm") : DateModified.ToString("d/M/yy h:mm");
    }
    public DateTime DateModified { get; set; } = DateTime.Now;

    public string CompanyNames {
        get => $"{Customer?.Name} / {Vendor?.Name} / {Supplier?.Name}";
    }

    public Company? Customer { get; set; }
    public Company? Vendor { get; set; }
    public Company? Supplier { get; set; }

}

public class Company {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
}

public static class DateTimeExtension {

    public static bool IsSameWeek(this DateTime date1, DateTime date2) {
        var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
        var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
        var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

        return d1 == d2;
    }

}
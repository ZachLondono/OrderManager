using System;

namespace DesktopUI.Models;

public class OrderListItem {

    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public DateTime LastModified { get; set; } = DateTime.Now;
    // If the date is the same week, show the day of the week
    public string LastModifiedStr => LastModified.ToString();

    public CompanyName? Customer { get; set; }

    public CompanyName? Vendor { get; set; }

    public CompanyName? Supplier { get; set; }

}

public class CompanyName {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

}
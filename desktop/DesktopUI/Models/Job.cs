using System;

namespace DesktopUI.Models;

public class Job {

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Vendor { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public int Qty { get; set; }

    public DateTime? ScheduledDate { get; set; } 

    public string ProductClass { get; set; } = string.Empty;

    public string WorkCell { get; set; } = string.Empty;

}

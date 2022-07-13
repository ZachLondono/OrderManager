namespace OrderManager.Domain.Jobs;

public record JobQuery {

    public int Page { get; set; }

    public int PageSize { get; set; }

    /*public string? Vendor { get; init; }

    public DateTime? StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public JobOrder Order { get; init; }

    public enum JobOrder {

        Number,
        Name,
        Vendor,
        Customer,
        WorkCell,
        ScheduledDate

    }*/

}

public record JobQueryResponse {

    public int Page { get; set; }

    public int PageCount { get; set; }

    public IEnumerable<JobSummary> Jobs { get; set; } = Enumerable.Empty<JobSummary>();

}
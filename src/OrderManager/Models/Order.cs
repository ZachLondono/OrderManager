namespace OrderManagment.Models;

public class Order {

    public string RefNum { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.Today;

    public IList<LineItem> LineItems { get; set; } = new List<LineItem>();

    public class LineItem {

        public int Line { get; set; }

        public int ProductId { get; set; }

        public IReadOnlyDictionary<string, string>? Attributes { get; set; }

    }

}

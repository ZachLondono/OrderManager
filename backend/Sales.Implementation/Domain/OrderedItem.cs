namespace Sales.Implementation.Domain;

public class OrderedItem {
    
    public Guid Id { get; set; }
    
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public int LineNumber { get; set; }

    public Dictionary<string, string> Options { get; set; } = new();

    public OrderedItem() {

    }

    public void SetQuantity(int qty) => throw new NotImplementedException();
    
    public void SetLineNumber(int line) => throw new NotImplementedException();
    
    public void SetProduct(Guid productId) => throw new NotImplementedException();

    public void SetOption(string option, string value) => throw new NotImplementedException();

}

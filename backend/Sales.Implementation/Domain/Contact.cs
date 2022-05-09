namespace Sales.Implementation.Domain;

public class Contact {
    
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public Contact(string name) {
        Name = name;
    }

    public Contact(int id, string name, string? email, string? phone) {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
    }

}

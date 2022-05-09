using OrderManager.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Models;

public class ContactModel {

    public bool HasChanged { get; set; }

    public int Id { get; init; }

    private string _name;
    public string Name {
        get => _name;
        set {
            HasChanged = true;
            _name = value;
        }
    }

    private string? _email;
    public string? Email {
        get => _email;
        set {
            HasChanged = true;
            _email = value;
        }
    }

    private string? _phone;
    public string? Phone {
        get => _phone;
        set {
            HasChanged = true;
            _phone = value;
        }
    }

    public ContactModel(Contact company) {
        Id = company.Id;
        _name = company.Name;
        _email = company.Email;
        _phone = company.Phone;
    }

    public ContactModel(int id, string name, string? email, string? phone) {
        Id = id;
        _name = name;
        _email = email;
        _phone = phone;
    }

}

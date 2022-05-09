using DesktopUI.Models;
using OrderManager.ApplicationCore.Companies;
using OrderManager.Domain.Companies;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static OrderManager.ApplicationCore.Companies.ICompanyAPI;

namespace DesktopUI.ViewModels;

public class CompanyEditorViewModel : ViewModelBase {

    private Company? _company;

    public string Name {
        get => _company?.Name ?? string.Empty;
        set {
            if (_company is null) return;
            _company.Name = value;
            _nameChanged = true;
            CanSave = true;
        }
    }

    private bool _initialCustomerRole;
    private bool _isCustomer;
    public bool IsCustomer {
        get => _isCustomer;
        set {
            _isCustomer = value;
            if (value != _initialCustomerRole) {
                _customerRoleChanged = true;
                CanSave = true;
            }
        }
    }

    private bool _initialVendorRole;
    private bool _isVendor;
    public bool IsVendor {
        get => _isVendor;
        set {
            _isVendor = value;
            if (value != _initialVendorRole) {
                _vendorRoleChanged = true;
                CanSave = true;
            }
        }
    }

    private bool _initialSupplierRole;
    private bool _isSupplier;
    public bool IsSupplier {
        get => _isSupplier;
        set {
            _isSupplier = value;
            if (value != _initialSupplierRole) {
                _supplierRoleChanged = true;
                CanSave = true;
            }
        }
    }

    public string AddressLine1 {
        get => _company?.Line1 ?? string.Empty;
        set {
            if (_company is null) return;
            _company.Line1 = value;
            _addressChanged = true;
            CanSave = true;
        }
    }

    public string AddressLine2 {
        get => _company?.Line2 ?? string.Empty;
        set {
            if (_company is null) return;
            _company.Line2 = value;
            _addressChanged = true;
            CanSave = true;
        }
    }

    public string AddressLine3 {
        get => _company?.Line3 ?? string.Empty;
        set {
            if (_company is null) return;
            _company.Line3 = value;
            _addressChanged = true;
            CanSave = true;
        }
    }

    public string AddressCity {
        get => _company?.City ?? string.Empty;
        set {
            if (_company is null) return;
            _company.City = value;
            _addressChanged = true;
            CanSave = true;
        }
    }

    public string AddressState {
        get => _company?.State ?? string.Empty;
        set {
            if (_company is null) return;
            _company.State = value;
            _addressChanged = true;
            CanSave = true;
        }
    }

    public string AddressZip {
        get => _company?.Zip ?? string.Empty;
        set {
            if (_company is null) return;
            _company.Zip = value;
            _addressChanged = true;
            CanSave = true;
        }
    }

    public ObservableCollection<ContactModel> Contacts { get; set; } = new();

    private readonly ICompanyAPI _api;

    private bool _canSave = false;
    public bool CanSave {
        get => _canSave;
        set => this.RaiseAndSetIfChanged(ref _canSave, value);
    }
    private bool _nameChanged = false;
    private bool _addressChanged = false;
    private bool _customerRoleChanged = false;
    private bool _vendorRoleChanged = false;
    private bool _supplierRoleChanged = false;
    //private bool _addContact = false;

    public CompanyEditorViewModel(ICompanyAPI api) {
        _api = api;
        
        var canSave = this.WhenAny(x => x.CanSave, x => x.Value);
        SaveChangesCommand = ReactiveCommand.CreateFromTask(OnSaveChanges, canExecute: canSave);
        AddContactCommand = ReactiveCommand.CreateFromTask(OnAddContact);
    }

    public ICommand SaveChangesCommand { get; }
    public ICommand AddContactCommand { get; }

    public void SetData(Company company) {
        _company = company;

        Contacts.Clear();
        foreach (var contact in company.Contacts) {
            Contacts.Add(new(contact));
        }

        var roles = _company.Roles.Split(',');
        
        _isCustomer = roles.Contains("Customer");
        _initialCustomerRole = _isCustomer;
        
        _isVendor = roles.Contains("Vendor");
        _initialVendorRole = _isVendor;

        _isSupplier = roles.Contains("Supplier");
        _initialSupplierRole = _isSupplier;
    }

    private async Task OnSaveChanges() {
        if (_company is null) return;

        try {

            if (_nameChanged) {
                await _api.SetName(new() {
                    CompanyId = _company.Id,
                    Name = _company.Name,
                });
                _nameChanged = false;
            }

            if (_addressChanged) {
                await _api.SetAddress(new() {
                    CompanyId = _company.Id,
                    Line1 = _company.Line1,
                    Line2 = _company.Line2,
                    Line3 = _company.Line3,
                    City = _company.City,
                    State = _company.State,
                    Zip = _company.Zip
                });
                _addressChanged = false;
            }

            if (_customerRoleChanged && _initialCustomerRole != _isCustomer) {
                RoleChangeCommand roleChange = new() {
                    CompanyId = _company.Id,
                    Role = "Customer"
                };

                if (_isCustomer) { 
                    await _api.AddRole(roleChange);
                } else {
                    await _api.RemoveRole(roleChange);
                }

                _initialCustomerRole = _isCustomer;
            }

            if (_vendorRoleChanged && _initialVendorRole != _isVendor) {
                RoleChangeCommand roleChange = new() {
                    CompanyId = _company.Id,
                    Role = "Vendor"
                };

                if (_isVendor) {
                    await _api.AddRole(roleChange);
                } else {
                    await _api.RemoveRole(roleChange);
                }
                
                _initialVendorRole = _isVendor;
            }

            if (_supplierRoleChanged && _initialSupplierRole != _isSupplier) {
                RoleChangeCommand roleChange = new() {
                    CompanyId = _company.Id,
                    Role = "Supplier"
                };

                if (_isSupplier) {
                    await _api.AddRole(roleChange);
                } else {
                    await _api.RemoveRole(roleChange);
                }

                _initialSupplierRole = _isSupplier;
            }

            foreach (var contact in Contacts) {

                if (contact.HasChanged) {
                    await _api.UpdateContact(new() {
                        CompanyId = _company.Id,
                        ContactId = contact.Id,
                        Name = contact.Name,
                        Email = contact.Email,
                        Phone = contact.Phone
                    });
                    contact.HasChanged = false;
                }

            }

            CanSave = false;
        } catch (Exception ex) {
            CanSave = true;
            Debug.WriteLine(ex);
        }
    }

    private async Task OnAddContact() {

        if (_company is null) return;

        string newName = "New Contact";
        try {
            
            int newId = await _api.AddContact(new() {
                CompanyId = _company.Id,
                Name = newName,
                Email = string.Empty,
                Phone = string.Empty
            });

            Contacts.Add(new(newId, newName, null, null));

        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

}

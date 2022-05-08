using OrderManager.ApplicationCore.Companies;
using OrderManager.Domain.Companies;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

    // TODO: split into a collection of strings
    public string Roles {
        get => _company?.Roles ?? string.Empty;
        set {
            if (_company is null) return;
            _company.Roles = value;
            //_rolesChanged = true;
            //CanSave = true;
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

    private readonly ICompanyAPI _api;

    private bool _canSave = false;
    public bool CanSave {
        get => _canSave;
        set => this.RaiseAndSetIfChanged(ref _canSave, value);
    }
    private bool _nameChanged = false;
    private bool _addressChanged = false;
    //private bool _rolesChanged = false;
    //private bool _addContact = false;

    public CompanyEditorViewModel(ICompanyAPI api) {
        _api = api;
        var canSave = this.WhenAny(x => x.CanSave, x => x.Value);
        SaveChangesCommand = ReactiveCommand.CreateFromTask(OnSaveChanges, canExecute: canSave);
    }

    public ICommand SaveChangesCommand { get; }

    public void SetData(Company company) {
        _company = company;
    }

    private async Task OnSaveChanges() {
        if (_company is null) return;
        CanSave = false;
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

        } catch (Exception ex) {
            CanSave = true;
            Debug.WriteLine(ex);
        }
    }

}

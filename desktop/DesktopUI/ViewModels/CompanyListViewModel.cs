using OrderManager.ApplicationCore.Companies;
using OrderManager.Domain.Companies;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class CompanyListViewModel : ViewModelBase {

    public ObservableCollection<CompanySummary> Companies { get; } = new();

    private readonly ICompanyAPI _api;

    public CompanyListViewModel(ICompanyAPI api) {
        _api = api;

        EditCompanyCommand = ReactiveCommand.CreateFromTask<CompanySummary>(OnEditCompany);
        DeleteCompanyCommand = ReactiveCommand.CreateFromTask<CompanySummary>(OnDeleteCompany);
        CreateCompanyCommand = ReactiveCommand.CreateFromTask(OnCreateCompany);

    }

    public ICommand EditCompanyCommand { get; }
    public ICommand DeleteCompanyCommand { get; }
    public ICommand CreateCompanyCommand { get; }

    private Task<object> OnEditCompany(CompanySummary company) {
        throw new NotImplementedException();
    }

    private async Task OnCreateCompany() {

        try {
            string newName = "New Company";
            int newId = await _api.CreateCompany(new() { Name = newName });

            Companies.Add(new() {
                Id = newId,
                Name = newName
            });
        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }
    }

    private async Task OnDeleteCompany(CompanySummary company) {
        try {
            await _api.RemoveCompany(company.Id);
            Companies.Remove(company);
        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }
    }

    public async Task LoadData() {
        try {
            var companies = await _api.GetCompanies();
            Companies.Clear();
            foreach (var company in companies) {
                Companies.Add(company);
            }
        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }
    }

}

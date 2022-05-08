using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Companies;
using OrderManager.Domain.Companies;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class CompanyListViewModel : ViewModelBase {

    public ObservableCollection<CompanySummary> Companies { get; } = new();

    private readonly ICompanyAPI _api;

    public CompanyListViewModel(ICompanyAPI api) {
        _api = api;

        ShowDialog = new Interaction<ToolWindowContent, Unit>();

        EditCompanyCommand = ReactiveCommand.CreateFromTask<CompanySummary>(OnEditCompany);
        DeleteCompanyCommand = ReactiveCommand.CreateFromTask<CompanySummary>(OnDeleteCompany);
        CreateCompanyCommand = ReactiveCommand.CreateFromTask(OnCreateCompany);
    }

    public Interaction<ToolWindowContent, Unit> ShowDialog { get; }

    public ICommand EditCompanyCommand { get; }
    public ICommand DeleteCompanyCommand { get; }
    public ICommand CreateCompanyCommand { get; }

    private async Task OnEditCompany(CompanySummary company) {

        var details = await _api.GetCompany(company.Id);

        var editorvm = App.GetRequiredService<CompanyEditorViewModel>();
        editorvm.SetData(details);

        await ShowDialog.Handle(new("Company Editor", 450, 600, new CompanyEditorView {
            DataContext = editorvm
        }));

        var index = Companies.IndexOf(company);
        Companies.RemoveAt(index);
        company.Name = details.Name;
        Companies.Insert(index, company);

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

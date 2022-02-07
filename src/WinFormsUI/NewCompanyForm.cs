using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Features.Companies;
using OrderManager.ApplicationCore.Features.Orders;
using System.Diagnostics;

namespace OrderManager.WinFormsUI;
public partial class NewCompanyForm : Form {

    private readonly CompanyController _controller;
    
    public NewCompanyForm() {

        if (Program.ServiceProvider is null) throw new ArgumentNullException(nameof(Program.ServiceProvider)); ;
        var controller = Program.ServiceProvider.GetService<CompanyController>();
        if (controller is null) throw new ArgumentNullException(nameof(_controller));
        _controller = controller;

        InitializeComponent();
    }



    private async Task<Company> CreateCompany() {

        string companyName = CompanyNameBox.Text;
        string contactName = ContactNameBox.Text;
        string addressLine1 = Address1Box.Text;
        string addressLine2 = Address2Box.Text;
        string addressLine3 = Address3Box.Text;
        string city = CityBox.Text;
        string state = StateBox.Text;
        string zip = ZipBox.Text;

        var company = await _controller.CreateCompany(companyName, contactName, addressLine1, addressLine2, addressLine3, city, state, zip);

        if (company is null) throw new InvalidDataException("Company could not be created");

        return company;

    }

    private async void CreateBtn_Click(object sender, EventArgs ev) {

        try {
            Company company = await CreateCompany();
            MessageBox.Show($"New Company '{company.Name}'[{company.Id}] created");
        } catch (Exception ex) {
            MessageBox.Show("Failed to create new company\n" + ex.Message);
        }

    }
}

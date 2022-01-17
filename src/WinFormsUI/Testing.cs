using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Features.Companies;
using OrderManager.ApplicationCore.Features.Products;
using OrderManager.ApplicationCore.Features.Reports;
using OrderManager.ApplicationCore.Features.Scripts;

namespace OrderManager.WinFormsUI;

public partial class Testing : Form {

    private readonly ScriptController? _scriptController;
    private readonly CompanyController? _companyController;
    private readonly ProductController? _productController;
    //private readonly ReportController? _reportController;

    public Testing() {
        _scriptController = Program.ServiceProvider?.GetService<ScriptController>();
        _companyController = Program.ServiceProvider?.GetService<CompanyController>();
        _productController = Program.ServiceProvider?.GetService<ProductController>();
        //_reportController = Program.ServiceProvider?.GetService<ReportController>();
        InitializeComponent();
    }

    private async void LoadScriptsFromDirectory(object sender, EventArgs e) {
        if (_scriptController is null) {
            MessageBox.Show("Controller is null");
            return;
        }

        await _scriptController.LoadScripts();
        IEnumerable<Script> availableScripts = await _scriptController.GetAvailableScripts();
        foreach (Script script in availableScripts) {
            ScriptList.Items.Add(new ListViewItem(script.Name));
        }

    }

    private async void ExecuteSelectedScript(object s, EventArgs e) {
        
        if (_scriptController is null) {
            MessageBox.Show("Controller is null");
            return;
        }

        var selected = ScriptList.SelectedItems;
        if (selected.Count != 1) {
            MessageBox.Show("Select a single script to run");
            return;
        }

        string scriptName = selected[0].Text;

        ScriptResult result = await _scriptController.ExecuteScript(scriptName, ScriptParameter.Text);

        MessageBox.Show($"Script Status: {result.Status}\nResult: {result.Result}");

    }

    private async void LoadCompaniesBtn_Click(object sender, EventArgs e) {
        IEnumerable<Company> companies = await _companyController.GetAllCompanies();
        CompanyList.Items.Clear();
        foreach (var company in companies) {
            CompanyList.Items.Add($"{company.CompanyId}-{company.CompanyName}");
        }
    }
    
    private async void CreateCompanyBtn_Click(object sender, EventArgs e) {

        string newName = CompanyNameTextBox.Text;

        Company? newCompany = null;

        try {
            newCompany = await _companyController.CreateCompany(
                name: newName,
                contactName: "Job Snow",
                contactEmail: "john@gmail.com",
                contactPhone: "911",
                addressLine1: "",
                addressLine2: "",
                addressLine3: "",
                city: "",
                state: "",
                postalCode: ""
            );
        } catch { }

        if (newCompany is null) {
            MessageBox.Show($"Failed to create new Company with name '{newName}'");
        } else MessageBox.Show($"New Company '{newName}' created");

    }

    private async void CreateProductBtn_Click(object sender, EventArgs e) {
        string newName = ProductNameTextBox.Text;

        Product? newProduct = null;

        try {
            newProduct = await _productController.CreateProduct(
                name: newName,
                description: "Product Description",
                attributes: new List<string>() { "A", "B" }
            ); 
        } catch (Exception ex) {
            MessageBox.Show(ex.ToString());
        }

        if (newProduct is null) {
            MessageBox.Show($"Failed to create new Product with name '{newName}'");
        } else MessageBox.Show($"New Product '{newName}' created");
    }

    private async void LoadProductsBtn_Click(object sender, EventArgs e) {
        IEnumerable<Product> products = await _productController.GetAllProducts();
        ProductList.Items.Clear();
        foreach (var product in products) {
            ProductList.Items.Add(product);
        }
    }

    private async void ProductList_SelectedIndexChanged(object sender, EventArgs e) {

        Product product = (Product) ProductList.SelectedItem;

        IEnumerable<string> attributes = await _productController.GetProductAttributes(product.ProductId);

        AttributeList.Items.Clear();
        foreach (var attribute in attributes) {
            AttributeList.Items.Add(attribute);
        }

    }

    private void GenerateReportBtn_Click(object sender, EventArgs e) {

        /*Report report = new() {
            ReportId = 0,
            Name = "Cut List",
            Template = @"C:\Users\Zachary Londono\Desktop\ReportTemplate.xlsx",
            OutputDirectory = @"C:\Users\Zachary Londono\Desktop\"
        };*/


        /*try {

            var script = CSharpScript.Create("");
            var compilation = script.Compile();
            var a = script.RunAsync().Result;

            var obj = await CSharpScript.EvaluateAsync(@"
                using System.Collections.Generic;

                public class TestClass {
                    public string Title { get; set; }
                    public List<Item> Products {get;set;}
                }
                
                public class Item {
                    public int LineNumber { get; set; }
                    public int Name { get; set; }
                }

                ShowDialog(""Hello From Script"");

                return new TestClass() {
                    Title = Var,
                    Products = new List<Item>() {
                            new Item () {
                                LineNumber = 1,
                                Name = 1
                            },
                            new Item () {
                                LineNumber = 2,
                                Name = 2
                            }
                    }
                };
            ", globals: new Globals() { Var = "Hello", ShowDialog = (s => MessageBox.Show(s)) });

            ReportEnvelope envelope = await _reportController?.GenerateReport(report, obj, "report.xlsx");
            if (envelope is not null && envelope.Output is not null)
               Process.Start(new ProcessStartInfo(envelope.Output) { UseShellExecute = true });
        } catch (Exception ex) {
            MessageBox.Show(ex.ToString());
        }*/
    }

    /*
        private async Task<ScriptResult> PriceProduct() {

            Product product = new() {
                ProductId = 0,
                Name = "Test Product",
                Description = "Product for price testing",
                Attributes = new List<string>(){
                    "Height",
                    "Width",
                    "Depth"
                },
                PricingScript = "ScriptA"
            };

            LineItem lineItem = new() {
                Line = 1,
                ProductId = product.ProductId,
                Price = 0M,
                Attributes = new ReadOnlyDictionary<string, object>(
                    new Dictionary<string, object>() {
                        { "Height", 5 },
                        { "Width", 5 },
                        { "Depth", 5 }
                    })
            };

            if (_scriptController is null) {
                MessageBox.Show("Controller is null");
                return new ScriptResult(ScriptStatus.Failure, "Cant excecute scripts");
            }

            return await _scriptController.ExecuteScript(product.PricingScript, lineItem);

        }
    */
}

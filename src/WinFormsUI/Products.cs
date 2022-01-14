using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Features.Products;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.WinFormsUI;

public partial class Products : Form {
    
    public Products() {
        InitializeComponent();
    }

    private async void CreateProductBtn_Click(object sender, EventArgs e) {

        string productName = nameInput.Text;
        string productDescription = descriptionInput.Text;
        IEnumerable<string> attributes = new List<string>() {
            attribute1Input.Text,
            attribute2Input.Text
        };

        if (Program.ServiceProvider is null) {
            MessageBox.Show("null ServiceProvider");
            return;
        }

        ProductController? controller = Program.ServiceProvider.GetService<ProductController>();

        if (controller is null) {
            MessageBox.Show("null controller");
            return;
        }

        try {
            Product product = await controller.CreateProduct(productName, productDescription, attributes);
            MessageBox.Show($"Product created, new id: {product.Id}");
        } catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }
    }

}

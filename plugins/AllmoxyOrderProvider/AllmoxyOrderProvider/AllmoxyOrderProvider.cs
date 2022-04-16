using PluginContracts.Interfaces;
using PluginContracts.Models;
using System.Xml;

namespace AllmoxyOrderProvider;

public class AllmoxyOrderProvider : INewOrderFromFileProvider {
    
    public string PluginName => "Allmoxy Orders";

    public int Version => 1;

    public OrderDto GetNewOrder(string filePath) {
        Console.WriteLine($"File path passed to allmoxy order provider '{filePath}'");

        XmlDocument doc = new();

        if (string.IsNullOrEmpty(filePath))
            throw new InvalidOperationException("No file path set");

        doc.Load(filePath);

        XmlNode? currentOrderNode = doc.FirstChild;

        if (currentOrderNode is null)
            throw new InvalidDataException("Could not get data from xml file");

        if (currentOrderNode.LocalName.Equals("xml")) {
            currentOrderNode = currentOrderNode.NextSibling;
        }

        if (currentOrderNode is not XmlElement xmlElement)
            throw new InvalidDataException("Could not get order data from xml node");

        string customerName = xmlElement["customer"]?.InnerText ?? "";

        OrderDto order = new();
        order.Name = xmlElement["name"]?.InnerText ?? "";
        order.Number = xmlElement.Attributes.GetNamedItem("id")?.Value ?? "---";
        order.Customer = ParseCustomer(xmlElement, customerName);
        order.SupplierId = 3;
        order.VendorId = 3;

        order.Products = new();

        var drawerboxes = xmlElement.SelectNodes($"/order[1]/DrawerBox");

        if (drawerboxes is null) return order;

        foreach (XmlNode drawerbox in drawerboxes) {


            XmlNode? dimensions = drawerbox["dimensions"];

            if (dimensions is null) continue;

                var product = new ProductDto {
                ProductId = 1,
                Options = new() {
                    { "Height", dimensions["height"]?.InnerText ?? "" },
                    { "Width", dimensions["width"]?.InnerText ?? "" },
                    { "Depth", dimensions["depth"]?.InnerText ?? "" },
                    { "Material", drawerbox["material"]?.InnerText ?? "" },
                    { "Bottom", drawerbox["bottom"]?.InnerText ?? ""},
                    { "Insert", drawerbox["insert"]?.InnerText ?? ""},
                    { "Notch", drawerbox["notch"]?.InnerText ?? ""},
                    { "Clips", drawerbox["clip"]?.InnerText ?? ""},
                    { "Logo", drawerbox["logo"]?.InnerText ?? ""},
                    { "Scoop", drawerbox["scoop"]?.InnerText ?? ""},
                    { "Note", drawerbox["note"]?.InnerText ?? ""},
                    { "Price", drawerbox["price"]?.InnerText ?? ""},
                    { "Comments", drawerbox["comments"]?.InnerText ?? "" }
                }
            };

            XmlNode? udimensions = drawerbox["udimensions"];

            if (udimensions is not null) {

                product.Options.Add("A", udimensions["a"]?.InnerText ?? "");
                product.Options.Add("B", udimensions["b"]?.InnerText ?? "");
                product.Options.Add("C", udimensions["c"]?.InnerText ?? "");

            }

            if (!TryGetAttribute(drawerbox, "group", out string? group)) group = "0";
            if (!TryGetAttribute(drawerbox, "lineNumber", out string? line)) line = "0";

            product.LineNumber = int.Parse($"{line}{group}");

            product.Qty = int.Parse(drawerbox["qty"]?.InnerText ?? "0");

            order.Products.Add(product);

        }

        return order;

    }

    private static bool TryGetAttribute(XmlNode? node, string attribute, out string? value) {

        if (node is null || node.Attributes is null) {
            value = null;
            return false;
        }
        
        var attr = node.Attributes[attribute];
        if (attr is null){
            value = null;
            return false;
        }

        value = attr.InnerText;
        return true;

    }

    private static CompanyDto ParseCustomer(XmlNode currentOrderNode, string customerName) {

        var customer = new CompanyDto();
        customer.Name = customerName;

        XmlNode? shipping = currentOrderNode.SelectSingleNode($"/order[{1}]/shipping");

        if (shipping is null) return customer;

        string shipAddress = shipping["address"]?.InnerText ?? "";
        var addressParts = shipAddress.Split(',');

        customer.Contact = addressParts[0].Trim();
        customer.Address1 = addressParts[1].Trim();
        customer.Address2 = "";
        if (addressParts.Length > 6)
            customer.Address2 = addressParts[2].Trim();
        customer.City = addressParts[addressParts.Length - 4].Trim();
        customer.State = addressParts[addressParts.Length - 3].Trim();
        customer.Zip = addressParts[addressParts.Length - 2].Trim();

        return customer;
    }

}

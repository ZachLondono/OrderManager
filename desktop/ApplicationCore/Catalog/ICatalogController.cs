using OrderManager.Domain.Catalog;
using Refit;

namespace OrderManager.ApplicationCore.Catalog;

public interface ICatalogController {

    public record CreatedResult(string Location, object Value);

    [Post("/AddToCatalog")]
    public Task<CreatedResult> AddToCatalog([Body(buffered:true)] AddToCatalogCommand command);

    public class AddToCatalogCommand {

        public string Name { get; set; } = string.Empty;

    }

    [Delete("/{id}")]
    public Task RemoveFromCatalog(int id);

    [Put("/SetProductName")]
    public Task SetProductName([Body(buffered:true)] SetProductNameCommand command);

    public class SetProductNameCommand {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
    }


    [Post("/AddAttribute")]
    public Task<CreatedResult> AddAttribute([Body(buffered:true)] AddAttributeCommand command);

    public class AddAttributeCommand {

        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Default { get; set; } = string.Empty;

    }

    [Delete("/RemoveAttribute")]
    public Task<CreatedResult> RemoveAttribute([Body(buffered:true)] RemoveAttributeCommand command);

    public class RemoveAttributeCommand {

        public int ProductId { get; set; }

        public string Attribute { get; set; } = string.Empty;

    }

    [Put("/UpdateAttribute")]
    public Task UpdateAttribute([Body(buffered:true)] UpdateAttributeCommand command);

    public class UpdateAttributeCommand {

        public int ProductId { get; set; }

        public string OldAttribute { get; set; } = string.Empty;

        public string NewAttribute { get; set; } = string.Empty;

        public string Default { get; set; } = string.Empty;

    }

    [Get("/")]
    public Task<IEnumerable<ProductSummary>> GetProducts();

    [Get("/{id}")]
    public Task<Product> GetProductDetails(int id);

}
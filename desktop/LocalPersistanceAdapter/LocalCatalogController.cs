using OrderManager.ApplicationCore.Catalog;
using OrderManager.Domain.Catalog;
using Refit;

namespace LocalPersistanceAdapter;

public class LocalCatalogController : ICatalogController {

    public Task<ICatalogController.CreatedResult> AddAttribute([Body(true)] ICatalogController.AddAttributeCommand command) {
        throw new NotImplementedException();
    }

    public Task<ICatalogController.CreatedResult> AddToCatalog([Body(true)] ICatalogController.AddToCatalogCommand command) {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductDetails(int id) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductSummary>> GetProducts() {
        throw new NotImplementedException();
    }

    public Task<ICatalogController.CreatedResult> RemoveAttribute([Body(true)] ICatalogController.RemoveAttributeCommand command) {
        throw new NotImplementedException();
    }

    public Task RemoveFromCatalog(int id) {
        throw new NotImplementedException();
    }

    public Task SetProductName([Body(true)] ICatalogController.SetProductNameCommand command) {
        throw new NotImplementedException();
    }

    public Task UpdateAttribute([Body(true)] ICatalogController.UpdateAttributeCommand command) {
        throw new NotImplementedException();
    }

}
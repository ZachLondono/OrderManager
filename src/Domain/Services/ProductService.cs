using Domain.Entities.ProductAggregate;
using Persistance.Repositories.Catalog;
using Persistance.Repositories.Parts;
using Persistance.Repositories.Parts.PartAttributes;

namespace Domain.Services;

public class ProductService : EntityService {

    private readonly ICatalogRepository _catalogRepository;
    private readonly IPartRepository _partRepository;
    private readonly IPartAttributeRepository _partAttributeRepository;
    //private readonly ProductAttributeRepository _productAttributeRepository;

    public ProductService(ICatalogRepository catalogRepository, IPartRepository partRepository, IPartAttributeRepository partAttributeRepository) {
        _catalogRepository = catalogRepository;
        _partRepository = partRepository;
        _partAttributeRepository = partAttributeRepository;
    }

    public IEnumerable<CatalogProduct> GetAllProducts() {
        throw new NotImplementedException();
    }

    public void UpdateProduct(CatalogProduct product) {
        throw new NotImplementedException();
    }

    public CatalogProduct GetProduct(int id) {
        throw new NotImplementedException();
    }

}

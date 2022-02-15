using Domain.Entities.ProductAggregate;
using Persistance.Repositories.Catalog;
using Persistance.Repositories.Catalog.ProductAttribute;
using Persistance.Repositories.Parts;
using Persistance.Repositories.Parts.PartAttributes;

namespace Domain.Services;

public class ProductService : EntityService {

    private readonly ICatalogRepository _catalogRepository;
    private readonly IPartRepository _partRepository;
    private readonly IPartAttributeRepository _partAttributeRepository;
    private readonly ProductAttributeRepository _productAttributeRepository;

    public ProductService(ICatalogRepository catalogRepository, IPartRepository partRepository, IPartAttributeRepository partAttributeRepository, ProductAttributeRepository productAttributeRepository) {
        _catalogRepository = catalogRepository;
        _partRepository = partRepository;
        _partAttributeRepository = partAttributeRepository;
        _productAttributeRepository = productAttributeRepository;
    }

    public IEnumerable<CatalogProduct> GetAllProducts() {
        var products = _catalogRepository.GetCatalog();

        List<CatalogProduct> catalog = new();
        foreach (var productDao in products)
            catalog.Add(GetProductFromDao(productDao));

        return catalog;
    }

    public void UpdateProduct(CatalogProduct product) {

        var prodAttributes = product.Attributes;
        foreach (var attribute in prodAttributes) {
            _productAttributeRepository.UpdateAttribute(new() {
                Id = attribute.Id,
                ProductId = attribute.ProductId,
                Name = attribute.Name,
            });
        }
        var storedAttributes = _productAttributeRepository.GetAttributesByProductId(product.Id);
        foreach (var storedAttribute in storedAttributes) {
            // If there is an attribute that is stored for this product, but it is not in the current CatalogProduct entity, remove it's maping
            if (!prodAttributes.Where(a => a.Id == storedAttribute.Id).Any())
                _productAttributeRepository.RemoveAttributeFromProduct(product.Id, storedAttribute.Id);
        }

        foreach (var part in product.Parts) {

            _partRepository.UpdatePart(new() {
                Id = part.Id,
                ProductId = part.Product.Id,
                Name = part.Name
            });

            foreach (var attribute in part.Attributes) {
                _partAttributeRepository.UpdateAttribute(new() {
                    Id = attribute.Id,
                    PartId = attribute.PartId,
                    Name = attribute.Name
                });
            }

        }

        _catalogRepository.UpdateProduct(new() {
            Id = product.Id,
            Name = product.Name
        });
    }

    public CatalogProduct GetProduct(int id) {
        var productDao = _catalogRepository.GetProductById(id);
        return GetProductFromDao(productDao);
    }

    private CatalogProduct GetProductFromDao(CatalogProductDAO productDao) {
        CatalogProduct product = new();

        var attributes = _productAttributeRepository.GetAttributesByProductId(productDao.Id);
        foreach (var attribute in attributes)
            product.AddAttribute(attribute.Name);

        var parts = _partRepository.GetPartsByProductId(productDao.Id);
        foreach (PartDAO partDao in parts) {
            var partAttributes = _partAttributeRepository.GetAttributesByPartId(partDao.Id);

            Part part = new(partDao.Name, product);
            foreach (var partAttribute in partAttributes)
                part.AddAttribute(partAttribute.Name);

            product.AddPart(part);
        }

        return product;
    }

}

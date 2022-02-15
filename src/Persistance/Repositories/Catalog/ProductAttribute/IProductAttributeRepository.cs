namespace Persistance.Repositories.Catalog.ProductAttribute;

public interface IProductAttributeRepository {

    public ProductAttributeDAO CreateAttribute(int productId, string Name);

    public IEnumerable<ProductAttributeDAO> GetAttributesByProductId(int productId);

    public void UpdateAttribute(ProductAttributeDAO productAttribute);

    public void RemoveAttributeFromProduct(int productId, int attributeId);

}
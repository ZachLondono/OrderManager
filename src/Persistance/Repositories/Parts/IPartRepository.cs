namespace Persistance.Repositories.Parts;

public interface IPartRepository {

    public PartDAO CreatePart(int productId, string name);

    public void UpdatePart(PartDAO part);

    public IEnumerable<PartDAO> GetPartsByProduct(int productId);

}
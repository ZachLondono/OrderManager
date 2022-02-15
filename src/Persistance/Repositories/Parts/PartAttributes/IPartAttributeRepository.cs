namespace Persistance.Repositories.Parts.PartAttributes;

public interface IPartAttributeRepository {

    public PartAttributeDAO CreateAttribute(int partId, string Name);

    public void UpdateAttribute(PartAttributeDAO attribute);

    public IEnumerable<PartAttributeDAO> GetAttributesByPartId(int partId);

}

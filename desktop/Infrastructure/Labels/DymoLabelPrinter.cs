using OrderManager.ApplicationCore.Labels;
using System.Xml;

namespace Infrastructure.Labels;

public class DymoLabelPrinter : ILabelPrinter {

    public Task<IEnumerable<string>> GetLabelFields(string templatePath) {
		List<string> fields = new();

		XmlDocument doc = new();
		doc.Load(templatePath);
		var labelObjectNodes = doc.SelectNodes("/DieCutLabel/ObjectInfo");
		if (labelObjectNodes is null)
			throw new ArgumentException($"The provided file is not a valid label template\n{templatePath}");

		foreach (XmlNode labelObjectInfo in labelObjectNodes) {
			XmlNodeList childObject = labelObjectInfo.ChildNodes;
			foreach (XmlNode labelObjectNode in childObject) {
				if (labelObjectNode.Name.Equals("TextObject") || labelObjectNode.Name.Equals("AddressObject")) {
					var node = labelObjectNode["Name"];
					if (node is not null) fields.Add(node.InnerText);
					break;
				}
			}
		}

		IEnumerable<string> fieldsEnum = fields;

		return Task.FromResult(fieldsEnum);
	}

    public Task PrintLabel(string templatePath, Dictionary<string, string> fields, int qty) {
        throw new NotImplementedException();
    }
}
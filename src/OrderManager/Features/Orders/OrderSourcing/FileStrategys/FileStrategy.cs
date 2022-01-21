using OrderManager.ApplicationCore.Domain;
using ClosedXML.Excel;
using System.Xml;
using System.Xml.Serialization;

namespace OrderManager.ApplicationCore.Features.Orders.OrderSourcing.FileStrategys;

public abstract class FileOrderStrategy : IOrderLoadStrategy {

    public Func<string> _filePickerStrat;
    public FileOrderStrategy(Func<string> filePickerStrat) {
        _filePickerStrat = filePickerStrat;
    }

    public Order LoadOrder() {
        string filePath = _filePickerStrat();
        return LoadOrderFromFile(filePath);
        throw new NotImplementedException();
    }

    public abstract Order LoadOrderFromFile(string filePath);
}

public abstract class XlFileOrderStrategy : FileOrderStrategy {
    protected XlFileOrderStrategy(Func<string> filePickerStrat) : base(filePickerStrat) { }
    public override Order LoadOrderFromFile(string filePath) {
        return LoadOrderFromXLFile(new(filePath));
    }
    public abstract Order LoadOrderFromXLFile(XLWorkbook workbook);
}

public abstract class XmlFileOrderStrategy<T> : FileOrderStrategy {
    protected XmlFileOrderStrategy(Func<string> filePickerStrat) : base(filePickerStrat) { }
    public override Order LoadOrderFromFile(string filePath) {

        // Create an xml serializer to convert xml file to an object of type T
        XmlSerializer xmlSerializer = new(typeof(T));

        // Open the file in an xml reader so it can be read by the serializer
        FileStream fs = new FileStream(filePath, FileMode.Open);
        XmlReader reader = XmlReader.Create(fs);

        // Serialize the xml to the type T
        T? data = (T?)xmlSerializer.Deserialize(reader);

        if (data is null) throw new InvalidDataException("Xml data does not match data model");

        return LoadOrderFromXmlFile(data);
    }
    public abstract Order LoadOrderFromXmlFile(T data);
}

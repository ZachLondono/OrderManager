using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Orders.OrderSourcing.FileStrategys.AllmoxyXML;

internal class AllmoxyXMLStrategy : XmlFileOrderStrategy<AllmoxyOrderModel> {
    public AllmoxyXMLStrategy(Func<string> filePickerStrat) : base(filePickerStrat) { }

    public override Order LoadOrderFromXmlFile(AllmoxyOrderModel data) {
        throw new NotImplementedException();
    }
}

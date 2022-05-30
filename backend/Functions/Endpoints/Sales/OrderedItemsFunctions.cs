using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sales.Implementation.Application.OrderedItems;
using Microsoft.AspNetCore.Http;

namespace Functions.Endpoints.Sales;

public class OrderedItemsFunctions {

    private readonly ISender _sender;

    public OrderedItemsFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(AddItemToOrder))]
    public async Task<IActionResult> AddItemToOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sales/orders/items")] AddItemToOrder.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    /*[FunctionName(nameof(UpdateItem))]
    public async Task<IActionResult> UpdateItem([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "sales/orders/items")] UpdateItem.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }*/

    [FunctionName(nameof(RemoveItem))]
    public async Task<IActionResult> RemoveItem([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "sales/orders/items/{id}")] HttpRequest req, int id) {
        await _sender.Send(new RemoveItemFromOrder.Command(id));
        return new NoContentResult();
    }

}
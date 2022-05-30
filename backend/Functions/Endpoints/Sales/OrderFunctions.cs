using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sales.Implementation.Application.Orders;
using Microsoft.AspNetCore.Http;

namespace Functions.Endpoints.Sales;

public class OrderFunctions {

    private readonly ISender _sender;

    public OrderFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(GetOrders))]
    public async Task<IActionResult> GetOrders([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"sales/orders")] HttpRequest req) {
        var orders = await _sender.Send(new GetOrders.Query());
        return new OkObjectResult(orders);
    }

    [FunctionName(nameof(GetOrderDetails))]
    public async Task<IActionResult> GetOrderDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Sales/Orders/{id}")] HttpRequest req, int id) {
        var order = await _sender.Send(new GetOrderDetails.Query(id));
        return new OkObjectResult(order);
    }

    [FunctionName(nameof(PlaceOrder))]
    public async Task<IActionResult> PlaceOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sales/orders/")] PlaceOrder.Command command) {
        int newId = await _sender.Send(command);
        return new CreatedResult($"/Sales/Orders/{newId}", newId);
    }

    /*[FunctionName(nameof(UpdateOrder))]
    public async Task<IActionResult> UpdateOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Sales/Orders/")] UpdateOrder.Command command) {
        await _sender.Send(command);
        return new OkResult();
    }*/

    [FunctionName(nameof(ConfirmOrder))]
    public async Task<IActionResult> ConfirmOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "sales/orders/{id}/confirm")] HttpRequest req, int id) {
        await _sender.Send(new ConfirmOrder.Command(id));
        return new NoContentResult();
    }

    [FunctionName(nameof(VoidOrder))]
    public async Task<IActionResult> VoidOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Sales/Orders/{id}/void")] VoidOrder.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

}

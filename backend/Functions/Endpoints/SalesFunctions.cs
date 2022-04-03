using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sales.Implementation.Application.Companies;
using Sales.Implementation.Application.OrderedItems;
using Sales.Implementation.Application.Orders;

namespace Functions.Endpoints;

public class SalesFunctions {

    private readonly ISender _sender;

    public SalesFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(AddContact))]
    public async Task<IActionResult> AddContact([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(AddContact)}")] AddContact.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveContact))]
    public async Task<IActionResult> RemoveContact([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(RemoveContact)}")] RemoveContact.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(AddRole))]
    public async Task<IActionResult> AddRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(AddRole)}")] AddRole.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveRole))]
    public async Task<IActionResult> RemoveRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(RemoveRole)}")] RemoveRole.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(CreateCompany))]
    public async Task<IActionResult> CreateCompany([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(CreateCompany)}")] CreateCompany.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetAddress))]
    public async Task<IActionResult> SetAddress([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(SetAddress)}")] SetAddress.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetName))]
    public async Task<IActionResult> SetName([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(SetName)}")] SetName.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(AddItemToOrder))]
    public async Task<IActionResult> AddItemToOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(AddItemToOrder)}")] AddItemToOrder.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetOptionValue))]
    public async Task<IActionResult> SetOptionValue([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(SetOptionValue)}")] SetOptionValue.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetQty))]
    public async Task<IActionResult> SetQty([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(SetQty)}")] SetQty.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(ConfirmOrder))]
    public async Task<IActionResult> ConfirmOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(ConfirmOrder)}")] ConfirmOrder.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(PlaceOrder))]
    public async Task<IActionResult> PlaceOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(PlaceOrder)}")] PlaceOrder.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(VoidOrder))]
    public async Task<IActionResult> VoidOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/{nameof(VoidOrder)}")]  VoidOrder.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

}

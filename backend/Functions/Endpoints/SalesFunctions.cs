using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sales.Implementation.Application.Companies;
using Sales.Implementation.Application.OrderedItems;
using Sales.Implementation.Application.Orders;
using Microsoft.AspNetCore.Http;

namespace Functions.Endpoints;

public class SalesFunctions {

    private readonly ISender _sender;

    public SalesFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(AddContact))]
    public async Task<IActionResult> AddContact([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(AddContact)}")] AddContact.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveContact))]
    public async Task<IActionResult> RemoveContact([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(RemoveContact)}")] RemoveContact.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(AddRole))]
    public async Task<IActionResult> AddRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(AddRole)}")] AddRole.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveRole))]
    public async Task<IActionResult> RemoveRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(RemoveRole)}")] RemoveRole.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(CreateCompany))]
    public async Task<IActionResult> CreateCompany([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Companies/{nameof(CreateCompany)}")] CreateCompany.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetAddress))]
    public async Task<IActionResult> SetAddress([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Companies/{nameof(SetAddress)}")] SetAddress.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetName))]
    public async Task<IActionResult> SetName([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Companies/{nameof(SetName)}")] SetName.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(AddItemToOrder))]
    public async Task<IActionResult> AddItemToOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Orders/{nameof(AddItemToOrder)}")] AddItemToOrder.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetOptionValue))]
    public async Task<IActionResult> SetOptionValue([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Orders/{nameof(SetOptionValue)}")] SetOptionValue.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(SetQty))]
    public async Task<IActionResult> SetQty([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Orders/{nameof(SetQty)}")] SetQty.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(ConfirmOrder))]
    public async Task<IActionResult> ConfirmOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Orders/{nameof(ConfirmOrder)}")] ConfirmOrder.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(PlaceOrder))]
    public async Task<IActionResult> PlaceOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Orders/{nameof(PlaceOrder)}")] PlaceOrder.Command command)  {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(VoidOrder))]
    public async Task<IActionResult> VoidOrder([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Orders/{nameof(VoidOrder)}")]  VoidOrder.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(GetOrders))]
    public async Task<IActionResult> GetOrders([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Sales/Orders/{nameof(GetOrders)}")] HttpRequest req) {
        var orders = await _sender.Send(new GetOrders.Query());
        return new OkObjectResult(orders);
    }

    [FunctionName(nameof(GetOrderDetails))]
    public async Task<IActionResult> GetOrderDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Sales/Orders/{nameof(GetOrderDetails)}")] HttpRequest req) {

        string id = req.Query["id"];

        int orderId = int.Parse(id);
        if (orderId <= 0) {
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var order = await _sender.Send(new GetOrderDetails.Query(orderId));
        return new OkObjectResult(order);
    }

    [FunctionName(nameof(GetCompanies))]
    public async Task<IActionResult> GetCompanies([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Sales/Companies/{nameof(GetCompanies)}")] HttpRequest req) {
        var companies = await _sender.Send(new GetCompanies.Query());
        return new OkObjectResult(companies);
    }

    [FunctionName(nameof(GetCompanyDetails))]
    public async Task<IActionResult> GetCompanyDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Sales/Companies/{nameof(GetCompanyDetails)}")] HttpRequest req) {

        string id = req.Query["id"];

        int companyId = int.Parse(id);
        if (companyId <= 0) {
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var company = await _sender.Send(new GetCompanyDetails.Query(companyId));
        return new OkObjectResult(company);
    }

    [FunctionName(nameof(GetOrderedItemsDetails))]
    public async Task<IActionResult> GetOrderedItemsDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Sales/Orders/{nameof(GetOrderedItemsDetails)}")] HttpRequest req) {
        
        string id = req.Query["id"];

        int orderId = int.Parse(id);
        if (orderId <= 0) {
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var items = await _sender.Send(new GetOrderedItemsDetails.Query(orderId));
        return new OkObjectResult(items);
    }

}

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sales.Implementation.Application.Companies;

namespace Functions.Endpoints.Sales;

public class ContactFunctions {

    private readonly ISender _sender;

    public ContactFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(AddContact))]
    public async Task<IActionResult> AddContact([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(AddContact)}")] AddContact.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveContact))]
    public async Task<IActionResult> RemoveContact([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(RemoveContact)}")] RemoveContact.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(AddRole))]
    public async Task<IActionResult> AddRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(AddRole)}")] AddRole.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveRole))]
    public async Task<IActionResult> RemoveRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Contacts/{nameof(RemoveRole)}")] RemoveRole.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

}
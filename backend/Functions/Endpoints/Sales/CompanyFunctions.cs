using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sales.Implementation.Application.Companies;
using Microsoft.AspNetCore.Http;

namespace Functions.Endpoints.Sales;

public class CompanyFunctions {

    private readonly ISender _sender;

    public CompanyFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(CreateCompany))]
    public async Task<IActionResult> CreateCompany([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Sales/Companies/")] CreateCompany.Command command)  {
        int newId = await _sender.Send(command);
        return new OkObjectResult(newId);
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
    
    [FunctionName(nameof(AddRole))]
    public async Task<IActionResult> AddRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Companies/{nameof(AddRole)}")] AddRole.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveRole))]
    public async Task<IActionResult> RemoveRole([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Companies/{nameof(RemoveRole)}")] RemoveRole.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveCompany))]
    public async Task<IActionResult> RemoveCompany([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Sales/Companies/{id}")] HttpRequest req, int id) {
        await _sender.Send(new RemoveCompany.Command(id));
        return new NoContentResult();
    }

    [FunctionName(nameof(GetCompanies))]
    public async Task<IActionResult> GetCompanies([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Sales/Companies/")] HttpRequest req) {
        var companies = await _sender.Send(new GetCompanies.Query());
        return new OkObjectResult(companies);
    }

    [FunctionName(nameof(GetCompanyDetails))]
    public async Task<IActionResult> GetCompanyDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Sales/Companies/{id}")] HttpRequest req, int id) {

        if (id <= 0) {
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var company = await _sender.Send(new GetCompanyDetails.Query(id));
        return new OkObjectResult(company);
    }


    [FunctionName(nameof(AddContact))]
    public async Task<IActionResult> AddContact([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = $"Sales/Companies/{nameof(AddContact)}")] AddContact.Command command) {
        int newId = await _sender.Send(command);
        return new OkObjectResult(newId);
    }

    [FunctionName(nameof(RemoveContact))]
    public async Task<IActionResult> RemoveContact([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = $"Sales/Companies/{nameof(RemoveContact)}")] RemoveContact.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(UpdateContact))]
    public async Task<IActionResult> UpdateContact([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Sales/Companies/{nameof(UpdateContact)}")] UpdateContact.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

}

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

}

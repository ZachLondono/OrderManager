using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MediatR;
using Manufacturing.Implementation.Application;
using System;

namespace Functions.Endpoints;

public class ManufacturingFunctions {

    private readonly ISender _sender;

    public ManufacturingFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(CompleteJob))]
    public async Task<IActionResult> CompleteJob([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Manufacturing/{nameof(CompleteJob)}")]
                                                  CompleteJob.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(ReleaseJob))]
    public async Task<IActionResult> ReleaseJob([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Manufacturing/{nameof(ReleaseJob)}")]
                                                  ReleaseJob.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(ShipJob))]
    public async Task<IActionResult> ShipJob([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Manufacturing/{nameof(ShipJob)}")]
                                                  ShipJob.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(GetJob))]
    public async Task<IActionResult> GetJob([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Manufacturing/{nameof(GetJob)}")] HttpRequest req) {

        string id = req.Query["id"];

        int productId;
        try {
            productId = int.Parse(id);
            if (productId <= 0) {
                throw new InvalidOperationException("Invalid id");
            }
        } catch {
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var result = await _sender.Send(new GetJobById.Query(productId));
        return result is not null ? new OkObjectResult(result) : new BadRequestResult();

    }


    [FunctionName(nameof(GetJobs))]
    public async Task<IActionResult> GetJobs([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Manufacturing/{nameof(GetJobs)}")] HttpRequest req) {
        var result = await _sender.Send(new GetJobs.Query());
        return result is not null ? new OkObjectResult(result) : new BadRequestResult();
    }

}

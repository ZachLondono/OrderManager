using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MediatR;
using Manufacturing.Implementation.Application;
using System;

namespace Functions.Endpoints.Manufacturing;

public class JobFunctions {

    private readonly ISender _sender;

    public JobFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(GetJobs))]
    public async Task<IActionResult> GetJobs([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"manufacturing/jobs")] HttpRequest req) {
        var result = await _sender.Send(new GetJobs.Query());
        return result is not null ? new OkObjectResult(result) : new NoContentResult();
    }

    [FunctionName(nameof(GetJob))]
    public async Task<IActionResult> GetJob([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "manufacturing/jobs/{id}")] HttpRequest req, int id) {

        if (id <= 0)
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });

        var result = await _sender.Send(new GetJobById.Query(id));
        return result is not null ? new OkObjectResult(result) : new NotFoundResult();

    }

    [FunctionName(nameof(CompleteJob))]
    public async Task<IActionResult> CompleteJob([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "manufacturing/jobs/{id}/complete")] HttpRequest req, int id) {
        await _sender.Send(new CompleteJob.Command(id));
        return new NoContentResult();
    }

    [FunctionName(nameof(ShipJob))]
    public async Task<IActionResult> ShipJob([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "manufacturing/jobs/{id}/ship")] HttpRequest req, int id) {
        await _sender.Send(new ShipJob.Command(id));
        return new NoContentResult();
    }

}

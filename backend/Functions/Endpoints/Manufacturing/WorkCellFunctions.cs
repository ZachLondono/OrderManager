using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MediatR;
using System;
using Manufacturing.Implementation.Application.WorkCell;

namespace Functions.Endpoints.Manufacturing;

public class WorkCellFunctions {

    private readonly ISender _sender;

    public WorkCellFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(GetWorkCells))]
    public async Task<IActionResult> GetWorkCells([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "manufacturing/cells/")] HttpRequest req) {
        /*var cells = await _sender.Send(new GetWorkCells.Query(id));
        return new OkObjectResult(cells);*/
        throw new NotImplementedException();
    }

    [FunctionName(nameof(GetWorkCell))]
    public async Task<IActionResult> GetWorkCell([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "manufacturing/cells/{id}")] HttpRequest req, int id) {
        /*var cell = await _sender.Send(new GetWorkCell.Query(id));
        return new OkObjectResult(cell);*/
        throw new NotImplementedException();
    }
    
    [FunctionName(nameof(CreateWorkCell))]
    public async Task<IActionResult> CreateWorkCell([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "manufacturing/cells/")] CreateWorkCell.Command command) {
        var response = await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(UpdateWorkCell))]
    public async Task<IActionResult> UpdateWorkCell([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "manufacturing/cells/")] UpdateWorkCell.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(ScheduleJobInWorkCell))]
    public async Task<IActionResult> ScheduleJobInWorkCell([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "manufacturing/cells/schedule")] ScheduleJob.Command command) {
        await _sender.Send(command);
        return new NoContentResult();
    }

    [FunctionName(nameof(RemoveWorkCell))]
    public async Task<IActionResult> RemoveWorkCell([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "manufacturing/cells/{id}")] HttpRequest req, int id) {
        await _sender.Send(new RemoveWorkCell.Command(id));
        return new NoContentResult();
    }

    [FunctionName(nameof(UnScheduleJob))]
    public async Task<IActionResult> UnScheduleJob([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "manufacturing/cells/{cell_id}/{job_id}")] HttpRequest req, int cell_id, int job_id) {
        await _sender.Send(new RemoveJob.Command(cell_id, job_id));
        return new NoContentResult();
    }

}
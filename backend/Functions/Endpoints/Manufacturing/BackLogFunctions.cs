using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MediatR;
using Manufacturing.Implementation.Application.BackLogs;

namespace Functions.Endpoints.Manufacturing;

public class BackLogFunctions {

    private readonly ISender _sender;

    public BackLogFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(GetBackLog))]
    public async Task<IActionResult> GetBackLog([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "manufacturing/baklog/")] HttpRequest req) {
        var backlog =  await _sender.Send(new GetBackLog.Query());
        return new OkObjectResult(backlog);
    }

}

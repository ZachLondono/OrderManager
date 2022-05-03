using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MediatR;
using Catalog.Implementation.Application;
using Microsoft.Extensions.Logging;

namespace Functions.Endpoints;

public class CatalogFunctions {

    private readonly ISender _sender;

    public CatalogFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(AddToCatalog))]
    public async Task<IActionResult> AddToCatalog([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = $"Catalog/{nameof(AddToCatalog)}")] 
                                                  AddToCatalog.Command command) {
        int newId = await _sender.Send(command);
        return new CreatedResult($"/Catalog/GetProductDetails/{newId}", new { Id = newId });
    }

    [FunctionName(nameof(AddAttribute))]
    public async Task<IActionResult> AddAttribute([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = $"Catalog/{nameof(AddAttribute)}")]
                                                    AddAttributeToProduct.Command command) {
        await _sender.Send(command);
        return new CreatedResult($"/Catalog/GetProductDetails/{command.ProductId}", new { Id = command.ProductId });
    }

    [FunctionName(nameof(RemoveAttribute))]
    public async Task<IActionResult> RemoveAttribute([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = $"Catalog/{nameof(RemoveAttribute)}")]
                                                        RemoveProductAttribute.Command command) {
        await _sender.Send(command);
        return new CreatedResult($"/Catalog/GetProductDetails/{command.ProductId}", new { Id = command.ProductId });
    }

    [FunctionName(nameof(UpdateAttribute))]
    public async Task<IActionResult> UpdateAttribute([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = $"Catalog/{nameof(UpdateAttribute)}")]
                                                        UpdateProductAttribute.Command command) {
        await _sender.Send(command);
        return new CreatedResult($"/Catalog/GetProductDetails/{command.ProductId}", new { Id = command.ProductId });
    }

    [FunctionName(nameof(GetProducts))]
    public async Task<IActionResult> GetProducts([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Catalog/{nameof(GetProducts)}")] HttpRequest req, ILogger log) {
        var result = await _sender.Send(new GetProducts.Query());
        return result is not null ? new OkObjectResult(result) : new BadRequestResult();
    }

    [FunctionName(nameof(GetProductDetails))]
    public async Task<IActionResult> GetProductDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"Catalog/{nameof(GetProductDetails)}/")] HttpRequest req) {

        string id = req.Query["id"];

        int productId;
        productId = int.Parse(id);
        if (productId <= 0) {
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var result = await _sender.Send(new GetProductDetails.Query(productId));
        return result is not null ? new OkObjectResult(result) : new BadRequestResult();
        
    }

}
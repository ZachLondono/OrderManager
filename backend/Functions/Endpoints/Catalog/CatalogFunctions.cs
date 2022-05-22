using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MediatR;
using Catalog.Implementation.Application;

namespace Functions.Endpoints.Catalog;

public class CatalogFunctions {

    private readonly ISender _sender;

    public CatalogFunctions(ISender sender) {
        _sender = sender;
    }

    [FunctionName(nameof(AddToCatalog))]
    public async Task<IActionResult> AddToCatalog([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = $"catalog/products")] AddToCatalog.Command command) {
        var product = await _sender.Send(command);
        return new CreatedResult($"/catalog/products/{product.Id}", product);
    }

    [FunctionName(nameof(UpdateProduct))]
    public async Task<IActionResult> UpdateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "catalog/products/")] UpdateProduct.Command command) {
        var product = await _sender.Send(command);
        return new OkObjectResult(product);
    }

    [FunctionName(nameof(RemoveFromCatalog))]
    public async Task<IActionResult> RemoveFromCatalog([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "catalog/products/{id}")] HttpRequest req, int id) {
        await _sender.Send(new RemoveFromCatalog.Command(id));
        return new NoContentResult();
    }

    [FunctionName(nameof(GetProducts))]
    public async Task<IActionResult> GetProducts([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "catalog/products")] HttpRequest req) {
        var result = await _sender.Send(new GetProducts.Query());
        return result is not null ? new OkObjectResult(result) : new BadRequestResult();
    }

    [FunctionName(nameof(GetProductDetails))]
    public async Task<IActionResult> GetProductDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "catalog/products/{id}")] HttpRequest req, int id) {
        
        if (id <= 0) {
            return new BadRequestObjectResult(new ProblemDetails {
                Title = "Invalid Id",
                Detail = "Id is not a valid integer",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var result = await _sender.Send(new GetProductDetails.Query(id));
        return result is not null ? new OkObjectResult(result) : new NotFoundResult();
        
    }

}
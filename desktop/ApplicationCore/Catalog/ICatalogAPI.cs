﻿using OrderManager.Domain.Catalog;
using Refit;

namespace OrderManager.ApplicationCore.Catalog;

public interface ICatalogAPI {

    public record CreatedResult(string Location, object Value);

    [Post("Catalog/AddToCatalog")]
    public Task<CreatedResult> AddToCatalog([Body(buffered:true)] AddToCatalogCommand command);

    public class AddToCatalogCommand {

        public string Name { get; set; } = string.Empty;

    }

    [Post("Catalog/AddAttribute")]
    public Task<CreatedResult> AddAttribute([Body(buffered:true)] AddAttributeCommand command);

    public class AddAttributeCommand {

        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Default { get; set; } = string.Empty;

    }

    [Delete("Catalog/RemoveAttribute")]
    public Task<CreatedResult> RemoveAttribute([Body(buffered:true)] RemoveAttributeCommand command);

    public class RemoveAttributeCommand {

        public int ProductId { get; set; }

        public string Attribute { get; set; } = string.Empty;

    }

    [Delete("Catalog/UpdateAttribute")]
    public Task<CreatedResult> UpdateAttribute([Body(buffered:true)] UpdateAttributeCommand command);

    public class UpdateAttributeCommand {

        public int ProductId { get; set; }

        public string OldAttribute { get; set; } = string.Empty;

        public string NewAttribute { get; set; } = string.Empty;

    }

    [Get("/Catalog/")]
    public Task<IEnumerable<ProductSummary>> GetProducts();

    [Get("/Catalog/{id}")]
    public Task<Product> GetProductDetails(int id);

}
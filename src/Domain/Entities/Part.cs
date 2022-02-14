using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Part {

    public int Id { get; set; }

    public CatalogProduct Product { get; set; }

    public string Name { get; set; }

    public Part(string name, CatalogProduct product) {
        Name = name;
        Product = product;
    }

}

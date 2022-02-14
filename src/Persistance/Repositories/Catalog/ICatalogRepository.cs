using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories.Catalog;

public interface ICatalogRepository {
    public IEnumerable<CatalogProductDAO> GetCatalog();
}
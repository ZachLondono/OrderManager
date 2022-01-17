using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.ApplicationCore.Infrastructure;

public abstract class BaseController {

    private readonly ISender _sender;
    protected ISender Sender => _sender ?? throw new ArgumentNullException(nameof(Sender));

    public BaseController(ISender sender) {
        _sender = sender;
    }

}

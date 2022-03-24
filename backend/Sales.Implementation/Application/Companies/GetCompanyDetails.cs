using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

internal class GetCompanyDetails {

    public record Query() : IRequest<CompanyDetails>;

    public class Handler : IRequestHandler<Query, CompanyDetails> {
        public Task<CompanyDetails> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
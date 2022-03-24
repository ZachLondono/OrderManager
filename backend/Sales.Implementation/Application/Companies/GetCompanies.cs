using MediatR;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

internal class GetCompanies {

    public record Query() : IRequest<CompanySummary[]>;

    public class Handler : IRequestHandler<Query, CompanySummary[]> {
        public Task<CompanySummary[]> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
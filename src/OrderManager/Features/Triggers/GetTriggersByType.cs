using MediatR;
using Dapper;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Triggers;

public class GetTriggersByType {

    public record Query(TriggerType Type) : IRequest<IEnumerable<Trigger>>;

    internal class Handler : IRequestHandler<Query, IEnumerable<Trigger>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<Trigger>> Handle(Query request, CancellationToken cancellationToken) {

            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
  
            connection.Close();

            throw new NotImplementedException();
        }

    }

}
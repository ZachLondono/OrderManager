﻿using MediatR;
using Microsoft.Data.Sqlite;
using Persistance;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace OrderManager.Features.OrderDetails;

public class CreateRemake {

    public record Command(int OrderId, List<Remake> Remakes) : IRequest<int>;

    public record Remake(int ItemId, int RemakeQty);

    public class Handler : IRequestHandler<Command, int> {

        private readonly ConnectionStringManager _connectionStringManager;
        
        public Handler(ConnectionStringManager connectionStringManager) {
            _connectionStringManager = connectionStringManager;
        }

        public Task<int> Handle(Command request, CancellationToken cancellationToken) {

            using var connection = new SqliteConnection(_connectionStringManager.GetConnectionString);

            connection.Open();

            using var transaction = connection.BeginTransaction();

            string suffix = GetOrderSuffix(request.OrderId, "-Remake", connection);

            string copyOrderDetails =
                $@"INSERT INTO [Orders]
                SELECT null as [Id], [Number] || '{suffix}' as [Number], [Name], [IsPriority], [LastModified], [CustomerId], [VendorId], [SupplierId]
                FROM [Orders]
                WHERE [Id] = @OrderId
                RETURNING [Id];";

            int newId = connection.QuerySingle<int>(copyOrderDetails, request);

            const string copyOrderItems =
                @"INSERT INTO [OrderItems]
                SELECT null as [Id], @RemakeQty as Qty, [ProductId], @OrderId as [OrderId], [ProductName]
                FROM [OrderItems]
                WHERE [Id] = @ItemId
                RETURNING Id;";

            const string copyItemOptions =
                @"INSERT INTO [OrderItemOptions]
                SELECT null as [Id], @NewId as [ItemId], [Key], [Value]
                FROM [OrderItemOptions]
                WHERE [ItemId] = @OriginalId;";

            foreach (var remake in request.Remakes) {
                int newItem = connection.QuerySingle<int>(copyOrderItems, new { remake.ItemId, remake.RemakeQty, OrderId = newId });
                connection.Execute(copyItemOptions, new { NewId = newItem, OriginalId = remake.ItemId });
            }

            transaction.Commit();

            return Task.FromResult(newId);

        }

        /// <summary>
        /// If an order with the base suffix already exists, a number will be appended to the new remake
        /// </summary>
        private string GetOrderSuffix(int orderId, string baseSuffix, SqliteConnection connection) {

            int numSuffix = 0;

            string number = connection.QuerySingle<string>(
                    "SELECT [Number] FROM [Orders] WHERE [Id] = @Id;",
                    new { Id = orderId }
                );

            while (true) {

                string query = @"SELECT COUNT([Number]) FROM [Orders] WHERE [Number] = @Number";

                string suffix = numSuffix == 0 ? baseSuffix : $"{baseSuffix}{numSuffix}";
                int count = connection.QuerySingle<int>(query, new {
                    Number = $"{number}{suffix}"
                });

                if (count == 0)
                    return suffix;

                numSuffix++;

            }

        }
    }

}
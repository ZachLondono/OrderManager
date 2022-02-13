using MediatR;
using OrderManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderList;

public class GetOrders {

    public record Query() : IRequest<Either<OrderList, Error>>;

    public record OrderList(IEnumerable<OrderModel> Orders);

    public class Handler : IRequestHandler<Query, Either<OrderList, Error>> {
        
        public async Task<Either<OrderList, Error>> Handle(Query request, CancellationToken cancellationToken) {

            Company customer = new() {
                Id = 1,
                Name = "Joe's Cabinet Shop"
            };

            Company vendor = new() {
                Id = 1,
                Name = "On Track"
            };

            List<OrderModel> orders = new() {
                new() {
                    Id = 0,
                    Number = "OT000",
                    Name = "ABC Kitchen",
                    DateModified = DateTime.Now,
                    IsPriority = false,
                    Customer = customer,
                    Vendor = vendor
                },
                new() {
                    Id = 1,
                    Number = "OT111",
                    Name = "ABC Kitchen",
                    DateModified = DateTime.Now.AddHours(-1.35),
                    IsPriority = false,
                    Customer = customer,
                    Vendor = vendor
                },
                new() {
                    Id = 2,
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = true,
                    DateModified = DateTime.Now.AddHours(-3.65),
                    Customer = customer,
                    Vendor = vendor
                },
                new() {
                    Id = 3,
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    DateModified = DateTime.Now.AddHours(-3.98),
                    Customer = customer,
                    Vendor = vendor
                },
                new() {
                    Id = 4,
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    DateModified = DateTime.Now.AddDays(-3.98),
                    Customer = customer,
                    Vendor = vendor
                },
                new() {
                    Id = 5,
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = true,
                    DateModified = DateTime.Now.AddDays(-1.98),
                    Customer = customer,
                    Vendor = vendor
                },
                new() {
                    Id = 6,
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    DateModified = DateTime.Now.AddDays(-10.98),
                    Customer = customer,
                    Vendor = vendor
                },
                new() {
                    Id = 6,
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,

                    DateModified = DateTime.Now.AddDays(-12.98),
                    Customer = customer,
                    Vendor = vendor
                },

                new() {
                    Id = 6,
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,

                    DateModified = DateTime.Now.AddDays(-9.98),
                    Customer = customer,
                    Vendor = vendor
                }
            };

            await Task.Delay(1);

            return new(new OrderList(orders));

            //return new(new Error("No Data", "Could not connect to database with connection string 'Data Source=C:/'"));

        }

    }

}

using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Services;
using MediatR;
using OrderManager.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.OrderList;

public class GetOrders {

    public record Query() : IRequest<Either<OrderList, Error>>;

    public record OrderList(IEnumerable<Order> Orders);

    public class Handler : IRequestHandler<Query, Either<OrderList, Error>> {

        private readonly OrderService _orderService;

        public Handler(OrderService orderService) {
            _orderService = orderService;
        }

        public async Task<Either<OrderList, Error>> Handle(Query request, CancellationToken cancellationToken) {

            Company customer = new() {
                Id = 1,
                Name = "Joe's Cabinet Shop"
            };

            Company vendor = new() {
                Id = 1,
                Name = "On Track"
            };

            List<Order> orders = new() {
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT000",
                    Name = "ABC Kitchen",
                    LastModified = DateTime.Now,
                    IsPriority = false,
                    /*Customer = customer,
                    Vendor = vendor*/
                },
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT111",
                    Name = "ABC Kitchen",
                    LastModified = DateTime.Now.AddHours(-1.35),
                    IsPriority = false,
                    /*Customer = customer,
                    Vendor = vendor*/
                },
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = true,
                    LastModified = DateTime.Now.AddHours(-3.65),
                    /*Customer = customer,
                    Vendor = vendor*/
                },
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    LastModified = DateTime.Now.AddHours(-3.98),
                    /*Customer = customer,
                    Vendor = vendor*/
                },
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    LastModified = DateTime.Now.AddDays(-3.98),
                    /*Customer = customer,
                    Vendor = vendor*/
                },
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = true,
                    LastModified = DateTime.Now.AddDays(-1.98),
                    /*Customer = customer,
                    Vendor = vendor*/
                },
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    LastModified = DateTime.Now.AddDays(-10.98),
                    /*Customer = customer,
                    Vendor = vendor*/
                },
                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    LastModified = DateTime.Now.AddDays(-12.98),
                    /*Customer = customer,
                    Vendor = vendor*/
                },

                new() {
                    Id = Guid.NewGuid(),
                    Number = "OT222",
                    Name = "ABC Kitchen",
                    IsPriority = false,
                    LastModified = DateTime.Now.AddDays(-9.98),
                    /*Customer = customer,
                    Vendor = vendor*/
                }
            };

            try {
                var list = _orderService.GetAllOrders().ToList();
                list.AddRange(orders);

                return new(new OrderList(list));
            } catch (Exception e) {
                Debug.WriteLine(e);
            }

            await Task.Delay(1);

            return new(new Error("No Data", "Could not connect to database with connection string 'Data Source=C:/'"));

        }

    }

}

﻿using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Infrastructure.Implementations;

public class AllmoxyOrderService : IOrderService {
    public IEnumerable<Order> GetNewOrders() => throw new NotImplementedException();
    public Order GetOrder(string refNum) => throw new NotImplementedException();
}
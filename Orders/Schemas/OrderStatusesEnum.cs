﻿using GraphQL.Types;

namespace Orders.Schemas
{
    public class OrderStatusesEnum : EnumerationGraphType
    {
        public OrderStatusesEnum()
        {
            Name = "OrderStatuses";
            AddValue("CREATED", "Order was created", 2);
            AddValue("PROCESSING", "Order is being processed", 4);
            AddValue("COMPLETED", "Order is completed", 8);
            AddValue("CLOSED", "Order is closed", 16);
        }
    }
}

using GraphQL.Types;
using Orders.Services;

namespace Orders.Schemas
{
    public class OrdersQuery : ObjectGraphType
    {
        public OrdersQuery(IOrderService orders)
        {
            Name = "Query";
            Field<ListGraphType<OrderType>>("orders",
                resolve: context => orders.GetOrdersAsync());
        }
    }
}

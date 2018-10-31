using GraphQL.Types;
using Orders.Services;

namespace Orders.Schemas
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(ICustomerService customers)
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Description);
            Field<CustomerType>("customer", resolve: context =>
                customers.GetCustomerById(context.Source.CustomerId));
            Field(o => o.Created);
            Field<OrderStatusesEnum>("status", resolve:
                context => context.Source.Status);
        }
    }
}

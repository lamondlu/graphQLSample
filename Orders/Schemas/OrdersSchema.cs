using GraphQL;

namespace Orders.Schemas
{
    public class OrdersSchema : GraphQL.Types.Schema
    {
        public OrdersSchema(OrdersQuery query, IDependencyResolver resolver, OrdersMutation ordersMutation)
        {
            Query = query;
            DependencyResolver = resolver;
            Mutation = ordersMutation;
        }
    }
}

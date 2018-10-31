using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private IList<Order> _orders;

        public OrderService()
        {
            _orders = new List<Order>();
            _orders.Add(new Order("1000", "Order 1", DateTime.Now, 1, "6870CFF7-0527-42CD-8E14-42F03198BBAA"));

            _orders.Add(new Order("2000", "Order 2", DateTime.Now.AddHours(1), 2, "14522074-755B-45DD-8ACF-E1B26133AD5E"));

            _orders.Add(new Order("3000", "Order 3", DateTime.Now.AddHours(2), 3, "20741EF4-40A7-4A07-9A4B-7B2A47866495"));

            _orders.Add(new Order("4000", "Order 4", DateTime.Now.AddHours(3), 4, "2BEEBBE4-9F8A-47E7-8F6C-D38BF227AE96"));
        }

        public Task<Order> GetOrderByIdAsync(string id)
        {
            return Task.FromResult(_orders.Single(p => p.Id == id));
        }

        public Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }

        private Order GetById(string id)
        {
            var order = _orders.SingleOrDefault(p => p.Id == id);

            if (order == null)
            {
                throw new ArgumentException($"Order ID '{id}' is invalid.");
            }

            return order;
        }

        public Task<Order> CreateAsync(Order order)
        {
            _orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> StartAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Start();

            return Task.FromResult(order);
        }
    }

    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(string id);

        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order> CreateAsync(Order order);

        Task<Order> StartAsync(string orderId);
    }
}

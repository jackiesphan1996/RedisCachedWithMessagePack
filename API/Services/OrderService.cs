using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using MessagePack;
using StackExchange.Redis;
using Order = API.Models.Order;

namespace API.Services
{
    public interface IOrderService
    {
        Task<PaginationSet<Order>> GetAllOrderAsync(int pageIndex, int pageSize, string keyword);
    }
    public class OrderService : IOrderService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public OrderService(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<PaginationSet<Order>> GetAllOrderAsync(int pageIndex, int pageSize, string keyword)
        {
            var totalData = await GetAllAsync();
            var orders = totalData as Models.Order[] ?? totalData.ToArray();
            if (!string.IsNullOrEmpty(keyword))
            {
                orders = orders.Where(x => x.OrderCode.Contains(keyword)
                                       || (!string.IsNullOrEmpty(x.CancelBy) && x.CancelBy.Contains(keyword))
                                       || (!string.IsNullOrEmpty(x.StaffName) && x.StaffName.Contains(keyword))
                                       || (!string.IsNullOrEmpty(x.CustomerName) && x.CustomerName.Contains(keyword))
                ).ToArray();
            }
            var queryData = orders.OrderByDescending(x => x.CreateDateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var res = new PaginationSet<Models.Order>
            {
                Items = queryData,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = orders.Count()
            };
            return res;
        }

        private async Task<IEnumerable<Models.Order>> GetAllAsync()
        {
            try
            {
                var data = await _database.StringGetAsync("ALL_ORDERS");
                if (data.IsNullOrEmpty)
                {
                    return null;
                }

                return MessagePackSerializer.Deserialize<IEnumerable<Order>>(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

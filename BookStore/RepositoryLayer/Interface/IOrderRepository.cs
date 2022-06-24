using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRepository
    {
        Task<OrderModel> AddToOrder(OrderModel order);
        Task<bool> DeleteOrder(OrderModel order);
        IEnumerable<OrderModel> GetAllOrder();
    }
}

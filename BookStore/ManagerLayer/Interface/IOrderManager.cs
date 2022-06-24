using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IOrderManager
    {
        Task<OrderModel> AddToOrder(OrderModel order);
        Task<bool> DeleteOrder(OrderModel order);
        IEnumerable<OrderModel> GetAllOrder();
    }
}

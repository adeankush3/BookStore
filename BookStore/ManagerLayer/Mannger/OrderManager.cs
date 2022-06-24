using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Mannger
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository repo;
        public OrderManager(IOrderRepository repo)
        {
            this.repo = repo;
        }

        public async Task<OrderModel> AddToOrder(OrderModel order)
        {
            try
            {
                return await this.repo.AddToOrder(order);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        

        public async Task<bool> DeleteOrder(OrderModel order)
        {
            try
            {
                return await this.repo.DeleteOrder(order);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetAllOrder()
        {
            try
            {
                return this.repo.GetAllOrder();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

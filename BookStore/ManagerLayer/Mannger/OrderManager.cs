using ManagerLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Mannger
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderManager repo;
        public OrderManager(IOrderManager repo)
        {
            this.repo = repo;
        }
    }
}

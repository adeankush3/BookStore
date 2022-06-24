using ManagerLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager manager;


        public OrderController(IOrderManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("addOrder")]

        public async Task<IActionResult> AddToOrder([FromBody] OrderModel order)
        {
            try
            {

                var resp = await this.manager.AddToOrder(order);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = " Order Successfully!!! ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = " Book Not Availabale" });
                }
            }
            catch (Exception e)
            {
                {

                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }
        }
        [HttpDelete]
        [Route("deleteOrder")]
        public async Task<IActionResult> DeleteOrder(OrderModel order)
        {
            try
            {

                bool resp = await this.manager.DeleteOrder(order);
                if (resp != false)
                {

                    return this.Ok(new ResponseModel<OrderModel> { Status = true, Message = " Order Cancel ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Not Found" });
                }
            }
            catch (Exception e)
            {
                {
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }

        }
        [HttpGet]
        [Route("getallOrder")]
        public IEnumerable<OrderModel> GetAllOrder()
        {
            try
            {
                var resp = this.manager.GetAllOrder();
                return resp;
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}

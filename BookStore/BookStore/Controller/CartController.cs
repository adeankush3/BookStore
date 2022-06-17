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
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;


        public CartController(ICartManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("addtocart")]

        public async Task<IActionResult> AddToCart([FromBody] CartModel cart)
        {
            try
            {

                var resp = await this.manager.AddToCart(cart);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = " Add to Cart Successfully!!!!!!", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Record Not Found" });
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
        [Route("removeFromCart")]
        public async Task<IActionResult> RemoveCart(CartModel cart)
        {
            try
            {

                bool resp = await this.manager.RemoveCart(cart);
                if (resp != false)
                {

                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = " Remove the cart!!!!!!", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Not Found any Data!!!!!!" });
                }
            }
            catch (Exception e)
            {
                {
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }

        }
        [HttpPut]
        [Route("updateCart")]
        public async Task<IActionResult> UpdateCartQuantity([FromBody] CartModel cart)
        {
            try
            {

                var resp = await this.manager.UpdateCartQuantity(cart);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<CartModel> { Status = true, Message = " Data Is UpDate ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Record not Found" });
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
        [Route("getallCart")]
        public IEnumerable<CartModel> GetAllCart()
        {
            try
            {
                var resp = this.manager.GetAllCart();
                return resp;
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}

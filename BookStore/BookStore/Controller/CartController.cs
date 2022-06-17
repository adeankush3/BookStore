using ManagerLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
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
    }
}

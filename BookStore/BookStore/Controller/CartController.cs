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
    }
}

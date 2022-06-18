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
    public class WishListController : ControllerBase
    {
        private readonly IWishListManager manager;


        public WishListController(IWishListManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("addtoWishList")]

        public async Task<IActionResult> AddToWishList([FromBody] WishListModel wishList)
        {
            try
            {

                var resp = await this.manager.AddToWishList(wishList);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<WishListModel> { Status = true, Message = " Add to WishList Successfully!!!!!!", Data = resp });
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
        [HttpDelete]
        [Route("removeFromWishList")]
        public async Task<IActionResult> RemoveWishList(WishListModel wishList)
        {
            try
            {

                bool resp = await this.manager.RemoveWishList(wishList);
                if (resp != false)
                {

                    return this.Ok(new ResponseModel<WishListModel> { Status = true, Message = " Remove the Wish List!!!!!!", Data = resp });
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
        [HttpGet]
        [Route("getallWishList")]
        public IEnumerable<WishListModel> GetAllWishList()
        {
            try
            {
                var resp = this.manager.GetAllWishList();
                return resp;
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}

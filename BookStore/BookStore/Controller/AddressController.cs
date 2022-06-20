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
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;


        public AddressController(IAddressManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("addtoAddress")]

        public async Task<IActionResult> AddToAddress([FromBody] AddressModel address)
        {
            try
            {

                var resp = await this.manager.AddToAddress(address);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = " Add to Address Successfully!!!!!!", Data = resp });
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
        [Route("removeAddress")]
        public async Task<IActionResult> RemoveAddress(AddressModel wishList)
        {
            try
            {

                bool resp = await this.manager.RemoveAddress(wishList);
                if (resp != false)
                {

                    return this.Ok(new ResponseModel<AddressModel> { Status = true, Message = " Remove the Address!!!!!!", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Record Not Found any Data!!!!!!" });
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
        [Route("getallAddress")]
        public IEnumerable<AddressModel> GetAllAddress()
        {
            try
            {
                var resp = this.manager.GetAllAddress();
                return resp;
            }

            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut]
        [Route("updateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressModel address)
        {
            try
            {

                var resp = await this.manager.UpdateAddress(address);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<BookModel> { Status = true, Message = " Address is Update Succeessfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Address Record not Update" });
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

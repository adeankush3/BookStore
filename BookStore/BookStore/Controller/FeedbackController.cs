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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager manager;


        public FeedbackController(IFeedbackManager manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("feedback")]

        public async Task<IActionResult> AddToFeedback([FromBody] FeedbackModel feedback)
        {
            try
            {

                var resp = await this.manager.AddToFeedback(feedback);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<FeedbackModel> { Status = true, Message = "Feedback done!!!!!!", Data = resp });
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
        [HttpGet]
        [Route("getallFeedback")]
        public IEnumerable<FeedbackModel> GetAllFeedback()
        {
            try
            {
                var resp = this.manager.GetAllFeedback();
                return resp;
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}

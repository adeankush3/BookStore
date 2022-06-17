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
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;
        

        public BookController(IBookManager manager) 
        {
            this.manager = manager;
            
        }
        [HttpPost]
        [Route("addbookinfo")]

        public async Task<IActionResult> AddBook([FromBody] BookModel book)
        {
            try
            {
                
                var resp = await this.manager.AddBook(book);
                if (resp != null)
                {
                   
                    return this.Ok(new ResponseModel<BookModel> { Status = true, Message = " Book Record save ", Data = resp });
                }
                else
                {
                    
                    return this.BadRequest(new { Status = false, Message = "Book Record not Save" });
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
        [Route("upadatebook")]
        public async Task<IActionResult> UpdateBook([FromBody] BookModel book)
        {
            try
            {

                var resp = await this.manager.UpdateBook(book);
                if (resp != null)
                {

                    return this.Ok(new ResponseModel<BookModel> { Status = true, Message = " Book Fecord Update Succeessfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Book Record not Update" });
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
        [Route("deletebook")]
        public async Task<IActionResult> DeleteBook(BookModel book)
        {
            try
            {

                bool resp = await this.manager.DeleteBook(book);
                if (resp != false)
                {

                    return this.Ok(new ResponseModel<BookModel> { Status = true, Message = " Book Delete Successfully ", Data = resp });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Book Not Found" });
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
        [Route("getallbook")]
        public IEnumerable<BookModel> GetAllBooks()
        {
            try
            {
                var resp =  this.manager.GetAllBooks();
                return resp;
            }
            
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Mannger
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository repo;
        public BookManager(IBookRepository repo)
        {
            this.repo = repo;
        }

        public async Task<BookModel> AddBook(BookModel book)
        {
            try
            {
                return await this.repo.AddBook(book);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteBook(BookModel book)
        {
            try
            {
                return await this.repo.DeleteBook(book);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<BookModel> GetAllBooks()
        {
            try
            {
                return  this.repo.GetAllBooks();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            try
            {
                return await this.repo.UpdateBook(book);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

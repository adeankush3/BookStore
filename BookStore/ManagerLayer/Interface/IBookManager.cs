using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IBookManager
    {
        Task<BookModel> AddBook(BookModel book);
        Task<BookModel> UpdateBook(BookModel book);
        Task<bool> DeleteBook(BookModel book);
        IEnumerable<BookModel> GetAllBooks();
    }
}

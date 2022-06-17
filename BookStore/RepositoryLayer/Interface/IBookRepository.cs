using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBookRepository
    {
        Task<BookModel> AddBook(BookModel book);
        Task<BookModel> UpdateBook(BookModel book);
        Task<bool> DeleteBook(BookModel book);
        IEnumerable<BookModel> GetAllBooks();

    }
}

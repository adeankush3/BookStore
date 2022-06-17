using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<BookModel> Book;
        private readonly IConfiguration configuration;

        public BookRepository(IDBSetting db, IConfiguration configuration)
        {

            this.configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);
            Book = database.GetCollection<BookModel>("Book");
        }

        public async Task<BookModel> AddBook(BookModel book)
        {
            try
            {
                var ifExists = await this.Book.Find(x => x.BookId == book.BookId).SingleOrDefaultAsync();
                if (ifExists == null)
                {
                    await this.Book.InsertOneAsync(book);
                    return book;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteBook(BookModel book)
        {
            try
            {
                var ifExists = await this.Book.FindOneAndDeleteAsync(x => x.BookId == book.BookId);
                return true;

            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<BookModel> GetAllBooks()
        {
            return Book.Find(FilterDefinition<BookModel>.Empty).ToList();
        }

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            try
            {
                var ifExists = await this.Book.Find(x => x.BookId == book.BookId).SingleOrDefaultAsync();
                if (ifExists != null)
                {
                    await this.Book.UpdateOneAsync(x => x.BookId == book.BookId,
                        Builders<BookModel>.Update.Set(x => x.BookName, book.BookName)
                        .Set(x => x.Description, book.Description)
                        .Set(x => x.AuthorName, book.AuthorName)
                        .Set(x => x.Rating, book.Rating)
                        .Set(x => x.totalRating, book.totalRating)
                        .Set(x => x.DiscountPrice, book.DiscountPrice));
                    return ifExists;
                       
                }
                else
                {
                    await this.Book.InsertOneAsync(book);
                    return book;
                }
            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

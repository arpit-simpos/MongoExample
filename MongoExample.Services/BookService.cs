using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoExample.Data;
using MongoExample.Data.Config;

namespace MongoExample.Services
{
    public interface IBookService
    {
        Task<List<Book>> Get();
        Task<Book> Get(string id);
        Task<Book> Create(Book book);
        Task<Book> Update(string id, Book bookIn);
        Task Remove(Book bookIn);
        Task Remove(string id);

    }
    public class BookService : IBookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public async Task<List<Book>> Get() => await _books.Find(book => true).ToListAsync();
        public async Task<Book> Get(string id) => await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
        public async Task Remove(Book bookIn) => await _books.DeleteOneAsync<Book>(book => book.Id == bookIn.Id);
        public async Task Remove(string id) => await _books.DeleteOneAsync<Book>(book => book.Id == id);
        public async Task<Book> Create(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }
        public async Task<Book> Update(string id, Book bookIn)
        {
            await _books.ReplaceOneAsync<Book>(book => book.Id == id, bookIn);
            return bookIn;
        }
    }
}

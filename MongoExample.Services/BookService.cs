using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoExample.Data;
using MongoExample.Data.Config;
using MongoExample.Services.Utility;
using MongoExample.ViewModels;

namespace MongoExample.Services
{
    public interface IBookService
    {
        Task<List<Book>> Get();
        Task<Book> Get(string id);
        Task<Book> Create(Book book);
        Task<Book> Update(string id, Book bookIn);
        Task Remove(string id);

    }
    public class BookService : IBookService
    {
        private readonly string tableName = string.Empty;
        private readonly IMongoCRUD _mongoCrud;

        public BookService(IDatabaseSettings settings,IMongoCRUD mongoCrud)
        {
            tableName = settings.Books;
            _mongoCrud = new MongoCRUD(settings.DatabaseName);
        }

        public async Task<List<Book>> Get() => await _mongoCrud.LoadRecords<Book>(tableName);
        public async Task<Book> Get(string id) => await _mongoCrud.LoadRecordsById<Book>(tableName, id);
        public async Task Remove(string id) => await _mongoCrud.DeleteRecord<Book>(tableName, id);
        public async Task<Book> Create(Book book)
        {
            Guid g = Guid.NewGuid();

            book.BranchName.BranchId =g.ToString();
            book.BranchName.BranchName = "Gurukul";
            await _mongoCrud.InsertRecord<Book>(tableName,book);
            return book;
        }
        public async Task<Book> Update(string id, Book bookIn)
        {
            await _mongoCrud.UpdateRecord<Book>(tableName,id,bookIn);
            return bookIn;
        }
    }
}

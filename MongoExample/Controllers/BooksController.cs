using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Data;
using MongoExample.Services;
using MongoExample.ViewModels;

namespace MongoExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("Books")]
        public async Task<ActionResult<List<Book>>> Get() =>
          await _bookService.Get();

        [HttpGet(Name = "GetBook")]
        [Route("Book")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            await _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }
    }
}

using api_library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTOs;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var book = await _repository.GetAllBooksAsync();
            return Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(BookDTO bookDto)
        {
            var book = new Book
            {
                BookName = bookDto.BookName,
                ISBNNumber = bookDto.ISBN,
                Publisher = bookDto.Publisher,
                NoOfCopies = bookDto.NoOfCopies,
                ImageURL = bookDto.ImageURL,
                Genre = bookDto.Genre
            };
            await _repository.AddBookAsync(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookDTO bookDto)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            book.BookName = bookDto.BookName;
            book.ISBNNumber = bookDto.ISBN;
            book.Publisher = bookDto.Publisher;
            book.NoOfCopies = bookDto.NoOfCopies;
            book.ImageURL = bookDto.ImageURL;
            book.Genre = bookDto.Genre;

            await _repository.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _repository.DeleteBookAsync(id);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }

}
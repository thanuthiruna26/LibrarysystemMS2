// Controllers/BookBorrowingController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BookBorrowingController : ControllerBase
{
    private readonly IBookBorrowingRepository _repository;

    public BookBorrowingController(IBookBorrowingRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> BorrowBook([FromBody] BookBorrowingRequestDto requestDto)
    {
        var bookBorrowing = new BookBorrowing
        {
            BookId = requestDto.BookId,
            MemberNic = requestDto.MemberNic
        };

        var result = await _repository.BorrowBookAsync(bookBorrowing);
        var responseDto = new BookBorrowingResponseDto
        {
            Id = result.Id,
            BookId = result.BookId,
            MemberNic = result.MemberNic,
            BorrowedDate = result.BorrowedDate
        };

        return CreatedAtAction(nameof(GetBorrowingById), new { id = responseDto.Id }, responseDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBorrowingById(int id)
    {
        var borrowing = await _repository.GetBorrowingByIdAsync(id);
        if (borrowing == null)
            return NotFound();

        var responseDto = new BookBorrowingResponseDto
        {
            Id = borrowing.Id,
            BookId = borrowing.BookId,
            MemberNic = borrowing.MemberNic,
            BorrowedDate = borrowing.BorrowedDate,
            ReturnedDate = borrowing.ReturnedDate
        };

        return Ok(responseDto);
    }
}

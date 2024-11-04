using System.Threading.Tasks;

public interface IBookBorrowingRepository
{
    Task<BookBorrowing> BorrowBookAsync(BookBorrowing bookBorrowing);
    Task<BookBorrowing> GetBorrowingByIdAsync(int id);
}
using ProductAPI.Models;

namespace api_library.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(Book book);
        Task<int> UpdateBookAsync(Book book);
        Task<int> DeleteBookAsync(int id);
    }
}
using api_library.Interfaces;
using Microsoft.Data.Sqlite;
using ProductAPI.Models;

namespace ProductAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = new List<Book>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Books", connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    books.Add(new Book
                    {
                        BookName = reader.GetString(0),
                        ISBNNumber = reader.GetInt32(1),
                        Publisher = reader.GetString(2),
                        NoOfCopies = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Genre = reader.GetString(5)
                    });
                }
            }
            return books;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            Book book = null;
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Book WHERE ISBNnumber = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    book = new Book
                    {
                        BookName = reader.GetString(0),
                        ISBNNumber = reader.GetInt32(1),
                        Publisher = reader.GetString(2),
                        NoOfCopies = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Genre = reader.GetString(5)
                    };
                }
            }
            return book;
        }

        public async Task<int> AddBookAsync(Book book)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand(
                    "INSERT INTO Books (BookName, ISBN, Publisher, NoOfCopies, ImageURL, Genre) VALUES (@name, @isbn, @publisher, @copies, @image, @genre)", connection);
                command.Parameters.AddWithValue("@name", book.BookName);
                command.Parameters.AddWithValue("@isbn", book.ISBNNumber);
                command.Parameters.AddWithValue("@publisher", book.Publisher);
                command.Parameters.AddWithValue("@copies", book.NoOfCopies);
                command.Parameters.AddWithValue("@image", book.ImageURL);
                command.Parameters.AddWithValue("@genre", book.Genre);
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateBookAsync(Book book)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand(
                    "UPDATE Books SET BookName = @name, Publisher = @publisher, NoOfCopies = @copies, ImageURL = @image, Genre = @genre WHERE ISBNNumber = @isbn", connection);
               command.Parameters.AddWithValue("@name", book.BookName);
                command.Parameters.AddWithValue("@isbn", book.ISBNNumber);
                command.Parameters.AddWithValue("@publisher", book.Publisher);
                command.Parameters.AddWithValue("@copies", book.NoOfCopies);
                command.Parameters.AddWithValue("@image", book.ImageURL);
                command.Parameters.AddWithValue("@genre", book.Genre);
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteBookAsync(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand("DELETE FROM Books WHERE ISBNNumber = @isbn", connection);
                command.Parameters.AddWithValue("@isbn", id);
                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}
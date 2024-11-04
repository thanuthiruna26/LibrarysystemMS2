using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

public class BookBorrowingRepository : IBookBorrowingRepository
{
    private readonly Database _database;

    public BookBorrowingRepository(Database database)
    {
        _database = database;
    }

    public async Task<BookBorrowing> BorrowBookAsync(BookBorrowing bookBorrowing)
    {
        using (var connection = _database.GetConnection())
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO BookBorrowing (BookId, MemberNic, BorrowedDate)
                VALUES ($bookId, $memberNic, $borrowedDate);
                SELECT last_insert_rowid();
            ";
            command.Parameters.AddWithValue("$bookId", bookBorrowing.BookId);
            command.Parameters.AddWithValue("$memberNic", bookBorrowing.MemberNic);
            command.Parameters.AddWithValue("$borrowedDate", DateTime.UtcNow.ToString("o"));

            bookBorrowing.Id = (int)(long)await command.ExecuteScalarAsync();
            return bookBorrowing;
        }
    }

    public async Task<BookBorrowing> GetBorrowingByIdAsync(int id)
    {
        using (var connection = _database.GetConnection())
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT Id, BookId, MemberNic, BorrowedDate, ReturnedDate
                FROM BookBorrowing
                WHERE Id = $id;
            ";
            command.Parameters.AddWithValue("$id", id);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return new BookBorrowing
                    {
                        Id = reader.GetInt32(0),
                        BookId = reader.GetString(1),
                        MemberNic = reader.GetString(2),
                        BorrowedDate = DateTime.Parse(reader.GetString(3)),
                        ReturnedDate = reader.IsDBNull(4) ? (DateTime?)null : DateTime.Parse(reader.GetString(4))
                    };
                }
            }
        }
        return null;
    }
}
using api_library.Interfaces;
using Microsoft.Data.Sqlite;
using ProductAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Read the connection string from configuration
// Read the connection string from configuration
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the repository with the connection string
builder.Services.AddScoped<IBookRepository>(provider => new BookRepository(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(); // Add support for controllers
    services.AddSingleton<Database>(); // Add singleton database class
    services.AddScoped<IBookBorrowingRepository, BookBorrowingRepository>(); // Register repository
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

// Database creation
using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();
    var createBooksTableCommand = new SqliteCommand(
    @"CREATE TABLE IF NOT EXISTS Books 
        (
        ISBN INTEGER PRIMARY KEY,
        BookName TEXT, 
        Publisher TEXT, 
        NoOfCopies INTEGER,
        ImageURL TEXT,
        Genre TEXT
        )", connection);

    var createMembersTableCommand = new SqliteCommand(
        @"CREATE TABLE IF NOT EXISTS Members 
        (
        NIC INTEGER PRIMARY KEY,
        FirstName TEXT, 
        LastName TEXT, 
        RegisterdDate DATETIME
        )", connection);


 var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE BookBorrowing (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    BookId TEXT NOT NULL,
                    MemberNic TEXT NOT NULL,
                    BorrowedDate TEXT NOT NULL,
                    ReturnedDate TEXT
                )
            ";
            command.ExecuteNonQuery();

    // Execute the commands
    createBooksTableCommand.ExecuteNonQuery();
    createMembersTableCommand.ExecuteNonQuery();

public SqliteConnection GetConnection() => new SqliteConnection($"Data Source={DatabaseFileName}");
}

app.UseAuthorization();

app.MapControllers();

app.Run();



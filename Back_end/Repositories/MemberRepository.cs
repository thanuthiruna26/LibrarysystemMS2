using api_library.Interfaces;
using api_library.Models;
using Microsoft.Data.Sqlite;
using ProductAPI.Models;

namespace api_library.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString;

        public MemberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            var member = new List<Member>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Members", connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    member.Add(new Member
                    {
                        NIC = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        RegisteredDate = reader.GetDateTime(3)                      
                    });
                }
            }
            return member;
        }

        public async Task<Member> GetMemberByIdAsync(int NIC)
        {
            Member member = null;
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Member WHERE NIC = @id", connection);
                command.Parameters.AddWithValue("@id", NIC);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    member = new Member
                    {
                        NIC = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        RegisteredDate = reader.GetDateTime(3)
                    };
                }
            }
            return member;
        }

        public async Task<int> AddMemberAsync(Member member)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand(
                    "INSERT INTO Member (NIC, FirstName, LastName, RegisteredDate) VALUES (@nic, @firstname, @lastname, @registereddate)", connection);
                command.Parameters.AddWithValue("@nic", member.NIC);
                command.Parameters.AddWithValue("@firstname", member.FirstName);
                command.Parameters.AddWithValue("@lastname", member.LastName);
                command.Parameters.AddWithValue("@registereddate", member.RegisteredDate);
              
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateMemberAsync(Member member)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand(
                    "UPDATE Member SET FirstName = @firstname, LastName = @lastname, RegisteredDate = @registereddate WHERE NIC = @nic", connection);
                command.Parameters.AddWithValue("@nic", member.NIC);
                command.Parameters.AddWithValue("@firstname", member.FirstName);
                command.Parameters.AddWithValue("@lastname", member.LastName);
                command.Parameters.AddWithValue("@registereddate", member.RegisteredDate);
               
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteMemberAsync(int NIC)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqliteCommand("DELETE FROM Member WHERE NIC = @nic", connection);
                command.Parameters.AddWithValue("@isbn", NIC);
                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}

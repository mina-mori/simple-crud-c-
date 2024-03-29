using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Data;

namespace Infrastructure
{
    public class SQLiteDbContext
    {
        private readonly SqliteConnection _connection;

        public SQLiteDbContext(string connectionString)
        {
            Batteries.Init();
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            // Check if the Users table exists
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Users'";
            var result = command.ExecuteScalar();
            if (result == null)
            {
                // If the Users table doesn't exist, create it
                using var createCommand = _connection.CreateCommand();
                createCommand.CommandText = "CREATE TABLE Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Email TEXT, Phone TEXT, FirstName TEXT, LastName TEXT)";
                createCommand.ExecuteNonQuery();
                InsertDefaultUsers();
            }
        }
        private void InsertDefaultUsers()
        {
            // Generate fake data for default users
            string[] firstNames = { "John", "Emma", "Michael", "Olivia", "William", "Sophia", "James", "Ava", "Alexander", "Isabella" };
            string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };

            // Insert default users into the Users table
            using var insertCommand = _connection.CreateCommand();
            insertCommand.CommandText = "INSERT INTO Users (Email, Phone, FirstName, LastName) VALUES (@Email, @Phone, @FirstName, @LastName)";
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                string email = $"user{i + 1}@example.com";
                string phone = $"123-456-789{i + 1:00}";
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];

                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.Parameters.AddWithValue("@Phone", phone);
                insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                insertCommand.Parameters.AddWithValue("@LastName", lastName);
                insertCommand.ExecuteNonQuery();

                // Clear parameters for next iteration
                insertCommand.Parameters.Clear();
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public SqliteCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void ExecuteNonQuery(string query)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        public object ExecuteScalar(string query)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = query;
            return command.ExecuteScalar();
        }

        public DataTable ExecuteQuery(string query)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = query;
            using var reader = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;
        }
    }
}



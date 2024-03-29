using Xunit;
using Microsoft.Data.Sqlite;
using System.IO;
using System;

namespace Infrastructure.Tests
{
	public class SQLiteDbContextTests : IDisposable
	{
		private readonly string _testDatabasePath;
		private readonly string _connectionString;
		private readonly SQLiteDbContext _dbContext;

		public SQLiteDbContextTests()
		{
			_testDatabasePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".db");
			_connectionString = $"Data Source={_testDatabasePath}";
			_dbContext = new SQLiteDbContext(_connectionString);
		}

		[Fact]
		public void Constructor_InitializesDatabase()
		{
			
			Assert.True(File.Exists(_testDatabasePath));
		}

		[Fact]
		public void InsertDefaultUsers_AddsUsersToDatabase()
		{
			
			_dbContext.InsertDefaultUsers();

			
			var query = "SELECT COUNT(*) FROM Users";
			var count = Convert.ToInt32(_dbContext.ExecuteScalar(query));
			Assert.Equal(10, count); // 10 default users inserted
		}

		[Fact]
		public void ExecuteNonQuery_InsertsDataIntoDatabase()
		{
			
			var query = "INSERT INTO Users (Email, Phone, FirstName, LastName) VALUES ('test@example.com', '123-456-7890', 'Test', 'User')";

			
			_dbContext.ExecuteNonQuery(query);

			
			var selectQuery = "SELECT COUNT(*) FROM Users WHERE Email = 'test@example.com'";
			var count = Convert.ToInt32(_dbContext.ExecuteScalar(selectQuery));
			Assert.Equal(1, count); // User inserted successfully
		}

		[Fact]
		public void ExecuteQuery_ReturnsDataTable()
		{
			
			var query = "SELECT * FROM Users";

			
			var dataTable = _dbContext.ExecuteQuery(query);

			
			Assert.NotNull(dataTable);
			Assert.NotEmpty(dataTable.Rows);
			Assert.Equal(4, dataTable.Columns.Count); // Id, Email, Phone, FirstName, LastName
		}

		public void Dispose()
		{
			_dbContext.Dispose();
			File.Delete(_testDatabasePath); // Clean up the test database file
		}
	}
}

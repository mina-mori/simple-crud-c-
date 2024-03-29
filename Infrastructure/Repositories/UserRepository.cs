using Infrastrucure.Interfaces;
using DomainEntities.Entities;
using Infrastructure;
using System;

namespace Infrastrucure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteDbContext _dbContext;

        public UserRepository(SQLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                List<User> userList = new List<User>();
                using var command = _dbContext.CreateCommand();
                command.CommandText = "SELECT Id, Email, Phone, FirstName, LastName FROM Users ORDER BY FirstName";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(0),
                        Email = reader.GetString(1),
                        Phone = reader.GetString(2),
                        FirstName = reader.GetString(3),
                        LastName = reader.GetString(4)
                    };
                    userList.Add(user);
                }
                return userList;
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while fetching users.", ex);
            }
        }

        public async Task<User> GetUserAsync(int userId)
        {
            try
            {
                using var command = _dbContext.CreateCommand();
                command.CommandText = "SELECT Id, Email, Phone, FirstName, LastName FROM Users WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", userId);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32(0),
                        Email = reader.GetString(1),
                        Phone = reader.GetString(2),
                        FirstName = reader.GetString(3),
                        LastName = reader.GetString(4)
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while fetching user details.", ex);
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                using var command = _dbContext.CreateCommand();
                command.CommandText = "INSERT INTO Users (Email, Phone, FirstName, LastName) " +
                                      "VALUES (@Email, @Phone, @FirstName, @LastName)";
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while adding user.", ex);
            }
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            try
            {
                using var command = _dbContext.CreateCommand();
                command.CommandText = "UPDATE Users SET Email = @Email, Phone = @Phone, " +
                                      "FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while updating user.", ex);
            }
        }

        public async Task RemoveUserAsync(int userId)
        {
            try
            {
                using var command = _dbContext.CreateCommand();
                command.CommandText = "DELETE FROM Users WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", userId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while removing user.", ex);
            }
        }
    }
}

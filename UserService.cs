using System;
using System.Data.SqlClient;

namespace ADO.NET_Assignment
{
    /// <summary>
    /// Handles user registration, login, and forgot password functionality.
    /// </summary>
    public class UserService
    {
        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Registers a new user by storing username, email, and encrypted password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The plain text password.</param>
        public void Register(string username, string email, string password)
        {
            // Encrypt password
            string encryptedPassword = PasswordHelper.CaesarEncrypt(password, 3);

            // Insert user into database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", encryptedPassword);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Logs in the user by validating the username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The plain text password.</param>
        /// <returns>True if login is successful, otherwise false.</returns>
        public bool Login(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Password FROM Users WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string storedPassword = reader["Password"].ToString();
                    return PasswordHelper.CaesarEncrypt(password, 3) == storedPassword;
                }
            }
            return false;
        }

        /// <summary>
        /// Resets the user's password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="newPassword">The new plain text password.</param>
        public void ForgotPassword(string username, string newPassword)
        {
            // Validate new password
            while (!ValidationHelper.IsValidPassword(newPassword))
            {
                Console.WriteLine("Password must be at least 6 characters long and contain both letters and numbers. Please enter a valid password:");
                newPassword = PasswordHelper.ReadPassword();
            }

            // Check if the user exists
            if (UserExists(username))
            {
                // Encrypt new password
                string encryptedPassword = PasswordHelper.CaesarEncrypt(newPassword, 3);

                // Update password in database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Users SET Password = @Password WHERE Username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", encryptedPassword);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Password reset successfully.");
            }
            else
            {
                Console.WriteLine("Username not found. Please check the username and try again.");
            }
        }

        /// <summary>
        /// Checks if a user exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        public bool UserExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                int userCount = (int)command.ExecuteScalar();
                return userCount > 0;
            }
        }
    }
}

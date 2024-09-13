using System;
using System.Text.RegularExpressions;

namespace ADO.NET_Assignment
{
    public static class ValidationHelper
    {
        // Regex pattern for email validation
        private static readonly Regex EmailRegex = new Regex(@"^[\w\.-]+@[\w\.-]+\.\w+$", RegexOptions.Compiled);

        /// <summary>
        /// Prompts the user for a valid email and ensures correct input is stored.
        /// </summary>
        /// <returns>A valid email address.</returns>
        public static string GetValidEmail()
        {
            string email;
            while (true)
            {
                Console.Write("Enter Email: ");
                email = Console.ReadLine();

                if (IsValidEmail(email))
                {
                    return email; // Return correct email once validation passes
                }
                else
                {
                    Console.WriteLine("Invalid email format. Please try again.");
                }
            }
        }

        /// <summary>
        /// Validates if the provided email format is valid.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>True if the email format is valid; otherwise false.</returns>
        public static bool IsValidEmail(string email)
        {
            return EmailRegex.IsMatch(email);
        }

        /// <summary>
        /// Prompts the user for a valid password and ensures correct input is stored.
        /// </summary>
        /// <returns>A valid password.</returns>
        public static string GetValidPassword()
        {
            string password;
            while (true)
            {
                Console.Write("Enter Password: ");
                password = PasswordHelper.ReadPassword(); // Hides password input if required

                if (IsValidPassword(password))
                {
                    return password; // Return correct password once validation passes
                }
                else
                {
                    Console.WriteLine("Password must be at least 6 characters long and contain both letters and numbers. Please try again.");
                }
            }
        }

        /// <summary>
        /// Validates if the password meets the required criteria.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password is valid; otherwise false.</returns>
        public static bool IsValidPassword(string password)
        {
            // Password must be at least 6 characters long, and contain both letters and numbers
            return password.Length >= 6 && Regex.IsMatch(password, @"[A-Za-z]") && Regex.IsMatch(password, @"[0-9]");
        }
    }
}

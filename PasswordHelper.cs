using System;

namespace ADO.NET_Assignment
{
    /// <summary>
    /// Provides utility methods for password encryption and validation using Caesar Cipher.
    /// </summary>
    public static class PasswordHelper
    {

        /// <summary>
        /// Encrypts a password using Caesar Cipher with a shift value. 
        /// Handles both letters and numbers.
        /// </summary>
        /// <param name="password">The plain text password.</param>
        /// <param name="shift">The number of positions to shift the characters.</param>
        /// <returns>The encrypted password.</returns>
        public static string CaesarEncrypt(string password, int shift)
        {
            char[] buffer = password.ToCharArray();

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];

                if (char.IsLetter(letter))
                {
                    // Handle alphabet letters
                    char d = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)((((letter + shift) - d) % 26) + d);
                }
                else if (char.IsDigit(letter))
                {
                    // Handle numbers
                    letter = (char)((((letter - '0' + shift) % 10) + '0'));
                }

                buffer[i] = letter;
            }

            return new string(buffer);
        }

        /// <summary>
        /// Reads the password from the console with masking (hidden input).
        /// </summary>
        /// <returns>The entered password as plain text.</returns>
        public static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Handle Backspace and other non-character inputs
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // Erase the * from the console
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}

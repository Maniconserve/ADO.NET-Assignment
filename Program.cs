using ADO.NET_Assignment;
using System.Configuration;
using System;
class Program
{
    static void Main(string[] args)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        UserService userService = new UserService(connectionString);

        bool continueProgram = true;

        while (continueProgram)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1: Register");
            Console.WriteLine("2: Login");
            Console.WriteLine("3: Forgot Password");
            Console.WriteLine("4: Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Handle user registration process
                    HandleRegistration(userService);
                    break;

                case "2":
                    // Handle user login process
                    HandleLogin(userService);
                    break;

                case "3":
                    // Handle forgot password process
                    HandleForgotPassword(userService);
                    break;

                case "4":
                    Console.WriteLine("Exiting the program.");
                    continueProgram = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Handles the user registration process.
    /// </summary>
    /// <param name="userService">Instance of the UserService class.</param>
    private static void HandleRegistration(UserService userService)
    {
        Console.WriteLine("\nRegistration:");
        string username;

        // Loop until a unique username is provided by the user
        do
        {
            Console.Write("Enter Username: ");
            username = Console.ReadLine();

            // Check if the username already exists
            if (userService.UserExists(username))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
            }
        } while (userService.UserExists(username));

        // Get a valid email from the user
        string email = ValidationHelper.GetValidEmail();

        // Get a valid password from the user
        string password = ValidationHelper.GetValidPassword();

        // Register the user with the provided details
        userService.Register(username, email, password);
        Console.WriteLine("Registration successful.");
    }

    /// <summary>
    /// Handles the user login process.
    /// </summary>
    /// <param name="userService">Instance of the UserService class.</param>
    private static void HandleLogin(UserService userService)
    {
        Console.WriteLine("\nLogin:");

        // Get the username from the user
        Console.Write("Enter Username: ");
        string loginUsername = Console.ReadLine();

        // Get the password from the user
        Console.Write("Enter Password: ");
        string loginPassword = Console.ReadLine();

        // Verify the credentials and check if login is successful
        if (userService.Login(loginUsername, loginPassword))
        {
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Login failed! Please try again.");
        }
    }

    /// <summary>
    /// Handles the forgot password process where the user can reset their password.
    /// </summary>
    /// <param name="userService">Instance of the UserService class.</param>
    private static void HandleForgotPassword(UserService userService)
    {
        Console.WriteLine("\nForgot Password:");

        // Get the username from the user
        Console.Write("Enter Username: ");
        string forgotUsername = Console.ReadLine();

        // Get a new valid password from the user
        string newPassword = ValidationHelper.GetValidPassword();

        // Reset the user's password with the new password
        userService.ForgotPassword(forgotUsername, newPassword);
    }
}

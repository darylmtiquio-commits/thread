using System;

class AgeValidation
{
    static void Main()
    {
        try
        {
            Console.Write("Enter your age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            if (age > 18)
            {
                Console.WriteLine("Age is valid. Access granted.");
            }
            else
            {
                // Throw an exception if age is 18 or below
                throw new Exception("Invalid age. You must be over 18.");
            }
        }
        catch (Exception ex)
        {
            // Handle the thrown exception
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // Code that runs no matter what
            Console.WriteLine("Program execution completed.");
        }
    }
}

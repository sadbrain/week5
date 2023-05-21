using static System.Console;
namespace BookMan.ConsoleApp
{
    using Controllers;
    internal class Program
    {
        private static void Main(string[] args)
        {
            BookController controller = new BookController();
            while (true)
            {
                Write("Request> ");
                string request = ReadLine();
                switch (request.ToLower()) 
                {
                    case "single":
                        controller.Single(0);
                        break;

                    case "create":
                        controller.Create();
                        break;

                    default:
                        WriteLine("Unknown command");
                        break;


                }
            }
            ReadKey();
        }
    }


}
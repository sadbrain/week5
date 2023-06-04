namespace BookMan.ConsoleApp
{
    using FrameWork;


    internal partial class Program
    {
        /* 
         private static void Main(string[] args)
         {
             ///vì thằng này cần BookController để ghép nối dữ liệu với giao diện
             ///cần tạo đối tượng SimpleDataAccess để phù hợp với hàm tạo controller
             ///tại sao lại tạo thằng này để đây vì mình cần chạy chương trình để nơi này.

             SimpleDataAccess context = new SimpleDataAccess();
             //tạo đối tượng controller sẽ tạo thêm đối tượng repository, đối tượng repository lại có biến tham chiếu tới đối tượng SimpleDataAccess
             BookController controller = new BookController(context);

             while (true)
             {
                 Write("Request> ");
                 string request = ReadLine();
                 switch (request.ToLower())
                 {
                     case "single":
                         controller.Single(1);
                         break;

                     case "list":
                         controller.List();
                         break;

                     case "create":
                         controller.Create();
                         break;

                     case "update":
                         controller.Update(1);
                         break;

                     default:
                         WriteLine("Unknown command");
                         break;


                 }
             }
         } */

        /* private static void Main(string[] args)
         {
  
             //chạy reder to file list file ? path = list.json
             while (true)
             {
                 ViewHelp.Write("# Request >>> ", ConsoleColor.Green);
                 string request = Console.ReadLine();
                 r.Forward(request);
                 Console.WriteLine();
             }
         }
        */
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConfigRouter();
            while (true)
            {
                ViewHelp.Write("# Request >>> ", ConsoleColor.Green);
                string request = Console.ReadLine();

                try
                {
                    Router.Instance.Forward(request);
                }
                catch (Exception e)
                {
                    ViewHelp.WriteLine(e.Message, ConsoleColor.Red);
                }
                finally
                {
                    Console.WriteLine();
                }
            }
        }
        //hai hàm test route

        private static void About(Parameter parameter)
        {
            ViewHelp.WriteLine("BOOK MANAGER version 1.0", ConsoleColor.Green);
            ViewHelp.WriteLine("by dhnynit@TuHocIct.com", ConsoleColor.Magenta);
        }
        


        private static void Help(Parameter parameter)
        {
            if (parameter == null)
            {
                ViewHelp.WriteLine("SUPPORTED COMMANDS:", ConsoleColor.Green);
                ViewHelp.WriteLine(Router.Instance.GetRoutes(), ConsoleColor.Yellow);
                ViewHelp.WriteLine("type: help ? cmd= <command> to get command details", ConsoleColor.Cyan);
                return;
            }
            var command = parameter["cmd"].ToLower();
            ViewHelp.WriteLine(Router.Instance.GetHelp(command), ConsoleColor.DarkBlue);
        }

    }


}
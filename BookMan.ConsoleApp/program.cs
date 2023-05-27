using static System.Console;
namespace BookMan.ConsoleApp
{
    using FrameWork;
    using Controllers;
    using DataServices;
    internal class Program
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
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var context = new SimpleDataAccess();
            BookController controller = new BookController(context);

            Router r = Router.Instance;
            r.Register("about", About);
            r.Register("help", Help);
            r.Register(route: "create",
                action: p => controller.Create(),
                help: "[create]\r\nenter new book");
            r.Register(route: "update",
                action: p => controller.Update(p["id"].ToInt()),
                help: "[update ? id = <value>]\r\nfind and update a book");
            r.Register(route: "list",
                action: p => controller.List(),
                help: "[list]\r\ndisplay all book");
            r.Register(route: "single",
                action: p => controller.Single(p["id"].ToInt()),
                help: "[single ? id = < value >]\r\ndisplay a book by id");

            //đăng ký hàm xuất thông tin ra file
            r.Register(route: "list file",
                       action: p => controller.List(p["path"]),
                       help: "[list file ? path = <value>]");
            r.Register(route: "single file",
                       action: p => controller.Single(p["id"].ToInt(), p["path"]),
                       help: "[single file ? id = <value> & path = <value>]");
            //chạy reder to file list file ? path = list.json
            while (true)
            {
                ViewHelp.Write("# Request >>> ", ConsoleColor.Green);
                string request = Console.ReadLine();
                r.Forward(request);
                Console.WriteLine();
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
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            var command = parameter["cmd"].ToLower();
            ViewHelp.WriteLine(Router.Instance.GetHelp(command));
        }
    }


}
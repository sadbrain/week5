namespace BookMan.ConsoleApp
{
    using Models;
    using Controllers;
    using DataServices;
    using BookMan.ConsoleApp.FrameWork;
    using System;

    internal partial class Program
    {
        private static void ConfigRouter()
        {
            IDataAccess context = new XmlDataAccess();
            BookController controller = new BookController(context);

            ShellController shell = new ShellController(context);
            //r.Register(route: "",
            //    action: null,
            //    help: "");            
            #region helper
            Router r = Router.Instance;
            r.Register("about", About);
            r.Register("help", Help);
            r.Register(route: "create",
                action: p => controller.Create(),
                help: "[create] Enter a new book");
            r.Register(route: "do create",
                action: p => controller.Create(toBook(p)),
                help: "this route should be used only in code");

            r.Register(route: "list",
                action: p => controller.List(),
                help: "[list] display all books");
            r.Register(route: "list file",
                action: p =>  controller.List(p["path"]),
                help: "[list file ? path = <value>] retrieve all book to file");
            r.Register(route: "single",
                action: p => { 
                    if (!ParameterIsNull(p, "please enter single ? id = < value >")) controller.Single(p["id"].ToInt()); 
                },
                help: "[single ? id = < value >] display a book by id");

            r.Register(route: "single file",
                action: p => {
                    if (!ParameterIsNull(p, "please enter single ? id= <value> & path = <value>"))  controller.Single(p["id"].ToInt(), p["path"]);
                },
                help: "[single file ? id = <value> & path = <value>] retrieve a book to file");

            r.Register(route: "update",
                        action: p =>
                        {
                            if (!ParameterIsNull(p, "please enter update ? id = <value>")) controller.Update(p["id"].ToInt());
                        },
                        help: "[update ? id = <value>] find and update a book");

            r.Register(route: "do update",
                        action: p => controller.Update(p["id"].ToInt(), toBook(p)),
                        help: "this route should be used only in code");


            r.Register(route: "delete",
                        action: p => {
                            if (!ParameterIsNull(p, "please enter delete ? id= <value> & path = <value>")) controller.Delete(p["id"].ToInt());
                        },
                        help: "[delete ? id = <value>] delete a book");
            r.Register(route: "do delete",
                        action: p => controller.Delete(p["id"].ToInt(), true),
                        help: "this route should be used only in code");

            r.Register(route: "filter",
                        action: p => {
                            if (!ParameterIsNull(p, "please enter filter ? key = <value>")) controller.Filter(p["key"]);
                        },
                        help: "[filter ? key = <value>] find book by key");

            //add add file có đuôi theo người dùng chọn có trong môt thư mục
            r.Register(route: "add shell",
                        action: p =>  shell.Shell(p["path"], p["ext"]),
                        help: "[add shell ? path = <value> & ext = <value>] add books by a folder");
            //mở file và đọc
            r.Register(route: "read",
                        action: p =>  shell.Read(p["id"].ToInt()),
                        help: "[read ? id = <value>] open file and read book");


            r.Register(route: "mark",
                        action: p => controller.Mark(p["id"].ToInt()),
                        help: "[mark ? id = <value>] mark as read");
            r.Register(route: "unmark",
                       action: p =>  controller.Mark(p["id"].ToInt(), false)    ,
                        help: "[unmark ? id = <value>] unmark as read");
           
            r.Register(route: "show marks",
                        action: p => controller.ShowMarks(),
                        help: "[show marks] display all book mark as read");

            r.Register(route: "clear",
                        action: p => shell.Clear(),
                        help: "[clear] Use with care");
            r.Register(route: "do clear",
                        action: p => shell.Clear(true),
                        help: "[clear] Use with care");

            r.Register(route: "save shell",
                       action: p => shell.Save(),
                       help: "[save shell]");

            r.Register(route: "show stats",
                    action: p => controller.Stats(),
                    help: "[show stats]");

            r.Register(route: "filter and sort",
            action: p => controller.Filter(p["key"], p["optionSort"].ToLower()),
             help: "[filter and sort ? key = <value>] & optionSort = <value>");

            r.Register(route: "open folder",
                action: p => shell.OpenFolder(p["id"].ToInt()),
                help: "[open folder ? id = <value>] open folder contain file was found");
            r.Register(route: "copy file",
                      action: p => shell.CopyFile(p["id"], p["path"]),
                      help: "[copy file? id = <value>, <value2> ... & path = <value>(path of folder)] open folder contain file was found");
            r.Register(route: "auto create",
                    action: p => shell.AutoCreateData(),
                    help: "[auto create] auto add new book when have a new file in folder");
            r.Register(route: "auto delete",
                        action: p => shell.AutoDeleteData(),
                        help: "[auto delete] auto delete a book when have deleted new file in folder");
            //local function check Parameter is null and render a message
            bool ParameterIsNull(Parameter p, string message, ConsoleColor color = ConsoleColor.Red)
            {
                if (p == null)
                {
                    ViewHelp.WriteLine(message, color);
                    return true;
                }
                return false;
            }
            //local function to convert parameter to book object
            Book toBook(Parameter p)
            {
                var b = new Book();
                if (p.ContainsKey("id")) b.Id = p["id"].ToInt();
                if (p.ContainsKey("author")) b.Author = p["author"];
                if (p.ContainsKey("title")) b.Title = p["title"];
                if (p.ContainsKey("publisher")) b.Publisher = p["publisher"];
                if (p.ContainsKey("isbn")) b.Isbn = p["isbn"];
                if (p.ContainsKey("year")) b.Year = p["year"].ToInt();
                if (p.ContainsKey("edition")) b.Edition = p["edition"].ToInt();
                if (p.ContainsKey("isbn")) b.Isbn = p["isbn"];
                if (p.ContainsKey("tags")) b.Tags = p["tags"];
                if (p.ContainsKey("description")) b.Description = p["description"];
                if (p.ContainsKey("file")) b.File = p["file"];
                if (p.ContainsKey("rate")) b.Rating = p["rate"].ToInt();
                if (p.ContainsKey("reading")) b.Reading = p["reading"].ToBool();
                return b;
            }
            #endregion  

        }
    }
}
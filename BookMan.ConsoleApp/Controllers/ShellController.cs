using System.Diagnostics;
namespace BookMan.ConsoleApp.Controllers
{
    using BookMan.ConsoleApp.FrameWork;
    using DataServices;
    using Models;
    internal class ShellController : ControllerBase
    {

        protected Repository repository;

        public ShellController(IDataAccess context)
        {
            repository = new Repository(context);
        }

        //add nhiều sách bằng cách đưa đường dẫn về thư mục, md là file PDF
        public void Shell(string folder, string ext = ".pdf")
        {
            //check đường dãn đúng không
            if (!Directory.Exists(folder))
            {
                Error("Folder not found");
                return;

            }
            //lấy tất cả các file có đuôi từ người dùng chọn, ở trong thư mục cha lẫn con
            var files = Directory.GetFiles(folder, ext ?? "*.pdf", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                //lần lượt thêm những cuốn sách có title là tền file không có đuôi lẫn đường dẫn, và gán lại đường dẫn
                repository.Insert(new Book { Title = Path.GetFileNameWithoutExtension(f), File = f });
            }
           
            if (files.Length > 0)
            {
                //Render(new BookListView(Repository.Select()));
                Success($"{files.Length} item(s) found!");
                return;
            }
            Inform("No item found!", "Sorry!");
        }

        public void Read(int id)
        {
            var book = repository.Select(id);
            if (book == null)
            {
                Error("Book not found!");
                return;
            }
            //check file có tồn tại không
            if (!File.Exists(book.File))
            {
                Error("File not found!");
                return;
            }
            //mở file
            Process.Start(new ProcessStartInfo(@book.File) { UseShellExecute = true });
            Success($"You are reading the book '{book.Title}'");
        }

        //xóa tất cả cuốn sách
        public void Clear(bool process = false)
        {
            if (!process)
            {
                Confirm("Do you really want to clear the shell? ", "do clear");
                return;
            }
            repository.Clear();
            Success("The shell has been cleared");
        }

        public void Save()
        {
            repository.SaveChanges();
            Success("data save!");
        }
    }
}
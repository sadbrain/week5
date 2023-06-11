using System.Diagnostics;
namespace BookMan.ConsoleApp.Controllers
{
    using BookMan.ConsoleApp.FrameWork;
    using DataServices;
    using Models;
    using static System.Net.Mime.MediaTypeNames;

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

        public void OpenFolder(int id)
        {
            var book = repository.Select(id);
            if (book == null)
            {
                Error("Book not found!");
                return;
            }
            if (!File.Exists(book.File))
            {
                Error("File not found!");
                return;
            }
            //lấy folder của file
            var fo = Path.GetDirectoryName(book.File);
            //mở folder có chứa file đó
            Process.Start(new ProcessStartInfo(fo) { UseShellExecute = true });
            Success($"You are openning the folder contain '{book.Title}'");
        }

        public void CopyFile(string ids, string folder)
        {

            var arrId = ids.Split(',');
            foreach(var id in arrId) 
            {
                Console.WriteLine("Witd id: " + id);
                var book = repository.Select(id.Trim().ToInt());
                if (book == null)
                {
                    Error("Book not found!");
                    continue;
                }
                if (!File.Exists(book.File))
                {
                    Error("File not found!");
                    continue;
                }
                if (!Directory.Exists(folder))
                {
                    Error("Folder incorrect!");
                    return;
                }
                //lấy tên file
                var fe = Path.GetFileName(book.File);
                //gộp tên thư mục đích với file lại thành một đường dẫn
                //một trong hai rỗng thì nhận thằng còn lại
                //nếu para2 is absolute thì nhận thằng para2
                string destFile = Path.Combine(folder, fe);
                //tiến hành copy file này sang file thứ hai và file thứ hai có thể cùng tên
                //tên file thứ hai mình nên để nằm trong đương dẫn folder đích muốn đến 
                //nhận hai tham số chính file muốn copy, file nhận copy đó
                File.Copy(book.File, destFile, true);

                Success($"You are copied!");
                Console.WriteLine();

                    
            }
 
        }


        //tự động tạo dữ liệu bằng cách lập nhóm các file theo thư mục
        //kiểm tra thư mục ngoài có file mới không, có thì thêm data vào chương trình
        public void AutoCreateData()
        {
            //tiến hành nhóm các file theo thư mục
            IEnumerable<IGrouping<string, Book>> model = repository.Stats();
            //lập qua từng thư mục
            foreach (var folder in model)
            {

                if(!Directory.Exists(folder.Key))
                {
                    Console.WriteLine($"{folder.Key}");
                    Error("Folder incorrect!");
                    continue;
                }
                //lấy tất cả file trong thư mục tại thời điểm này 
                //tên thư mục folder.Key
                var filesInFolder = Directory.GetFiles(folder.Key, "*.pdf");
                //lấy những cuốn sách được nhóm theo thư mục, 
                var book = folder.ToList();
                //lâp qua danh sách các file ở ngoài chương trình
                foreach (var file in filesInFolder)
                {
                    //biến này để kiểm tra thêm file có khác với những file trong chương trình không
                    var isInsert = true;
                    //lập qua những cuốn sách được nhóm
                    foreach(var b in book)
                    {
                        
                        //nếu file trong chương trình và file ngoài thư mục như nhau
                        //sửa lại biến isInsert
                        //xóa sách hiện tại để tối ưu vòng lập

                        if (file == b.File)
                        {
                            isInsert = false;
                            book.Remove(b);
                            break;
                        }


                    }

                    if (isInsert)
                    {
                        repository.Insert(new Book { Title = Path.GetFileNameWithoutExtension(file), File = file });
                        Console.WriteLine(Path.GetFileName(file));
                        Save();
                    }
                }



            }
        }


        public void AutoDeleteData()
        {
            //tiến hành nhóm các file theo thư mục
            IEnumerable<IGrouping<string, Book>> model = repository.Stats();
            //lập qua từng thư mục
            foreach (var folder in model)
            {
                if (!Directory.Exists(folder.Key))
                {
                    Console.WriteLine($"{folder.Key}");
                    Error("Folder incorrect!");
                    continue;
                }
                //lấy tất cả file trong thư mục tại thời điểm này 
                //tên thư mục folder.Key
                var filesInFolder = Directory.GetFiles(folder.Key, "*.pdf").ToList();

                //lấy những cuốn sách được nhóm theo thư mục
                var book = folder.ToList();
                //lập qua các cuốn sách để ngoài chương trình
                foreach (var b in book)
                {
                    //biến này để kiểm tra là sach trong chương trình không băng file ngoài chương trình hay không
                    var isDelete = true;
                    Console.WriteLine(b.File);

                    //lập qua những cuốn sách được nhóm
                    foreach (var file in filesInFolder)
                    {
 


                        //nếu file trong chương trình và file ngoài thư mục như nhau
                        //sửa lại biến isDelete
                        //xóa file hiện tại để tối ưu vòng lập

                        if (file == b.File)
                        {
                            isDelete = false;
                            filesInFolder.Remove(file);
                            break;
                        }


                    }
                    Console.WriteLine();

                    if (isDelete)
                    {
                        repository.Books.Remove(b);
                        Console.WriteLine(b.File + "auto deleted");
                        repository.SaveChanges();
                    }
                    
                }



            }
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
            Success("data saved!");
        }
    }
}
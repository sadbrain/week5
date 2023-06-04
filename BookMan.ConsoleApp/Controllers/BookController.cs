
namespace BookMan.ConsoleApp.Controllers
{
    using BookMan.ConsoleApp.FrameWork;
    using DataServices;
    using Models;
    using Views;

    /// <summary>
    /// Lớp điều khiển, giúp ghép nối dữ liêu với giao diện
    /// hàm này sẽ gián tiếp xử lý dữ liệu thông qua lớp Repository
    /// </summary>
    class BookController : ControllerBase
    {

        protected Repository Repository;
        public BookController(IDataAccess context)
        {
            //tạo đối tượng của lớp Repostitory để có thể dùng các method xử lý dữ liệu
            Repository = new Repository(context);

        }
        /// <summary>
        /// ghép nối dữ liệu 1 cuốn sách với giao diện hiển thị 1 cuôn sách
        /// </summary>
        /// <param name="Id"></param>
        public void Single(int id, string path = "")
        {
            //lấy cuốn sách có id theo điều cầu
            var model = Repository.Select(id);
            //thực hiện lệnh hàm Render in ControllerBase
            Render(new BookSingleView(model), path);
        }

        public void List(string path = "")
        {

            var model = Repository.Select();
            //thực hiện lệnh hàm Render in ControllerBase

            Render(new BookListView(model), path);

        }
        public void Create(Book book = null)
        {
            //nếu book null thì cho người dùng nhập
            if (book == null)
            {
                Render(new BookCreateView());
                return;
            }
            //chèn book vào nơi lưu trữ
            Repository.Insert(book);
            Success("Book created");
        }
        public void Update(int id, Book book = null)
        {
            if (book == null)
            {
                var model = Repository.Select(id);
                Render(new BookUpdateView(model));
                return;
            }
            Repository.Update(id, book);
            Success("Book updated");
        }

        public void Delete(int id, bool process = false)
        {

            //đầu tiên md process sẽ là false để tìm sách và đưa ra thông báo có muốn xóa hay không.
            //có thì sẽ gọi hàm này một lần nữa và process sẽ là true, 
            if (!process)
            {
                var b = Repository.Select(id);
                Confirm($"Do you want to delete this book ({b.Title}) ? ", $"do delete ? id = {b.Id}");

            }
            else
            {
                Repository.Delete(id);
                Success("Book deleted");
            }
        }

        public void Filter(string key)
        {
            var models = Repository.Select(key);
            //lọc theo từ khóa 
            if (models.Length == 0)
            {
                Inform("No matched book found!");
            }
            else
            {
                Render(new BookListView(models));
            }
        }

        public void Mark(int id, bool read = true)
        {
            //lọc sách theo id
            var book = Repository.Select(id);

            if (book == null)
            {
                Error("Book not found");
                return;
            }
            //sửa lại value của reading
            book.Reading = read;
            Success($"The book '{book.Title}' are marked as {(read ? "READ" : "UNREAD")}");

        }

        public void ShowMarks()
        {
            //lọc ra những cuốn sách đã đọc và hiện ra màn hình
            var model = Repository.SelectMarked();
            Render(new BookListView(model));
        }

        public void Stats()
        {
            var model = Repository.Stats();
            Render(new BookStatsView(model));
        }

    }


}
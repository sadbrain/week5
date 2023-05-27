
namespace BookMan.ConsoleApp.Controllers
{
    using BookMan.ConsoleApp.FrameWork;
    using DataServices;

    using Views;

    /// <summary>
    /// Lớp điều khiển, giúp ghép nối dữ liêu với giao diện
    /// hàm này sẽ gián tiếp xử lý dữ liệu thông qua lớp Repository
    /// </summary>
    class BookController : ControllerBase
    {

        protected Repository Repository;
        public BookController (SimpleDataAccess context)
        {
            //tạo đối tượng của lớp Repostitory để có thể dùng các method xử lý dữ liệu
            Repository = new Repository (context);
   
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
        public void Create()
        {
            Render(new BookCreateView());
        }   
        public void Update(int id)
        {
            var model = Repository.Select(id);
            Render(new BookUpdateView(model));
        }
    }


}

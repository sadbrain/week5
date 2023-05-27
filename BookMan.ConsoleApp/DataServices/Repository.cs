using System.Collections.Generic;
namespace BookMan.ConsoleApp.DataServices
{
    using Models;
    /// <summary>
    /// trực tiếp xử lý, truy xuất dữ liệu 
    /// </summary>
    public class Repository
    {
        // chỉ đọc, là dữ liệu cần quản lý,
        protected readonly SimpleDataAccess _context;
        // khởi tạo giá trị cho biên thuộc kiểu SimpleDataAcess, và chạy Hàm load
        public Repository(SimpleDataAccess context)
        {
            _context = context;
            _context.Load();
        }
        
        public void SaveChanges() => _context.SaveChanges();

        // hàm trả về list các book
        public List<Book> Books => _context.Books;

        //trả về array các Book
        public Book[] Select() => _context.Books.ToArray();

        /// <summary>
        /// lặp qua list Book và tìm Book có id tướng ứng của người dùng
        /// tìm được trả về sách đó.
        /// không tìm thấy trả về null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book Select(int id)
        {
            foreach (var b in _context.Books)
            {
                if (b.Id == id) return b;
            }
            return null;
        }
        /// <summary>
        /// lặp qua list Book và tìm những Book có key tướng ứng của người dùng
        /// nếu key đúng thì thêm sách đó và một list khác
        /// trả về array những cuốn sách có chứa key người dùng nhập
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Book[] Select(string key)
        {
            var temp = new List<Book>();
            var k = key.ToLower();
            foreach (var b in _context.Books)
            {
                var logic =
                    b.Title.ToLower().Contains(k) ||
                    b.Author.ToLower().Contains(k) ||
                    b.Publisher.ToLower().Contains(k) ||
                    b.Tags.ToLower().Contains(k) ||
                    b.Description.ToLower().Contains(k)
                    ;
                if (logic) temp.Add(b);
            }
            return temp.ToArray();
        }

        /// <summary>
        /// chèn cuốn sách
        /// </summary>
        /// <param name="book"></param>
        public void Insert(Book book)
        {
            //tìm vị trí cuốn sách cuối
            var lastIndex = _context.Books.Count - 1;
            //tính id nếu không có sách id nhận 1, có thì id nhận id vị trí cuổi + 1;
            var id = lastIndex < 0 ? 1 : _context.Books[lastIndex].Id + 1;
            //sửa lại id cuốn sách được thêm
            book.Id = id;
            _context.Books.Add(book);
        }
        /// <summary>
        /// hàm xóa sách và trả về kiểu bool
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            //gọi hàm select tìm sách theo id
            var b = Select(id);
            if (b == null) return false;
            _context.Books.Remove(b);
            return true;
        }
        /// <summary>
        /// hầm update cuốn scash theo id, và trả về bool
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool Update(int id, Book book)
        {   //tìm sách theo id
            var b = Select(id);
            if (b == null) return false;
            //đã tìm thấy thực hiện update
            b.Author = book.Author;
            b.Description = book.Description;
            b.Edition = book.Edition;
            b.File = book.File;
            b.Isbn = book.Isbn;
            b.Publisher = book.Publisher;
            b.Rating = book.Rating;
            b.Reading = book.Reading;
            b.Tags = book.Tags;
            b.Title = book.Title;
            b.Year = book.Year;
            return true;
        }
    }
}
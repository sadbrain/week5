
namespace BookMan.ConsoleApp.DataServices
{
    using Models;
    /// <summary>
    /// trực tiếp xử lý, truy xuất dữ liệu 
    /// </summary>
    public class Repository
    {
        // chỉ đọc, là dữ liệu cần quản lý,
        protected readonly IDataAccess _context;
        // khởi tạo giá trị cho biên thuộc kiểu SimpleDataAcess, và chạy Hàm load
        public Repository(IDataAccess context)
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
        /// 
        public Book Select(int id)
        {
            //trả về phần tử book đầu tiên theo id, nếu không có trả về null
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }
        public Book[] Select(string key)
        {
            var k = key.ToLower();
            //dùng where để lập qua danh sách và lấy những cuốn sách theo điều kiện
            //nếu đk đúng thì book được thêm vào danh sách trả về
            return _context.Books.Where(b =>
                    b.Title.ToLower().Contains(k) ||
                    b.Author.ToLower().Contains(k) ||
                    b.Publisher.ToLower().Contains(k) ||
                    b.Tags.ToLower().Contains(k) ||
                    b.Description.ToLower().Contains(k)).ToArray();
        }

        /// <summary>
        /// chèn cuốn sách
        /// </summary>
        /// <param name="book"></param>
        public void Insert(Book book)
        {
            //nếu chưa có sách nào thì sách được chèn có id là 1
            //nếu đã có sách thì sẽ tìm book có id lớn nhất và + 1
            var id = _context.Books.Count == 0 ? 1 : _context.Books.Max(b => b.Id) + 1;
            book.Id = id;
            _context.Books.Add(book);
        }
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

        //lọc ra những cuốn sách đã đọc
        public Book[] SelectMarked()
        {
            return _context.Books.Where(b => b.Reading == true).ToArray();
        }

        public void Clear()
        {
            _context.Books.Clear();
        }

        public IEnumerable<IGrouping<string, Book>> Stats(string key = "folder")
        {

            //GroupBy nhóm theo yêu cầu
            //yêu cầu ở đây là tên thư mục
            //gom các file của một thư mục vào một nhóm
            //mỗi nhóm sẽ có tên là tên thư  mục
            //mỗi phân tử là all các cuốn sách in thư mục đó

            return _context.Books.GroupBy(b => System.IO.Path.GetDirectoryName(b.File));
        }

        //các hàm bên dưới cho mục đích sắp xếp
        //hàm chuyển đổi hai đối tướng trong mảng
        public void Swap(Book[] book, int a, int b)
        {
            var temp = book[a];
            book[a] = book[b];
            book[b] = temp;
        }

        //hàm so sánh với lựa chọn so sánh từ người dùng
        public bool Compare(Book book1, Book book2, string optionSort)
        {
            switch (optionSort)
            {
                case "author": return string.Compare(book1.Author, book2.Author) <= 0;
                case "title": return string.Compare(book1.Title, book2.Title) <= 0;
                case "publisher": return string.Compare(book1.Publisher, book2.Publisher) <= 0;
                case "year": return book1.Year <= book2.Year;
                default: return false;
            }
        }

        //hai hàm dưới là QickSort
        public int Partition(Book[] book, int low, int high, string optionSort)
        {
            int i = low;
            var pivot = book[high];
            for (int j = low; j < high; j++)
            {

                if (Compare(book[j], pivot, optionSort))
                {
                    Swap(book, i, j);
                    i++;
                }

            }
            Swap(book, i, high);
            return i;

        }

        public void Sort(Book[] book, int low, int high, string optionSort)
        {

            if (book.Length == 1)
                return;
            if (low < high)
            {
                int pi = Partition(book, low, high, optionSort);

                Sort(book, low, pi - 1, optionSort);
                Sort(book, pi + 1, high, optionSort);
            }

        }

        
    }
}
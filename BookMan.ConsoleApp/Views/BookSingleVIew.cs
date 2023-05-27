using static System.Console;

namespace BookMan.ConsoleApp.Views
{
    using Models;
    using FrameWork;

    /// <summary>
    /// class hiển thị một cuốn cách
    /// </summary>

    /*cách sử dụng kế thừa với viewBase khi không có viewBase<T>
    class BookSingleView : ViewBase
    {

        //chỉ định hàm tạo của viewBase
        public BookSingleView(Book model) : base(model) {}

        /// <summary>
        /// hàm hiện thị thông tin 1 cuốn sách
        /// </summary>
        public override void Render()
        {
            //kiểm tra xem Model có dữ liệu không
            if (Model == null) 
            {
                ViewHelp.WriteLine("NO BOOK FOUND/. SORRY!", ConsoleColor.Red);
                return;
            }


            ViewHelp.WriteLine("BOOK DETAIL INFORMATION", ConsoleColor.Green);

            //chuyển đổi từ obj về book, chỉ áp dụng với kiểu class
            var model = Model as Book;
            WriteLine($"Author:         {model.Author}");
            WriteLine($"Title:          {model.Title}");
            WriteLine($"Publisher:      {model.Publisher}");
            WriteLine($"Year:           {model.Year}");
            WriteLine($"Edition:        {model.Edition}");
            WriteLine($"Isbn:           {model.Isbn}");
            WriteLine($"Tags:           {model.Tags}");
            WriteLine($"Description:    {model.Description}");
            WriteLine($"Rating:         {model.Rating}");
            WriteLine($"Reading:        {model.Reading}");
            WriteLine($"FIle:           {model.File}");
            WriteLine($"NameFile:       {model.FileName}");

        }


    } */

    //cách sử dụng kế thừa kết hợp với generic
    class BookSingleView : ViewBase<Book>
    {

        //chỉ định hàm tạo của viewBase
        public BookSingleView(Book model) : base(model) { }

        /// <summary>
        /// hàm hiện thị thông tin 1 cuốn sách
        /// </summary>
        public override void Render()
        {
            //kiểm tra xem Model có dữ liệu không
            if (Model == null)
            {
                ViewHelp.WriteLine("NO BOOK FOUND/. SORRY!", ConsoleColor.Red);
                return;
            }


            ViewHelp.WriteLine("BOOK DETAIL INFORMATION", ConsoleColor.Green);

            WriteLine($"Author:         {Model.Author}");
            WriteLine($"Title:          {Model.Title}");
            WriteLine($"Publisher:      {Model.Publisher}");
            WriteLine($"Year:           {Model.Year}");
            WriteLine($"Edition:        {Model.Edition}");
            WriteLine($"Isbn:           {Model.Isbn}");
            WriteLine($"Tags:           {Model.Tags}");
            WriteLine($"Description:    {Model.Description}");
            WriteLine($"Rating:         {Model.Rating}");
            WriteLine($"Reading:        {Model.Reading}");
            WriteLine($"FIle:           {Model.File}");
            WriteLine($"NameFile:       {Model.FileName}");

        }


    }
}

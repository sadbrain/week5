using static System.Console;

namespace BookMan.ConsoleApp.Views
{
    using Models;
    using FrameWork;

    /*
     * cách sử dụng kế thừa với viewBase không dùng generic
    class BookUpdateView : ViewBase
    {
        //chỉ định hàm tạo của viewBase

        public BookUpdateView(Book model) : base(model) { }  

        public override void Render()
        {
            ViewHelp.WriteLine("UPDATE BOOK INFORMATION", ConsoleColor.Green);
            // chuyển đổi từ kiểu obj về Book
            var model = Model as Book;

            //method nhập chuổi và tra về giá trị gốc nếu chuổi bạn nhập rổng, trả về chuổi bạn nhập
            var author = ViewHelp.InputString("Author", model.Author);  
            var title =  ViewHelp.InputString("Title", model.Title);
            var publisher = ViewHelp.InputString("Publisher", model.Publisher);
            var isbn = ViewHelp.InputString("Isbn", model.Isbn);
            var description = ViewHelp.InputString("Description", model.Description);
            var tags = ViewHelp.InputString("Tags", model.Tags);
            var file = ViewHelp.InputString("File", model.File);
            var year = ViewHelp.InputInt("Year", model.Year);
            var rating = ViewHelp.InputInt("Rate", model.Rating);
            var edition = ViewHelp.InputInt("Edition", model.Edition);
            var reading = ViewHelp.InputBool("Reading", model.Reading);
        }

    }*/
    
    //cách dùng kế thừa với generic
    class BookUpdateView : ViewBase<Book>
    {
        //chỉ định hàm tạo của viewBase

        public BookUpdateView(Book model) : base(model) { }

        public override void Render()
        {
            ViewHelp.WriteLine("UPDATE BOOK INFORMATION", ConsoleColor.Green);

            //method nhập chuổi và tra về giá trị gốc nếu chuổi bạn nhập rổng, trả về chuổi bạn nhập
            var author = ViewHelp.InputString("Author", Model.Author);
            var title = ViewHelp.InputString("Title", Model.Title);
            var publisher = ViewHelp.InputString("Publisher", Model.Publisher);
            var isbn = ViewHelp.InputString("Isbn", Model.Isbn);
            var description = ViewHelp.InputString("Description", Model.Description);
            var tags = ViewHelp.InputString("Tags", Model.Tags);
            var file = ViewHelp.InputString("File", Model.File);
            var year = ViewHelp.InputInt("Year", Model.Year);
            var rating = ViewHelp.InputInt("Rate", Model.Rating);
            var edition = ViewHelp.InputInt("Edition", Model.Edition);
            var reading = ViewHelp.InputBool("Reading", Model.Reading);

            var request = "do update ? " +
                        $"id = {Model.Id}" +
                        $" & title = {title}" +
                        $" & author = {author}" +
                        $" & publisher = {publisher}" +
                        $" & year = {year} &" +
                        $" & edition = {edition}" +
                        $" & tags = {tags}" +
                        $" & description = {description}" +
                        $" & rate = {rating}" +
                        $" & reading = {reading}" +
                        $" & file = {file}";

            Router.Forward(request);
        }

    }
}

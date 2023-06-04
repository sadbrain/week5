
namespace BookMan.ConsoleApp.Views
{
    using FrameWork;
    using Models;

    /// <summary
    //class display list book
    /// </summary>
    /*cách sử dụng kế thừa với viewBase khi không có viewBase<T>
     * internal class BookListView : ViewBase
    {
        //chỉ định hàm tạo của viewBase

        public BookListView(Book[] model) : base(model) { }

        public virtual void Render()
        {
            /// check array co rong khong, rong thi hien thong bao , khong thi lap va in ra

            // cách ép kiểu object về một kiểu tham chiếu nhưng nếu không ép được  nó sẽ gây lỗi
            if (((Book[])Model).Length == 0)
            {
                ViewHelp.WriteLine("No book found!", ConsoleColor.Yellow);
                return;
            }
            ViewHelp.WriteLine("THE BOOK LIST", ConsoleColor.Green);
            foreach (Book b in Model as Book[]) //ép kiểu nhưng không gây lỗi
            {
                ViewHelp.Write($"[{b.Id}]", ConsoleColor.Yellow);
                ViewHelp.WriteLine($" {b.Title}", b.Reading ? ConsoleColor.Cyan : ConsoleColor.White);
            }


        }

        
    } */
    
    //cách sử dụng kế thừa với generic
    internal class BookListView : ViewBase<Book[]>
    {
        //chỉ định hàm tạo của viewBase

        public BookListView(Book[] model) : base(model) { }

        public override void Render()   
        {

            if (Model.Length == 0)
            {
                ViewHelp.WriteLine("No book found!", ConsoleColor.Yellow);
                return;
            }
            ViewHelp.WriteLine("THE BOOK LIST", ConsoleColor.Green);
            foreach (Book b in Model) 
            {
                ViewHelp.Write($"[{b.Id}]", ConsoleColor.Yellow);
                ViewHelp.WriteLine($" {b.Title}", b.Reading ? ConsoleColor.Cyan : ConsoleColor.White);
            }

            ViewHelp.WriteLine($"{Model.Length} item(s)", ConsoleColor.Green);
        }


    }
}

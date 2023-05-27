
using static System.Console;

namespace BookMan.ConsoleApp.Views
{
    using FrameWork;
    /// <summary>
    /// class tạo ra một cuốn sách mới
    /// </summary>
    internal class BookCreateView : ViewBase
    {
        
        public BookCreateView() { }
        /// <summary>
        /// hàm tạo cuốn sách mới với những thông tin
        /// title, authors, publisherm year, edition, reading
        /// tags, description, rate, file
        /// </summary>
        public override void Render()
        {
            WriteLine("CREATE A NEW BOOK", ConsoleColor.Green);
            string title = ViewHelp.InputString("Title");
            string authors = ViewHelp.InputString("Authors");
            string publisher = ViewHelp.InputString("Publisher");
            int year =  ViewHelp.InputInt("Year");
            int edition = ViewHelp.InputInt("Edition");
            var tags = ViewHelp.InputString("Tags");
            var description = ViewHelp.InputString("Description");
            var rating = ViewHelp.InputInt("Rate");
            bool reading = ViewHelp.InputBool("Reading");
            var file = ViewHelp.InputString("File");
        }
        




    }
}

using BookMan.ConsoleApp.FrameWork;
using BookMan.ConsoleApp.Models;


namespace BookMan.ConsoleApp.Views
{
    internal class BookStatsView : ViewBase<IEnumerable<IGrouping<string, Book>>>
    {
        public BookStatsView(IEnumerable<IGrouping<string, Book>> model) : base(model)
        {
        }
        public override void Render()
        {
            //vòng lập 1 duyệt danh sách nhóm
            foreach (var g in Model)
            {
                
                ViewHelp.WriteLine($"# {g.Key}", ConsoleColor.Magenta);
                //vòng lập thứ 2 duyệt danh sách phần tử trong mỗi nhóm, đây là những cuốn sách.
                foreach (var b in g)
                {
                    ViewHelp.Write($"[{b.Id}] ", ConsoleColor.Yellow);
                    ViewHelp.WriteLine(b.Title, b.Reading ? ConsoleColor.Cyan : ConsoleColor.White);
                }
            }
        }
    }
}
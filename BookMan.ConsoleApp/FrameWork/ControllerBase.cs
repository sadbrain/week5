

namespace BookMan.ConsoleApp.FrameWork
{
    public class ControllerBase
    {
        //kêt hợp giữa đa hình và kế thừa, biến kiểu cha có thể tham chiếu tới mọi đội tượng của kiểu con
        //view.Render là những hàm Render của class con tương ứng, vì đã sử dụng cơ chế ghi đè
        public virtual void Render(ViewBase view) => view.Render();
        public virtual void Render<T>(ViewBase<T> view, string path = "", bool both = false) 
        {
            if (string.IsNullOrEmpty(path)) { view.Render(); return; }
            if (both) 
            { 
                view.Render();
                view.RenderToFile(path); 
                return; 
            }
            view.RenderToFile(path);
        }
    }
}

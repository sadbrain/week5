

namespace BookMan.ConsoleApp.FrameWork
{
    //hổ trợ cho controller tốt hơn
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


        //hổ trợ việc hiển thị thông báo
        //
        public virtual void Render(Message message) => Render(new MessageView(message));
        public virtual void Success(string text, string label = "SUCCESS") => Render(new Message { Type = MessageType.Success, Text = text, Label = label });
        public virtual void Inform(string text, string label = "INFORMATION") => Render(new Message { Type = MessageType.Information, Text = text, Label = label });
        public virtual void Error(string text, string label = "ERROR!") => Render(new Message { Type = MessageType.Error, Text = text, Label = label });
        public virtual void Confirm(string text, string route, string label = "CONFIRMATION") => Render(new Message { Type = MessageType.Confirmation, Text = text, Label = label, BackRoute = route });
    }
}

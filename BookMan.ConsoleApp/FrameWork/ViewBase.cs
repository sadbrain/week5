

namespace BookMan.ConsoleApp.FrameWork
{
    public class ViewBase
    {
        //giúp các giao diện có thể sử dụng đến các phương thức controller
        protected Router Router = Router.Instance;

        public ViewBase() { }
        

        public virtual void Render() { }

        



    }
    //kết hợp kế thừa cùng generic
    //tập trung những phương thức sử dụng dữ liệu từ model
    //kiểu dữ liệu của model sẽ phụ thuộc khi các lớp con kế thừa ViewBase
    public class ViewBase<T> : ViewBase
    {
        protected T Model;
        public ViewBase(T model) => Model = model;

        //xuất dữ liệu ra file
        public virtual void RenderToFile(string path)
        {
            ViewHelp.WriteLine($"Saving data to file '{path}'");
            //chuyển dữ liệu object sang json
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Model);

            //tạo ra file mới có đường dẫn path, và có dữ liệu là chuỗi json,
            //nếu đã có thì sẽ ghi đè
            System.IO.File.WriteAllText(path, json);
            ViewHelp.WriteLine("Done!");

        }
    }

}

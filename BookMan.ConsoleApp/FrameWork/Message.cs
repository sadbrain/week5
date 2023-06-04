using System;

//hai class dưới có nhuieemh vụ gữi thông báo từ Controller tới người dùng
//update thành công hay thất bại, delete phải hỏi ý kiến người dùng

namespace BookMan.ConsoleApp.FrameWork
{
    /// <summary>
    /// enum các kiểu của message
    /// </summary>
    public enum MessageType { Success, Information,  Error, Confirmation}
    public class Message
    {
        //kiểu message
        public MessageType Type { get; set; } = MessageType.Success;

        public string Label { get; set; }
        //nội dung in
        public string Text { get; set; } = "Your action has completed succesfully";
        //hàm thực hiện khi đã đồng ý 
        public string BackRoute { get; set; }


    }

    //
    public class MessageView : ViewBase<Message>
    {
        public MessageView(Message model) : base(model) { } 
        public override void Render()
        {
            //check model.type thuộc kiểu nào và in ra label tương ứng, không có label in ra dòng mặt định
            switch (Model.Type)
            {
                case MessageType.Success:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "SUCCESS", ConsoleColor.Green);
                    break;
                case MessageType.Error:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "ERROR!", ConsoleColor.Red);
                    break;
                case MessageType.Information:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "INFORMATION!", ConsoleColor.Yellow);
                    break;
                case MessageType.Confirmation:
                    ViewHelp.WriteLine(Model.Label != null ? Model.Label.ToUpper() : "CONFIRMATION", ConsoleColor.Cyan);
                    break;

            }


            //in ra dòng text với những type không phải Confirmation
            if (!(Model.Type == MessageType.Confirmation)) ViewHelp.WriteLine(Model.Text);
            else
            {
                //in ra dòng text
                ViewHelp.WriteLine(Model.Text, ConsoleColor.Magenta);
                //nhận xác nhận của người dùng
                var answer = Console.ReadLine().ToLower();
                //check xác nhận của người dùng và thực hiện route tương ứng như delete
                if (answer == "yes" || answer == "y") Router.Forward(Model.BackRoute);
            }
        }
    }
}

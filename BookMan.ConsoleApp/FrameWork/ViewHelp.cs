
using static System.Console;


namespace BookMan.ConsoleApp.FrameWork
{
    
    public static class ViewHelp
    {

        /// <summary>
        /// xuất ra thông tin với màu sắc
        /// </summary>
        /// <param name="message"> thông tin cần xuất</param>
        /// <param name="color"></param>
        /// <param name="resetColor"> trả lại màu mặc định hay không</param>
        public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White, bool resetColor = true)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            if (resetColor) ResetColor();
        }

        public static void Write(string message, ConsoleColor color = ConsoleColor.White, bool resetColor = true)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            if (resetColor) ResetColor();
        }

        /// <summary>
        /// in ra thông báo và tiếp nhận chuỗi ký tự người dùng nhập
        /// rồi chuyển sang kiểu bool
        /// </summary>
        /// <param name="label">dòng thông báo</param>
        /// <param name="labelColor">màu chữ thông báo</param>
        /// <param name="valueColor">màu chữ người dùng nhập</param>
        /// <returns></returns>
        public static bool InputBool(string label, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{label} [y/n]: ", labelColor);
            ConsoleKeyInfo key = ReadKey();
            Console.WriteLine();
            bool @char = key.KeyChar == 'Y' || key.KeyChar == 'y' ? true : false;

            return @char;
        }

        /// <summary>
        /// cập nhật giá trị kiể bool, nhấn enter trả về giá trị cũ
        /// </summary>
        /// <param name="label"></param>
        /// <param name="oldValue"></param>
        /// <param name="labelColor"></param>
        /// <param name="valueColor"></param>
        /// <returns></returns>

        public static bool InputBool(string label, bool oldValue, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{label}: ", labelColor);
            //sử dụng phương thức mở rộng ToString
            WriteLine(oldValue.ToString("y/n"), ConsoleColor.Yellow);
            Write("New value >> ", ConsoleColor.Green);
            Console.ForegroundColor = valueColor;

            string str = ReadLine();
            if (string.IsNullOrEmpty(str)) return oldValue;
            return str.ToBool(); //sử dụng phương thức mở rộng ToBool
        }


        /// <summary>
        /// in ra thông báo và tiếp nhận chuỗi ký tự người dùng nhập
        /// rồi chuyển sang số nguyên
        /// </summary>
        /// <param name="label">dòng thông báo</param>
        /// <param name="labelColor">màu chữ thông báo</param>
        /// <param name="valueColor">màu chữ người dùng nhập</param>
        /// <returns></returns>
        public static int InputInt(string label, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {

            while (true)
            {
                var str = InputString(label);
                var result = int.TryParse(str, out int i);

                if (result) return i;
            }
        }

        /// <summary>
        ///  cập nhật giá trị kiểu int Nếu ấn enter mà không nhập dữ liệu sẽ trả lại giá trị cũ.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="oldValue"></param>
        /// <param name="labelColor"></param>
        /// <param name="valueColor"></param>
        /// <returns></returns>
        public static int InputInt (string label, int oldValue, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            //fomat update, 
            Write($"{label}: ", labelColor);
            WriteLine($"{oldValue}", ConsoleColor.Yellow);
            Write("New value >> ", ConsoleColor.Green);
            ForegroundColor = valueColor;

            string str = ReadLine();
            if (string.IsNullOrEmpty(str)) return oldValue;

            //chuyển sang số nguyên bằng extension ToInt
            if (str.ToInt(out int i)) return i;
            return oldValue;

        }
        /// <summary>
        /// in ra thông báo và tiếp nhận chuỗi ký tự người dùng nhập
        /// </summary>
        /// <param name="label">dòng thông báo</param>
        /// <param name="labelColor">màu chữ thông báo</param>
        /// <param name="valueColor">màu chữ người dùng nhập</param>
        /// <returns></returns>
        /// 

        public static string InputString(string label, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{label}: ", labelColor, true);
            ForegroundColor = valueColor;
            string value = ReadLine();
            ResetColor();
            return value;
        }

        /// <summary>
        /// cập nhật giá trị kiểu string. Nếu ấn enter mà không nhập dữ liệu sẽ trả lại giá trị cũ.
        /// </summary>
        /// <param name="label">dòng thông báo</param>
        /// <param name="oldValue">giá trị gốc</param>
        /// <param name="labelColor">màu chữ thông báo</param>
        /// <param name="valueColor">màu chữ dữ liệu</param>
        /// <returns></returns>
        public static string InputString(string label, string oldValue, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{label}: ", labelColor);
            WriteLine(oldValue, ConsoleColor.Yellow);
            Write("New value >> ", ConsoleColor.Green);
            ForegroundColor = valueColor;
            string newValue = ReadLine();
            return string.IsNullOrEmpty(newValue) ? oldValue : newValue;

        }
    }
}

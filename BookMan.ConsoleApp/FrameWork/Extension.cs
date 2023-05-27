

namespace BookMan.ConsoleApp.FrameWork
{
    /// <summary>
    /// một số phương thức mở rộng biến đổi kiểu dữ liệu
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// biển đổi từ chuổi sang số nguyên
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value) => int.Parse(value);

        /// <summary>
        /// biển đổi từ chuổi sang số nguyên
        /// </summary>
        /// <param name="value"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ToInt (this string value , out int i) => int.TryParse(value, out i);
        

        /// <summary>
        /// biến đổi true True y Y sang true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool (this string value)
        {
            var v = value.ToLower();
            if (v == "true" || v == "y") return true;
            return false;
        }

        /// <summary>
        /// biến đổi true false thành yes no, có không
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString (this bool value, string format)  
        {
            if (format == "y/n") return value ? "Yes" : "No";
            if (format == "c/k") return value ? "Có" : "Không";
            return value ? "True" : "False";
        }
    }
}

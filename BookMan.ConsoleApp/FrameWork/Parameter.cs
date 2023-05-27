

namespace BookMan.ConsoleApp.FrameWork
{
    /// <summary>
    /// lưu các cặp khóa-giá trị người dùng nhập
    /// chuỗi tham số cần viết key = value;
    /// nếu có nhiều tham số cần viết &
    /// </summary>
    public class Parameter
    {
        
        private readonly Dictionary<string, string> _pairs = new Dictionary<string, string>();

        /// <summary>
        /// nạp chồng phép toán indexing []; cho phép truy xuất giá trị theo kiểu biến[khóa] = giá_trị;
        /// </summary>
        /// <param name="key">khóa</param>
        /// <returns>giá trị tương ứng</returns>
        public string this [string key]
        {
            get
            {//kiểm tra key có hợp lệ không
                if (_pairs.ContainsKey(key))
                    return _pairs[key];
                else return null;
            }
            set => _pairs[key] = value;
        }
        /// <summary>
        /// Kiểm tra xem một khóa có trong danh sách tham số không
        /// </summary>
        /// <param name="key">khóa cần kiểm tra</param>
        /// <returns></returns>
        public bool ContainsKey (string key)
        {
            return _pairs.ContainsKey(key);
        }

        // hàm tạo nhận phần parameter để phân tích ra key và value
        public Parameter (string parameter)
        {
            //cắt chuổi theo móc ký tự &
            //trả về một mảng, mỗi phần tử là một chuổi có dạng key = value;
            var pairs = parameter.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                var p = pair.Split("="); //cắt value và key theo móc là =

                if (p.Length == 2 ) //thường khi cắt sẽ được 2 phần tử
                {
                    var key = p[0].Trim();
                    var value = p[1].Trim();
                    this[key] = value; // _pairs[key] = value, lưu cặp key value 
                }
            }
        }
            
    }
}

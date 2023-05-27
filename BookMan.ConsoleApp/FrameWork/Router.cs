using System.Text;

namespace BookMan.ConsoleApp.FrameWork
{
    /*tạo biệt danh cho một kiểu dữ liệu
     * khai báo này nằm trực tiếp ở namespace */
    //chưa khóa là những route, value delegate tham chiếu tới những cái hàm
    using RoutingTable = Dictionary<string, ControllerAction>;
    /// <summary>
    /// delegate đại diện tât cả phương thức có mô tả (Parameter) => void
    /// </summary>
    /// <param name="parameter"></param>
    public delegate void ControllerAction(Parameter parameter = null);
    
    /// <summary>
    /// lớp cho phép ánh xạ với phương thức
    /// </summary>
    /// lớp gồm hai biên _helpTable và _routingTable

    public class Router
    {
        private readonly RoutingTable _routingTable;
        //chứa key là những router, value là thông tin về route đó
        private readonly Dictionary<string, string> _helpTable;
        //nhóm 3 lệnh dưới biến router thành một singleton
        private static Router _instance;
        private Router ()
        {
            _routingTable = new RoutingTable();
            _helpTable = new Dictionary<string, string> ();
        }
        // constructor là private
        //người sử dụng class thông qua property để truy xuất phương thức của class
        // chỉ khi nào _instance == null mới tạo object. Một khi đã tạo object, _instance sẽ
        // không có giá trị null nữa.
        // vì là biến static, _instance một khi được khởi tạo sẽ tồn tại suốt chương trình
        public static Router Instance => _instance ?? (_instance = new Router ());
        
        public string GetRoutes ()
        {

            //tuyệt vời hơn string ở vấn đề hiệu suất, không tạo ra đối tượng mới khi gán lại giá trị cho biến
            StringBuilder stringBuilder = new StringBuilder ();
            foreach (var k in _routingTable.Keys)
            {
                
                stringBuilder.AppendFormat("{0}, ", k);
            }
            return stringBuilder.ToString();
        }

        
        public string GetHelp (string key)
        {
            //kiểm tra key có trong danh sách _helptable không, có thì trả về giá trị của key đó
            if (_helpTable.ContainsKey(key)) return _helpTable[key];
            else return "DOCUMENTATION NOT READY YET!";
        }

        /// <summary>
        /// đắng ký một router mới, mỗi router ánh xạ đến một phương thức
        /// </summary>
        /// <param name="Router"></param>
        /// <param name="action"></param>
        /// <param name="help"></param>
        public void Register(string route, ControllerAction action, string help = "")
        {
            //kiểm tra route có trong danh sách _routingTable, có thì tiến hành đăng ký
            if (!_routingTable.ContainsKey(route))
            {
                _routingTable[route] = action;
                _helpTable[route] = help;   
            }
        }
        /// <summary>
        /// phân tích truy vấn và gọi phương thức tương ứng với chuỗi truy vấn
        /// <para>chuỗi truy vấn bao gồm hai phần: route và parameter, phân tách bởi ký tự ?</para>
        /// </summary>
        /// <param name="request">chuỗi truy vấn, bao gồm hai phần: 
        /// route, paramete; phân tách bởi ký tự ?</param>
        public void Forward (string request)
        {
            var req = new Request(request);
            //kiểm tra tra thử route người dùng nhập có hợp lệ không
            if (!_routingTable.ContainsKey(req.Route))
                throw new Exception("Command not found!");
            if (req.Parameter == null)
                //lấy action tương ứng với route và gọi 
                _routingTable[req.Route]?.Invoke();
            else
                _routingTable[req.Route]?.Invoke(req.Parameter);

        }

        //Trong lớp Router có 3 loại logic tương đối độc lập: phân tích truy vấn, xử lý chuỗi tham số, đăng ký lệnh và gọi phương thức.
        //tách class request thành lớp nội bộ
        //phân tách yêu cầu thành hai phần Route và Parameter
        private class Request
        {
            /// <summary>
            /// thành phần lệnh của truy vấn
            /// </summary>
            public string Route { get; private set; }
            /// <summary>
            /// thành phần tham số của truy vấn
            /// </summary>
            public Parameter Parameter { get; private set; }

            // gọi hàm tạo tiến hành phân tích request thành hai phần
            public Request(string request)
            {
                Analyze(request);
            }

            private void Analyze(string request)
            {
                // tìm xem trong chuổi truy vấn có tham số hay không
                var firstIndex = request.IndexOf("?");

                // nếu chuỗi không có tham số 
                if (firstIndex < 0) Route = request.ToLower().Trim();
                else
                {
                    //nếu chuỗi lỗi (chỉ chứa tham số, không có router) 
                    if (firstIndex <= 1) throw new Exception("INVALID REQUEST PARAMETER");

                    //cắt chuổi truy vấn lấy mốc ký tử ?
                    //trả về mảng gồm router và parameters
                    var tokens = request.Split(new char[] { '?' }, 2, StringSplitOptions.RemoveEmptyEntries);

                    //lấy phần router
                    Route = tokens[0].ToLower().Trim();
                    //lấy phần parameter bằng hàm SubString cắt chuỗi từ vị trí chỉ định đến hểt
                    //xử lý tham số này bằng cách khởi tạo đối tượng cảu class Parameter
                    var parameterPart = request.Substring(firstIndex + 1).Trim(); // tokens[2].Trim
                    // tiến hành phân tích tham số bằng cách gọi đối tượng Parameter
                    Parameter = new Parameter(parameterPart);
                }
            }
        }
    }
}

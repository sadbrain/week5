
using System.Xml;
using System.Xml.Serialization;
namespace BookMan.ConsoleApp.DataServices
{
    using Models;
    /// <summary>
    /// lưu trử dữ liệu ở file xml
    /// </summary>
    public class XmlDataAccess  : IDataAccess
    {
        public List<Book> Books { get; set; } = new List<Book>();
        private readonly string _file = "data.xml";
        public void Load()
        {
            //kiểm tra file có tồn tại không, khoogn thi tạo file và tiên hành đưa dữ liệu vào file
            if (!File.Exists(_file))
            {

                SaveChanges();
                return;
            }

            // khởi tạo đối tượng của xmlSerializer để có thể serialize or de serialize
            var serializer = new XmlSerializer(typeof(List<Book>));
            //mở file ra để đọc
            using (var reader = XmlReader.Create(_file))
            {
                //tiền hành lấy dữ từ file về object và tiến hành ep kiểu
                Books = (List<Book>) serializer.Deserialize(reader);
            }
        }
        public void SaveChanges()
        {
                
            var serializer = new XmlSerializer(typeof(List<Book>));
            //mở file ra đẻ ghi
            using (var writer = XmlWriter.Create(_file))
            {
                //tiền hành đưa dữ liệu vào file
                serializer.Serialize(writer, Books);
            }
        }
    }
}
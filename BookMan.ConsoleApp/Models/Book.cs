
namespace BookMan.ConsoleApp.Models
{
    /// <summary>
    /// lớp mô tả sách điện tử
    /// id, author, title, publisher, year, edition
    /// Isbn, tags, description, rating, file, fileName, reading
    /// </summary>
    
    [Serializable] //cho phép đối tượng từ class này serialization and deserialization
    public class Book
    {
        int _id = 1;
        public int Id 
        {
            get { return _id; } 
            set { if (value > 1) _id = value; }
        }

        string _author = "Unknown author";
        public string Author 
        {
            get { return _author; } 
            set { if (!string.IsNullOrEmpty(value)) _author = value;}
        }

        string _title = "A new book";
        public string Title
        {
            get { return _title; }
            set { if (!string.IsNullOrEmpty(_title)) _title = value; }
        }

        string _publisher = "Unknown publisher";
        public string Publisher 
        {
            get { return _publisher; }
            set { if (!string.IsNullOrEmpty(value)) _publisher = value; }
        }

        int _year = 2018;
        public int Year
        {
            get => _year;
            set { if (value >= 1950) _year = value; }
        }

        int _edition;
        public int Edition
        {
            get => _edition;
            set { if (value >= 1) _edition = value; }
        }

        public string Isbn { get; set; } = "";
        public string Tags { get; set; } = "";

        public string Description { get; set; } = "A new book";
        
        int _rating = 1;
        public int Rating
        {
            get => _rating;
            set { if (value >= 1) _rating = value; }
        }

        public bool Reading { get; set; }
        string _file;
        public string File
        {
            get => _file;
            set { if (System.IO.File.Exists(value)) _file = value; }
        }

        public string FileName
        {
            get => System.IO.Path.GetFileName(_file);
        }
    }
    
}


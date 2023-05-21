using System;
using static System.Console;

namespace PatternMatch
{
    class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
    class Cirle
    {
        public int Radius { get; set; }
    }
    class Square
    {
        public int Length { get; set; }
    }
    static class Area
    {
        public static double GetArea(object shape)
        {

            //câu lệnh tương đương với var r = shape as rectangle
            //if (r != null) return r.Width * r.Height;
            //vừa chuyển như as và kiểm tra có null ko
            if (shape is Rectangle r) return r.Width * r.Height;
            if (shape is Cirle c) return c.Radius * c.Radius * 3.14;
            if (shape is Square s) return s.Length * 4;
            return Double.NaN;
        }

        public static double GetAreaWithSwitchCase(object shape)
        {

            switch (shape)
            {
                case Rectangle r: return r.Width * r.Height;
                //từ khóa when đặt thêm điều kiện bổ sung
                case Cirle c when c.Radius > 10 && c.Radius < 100: return Math.PI * c.Radius * c.Radius;
                case Square s when s.Length > 10 && s.Length < 100: return s.Length * s.Length;
                default: return double.NaN;
            }

        }


        public static double GetAreaWithSwitchExpression(object shape)
        {
            // như switch case nhưng nó trả về giá trị cho area, biến kiểm tra là shape
            var area = shape switch
            {
                //
                Rectangle r => r.Width * r.Height,
                Cirle c when c.Radius > 10 && c.Radius < 100 => c.Radius * c.Radius * 3.14,
                Square s => s.Length * s.Length,
                _ => double.NaN // như default in switch case
            };

            return area;
        }


    }

    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //so khớp kiểu với Rectangle, Cirle, Square, Area 
            var r = new Rectangle { Height = 100, Width = 90 };
            var c = new Cirle { Radius = 5 };
            WriteLine(Area.GetArea(r));
            WriteLine(Area.GetAreaWithSwitchCase(c)); // output ra Nan vì Radius = 5 < 10

            WriteLine();
            WriteLine("switch expression");
            WriteLine(Area.GetAreaWithSwitchExpression(r));

            //so khớp biến
            WriteLine();
            WriteLine("so khớp biến");
            WriteLine(GreetingSwitch(""));

            WriteLine();
            WriteLine("property pattern");
            var point = new Point();
            WriteLine(Position(point));
        }
        //so Khớp biến
        private static string GreetingSwitch(string name)
        {
            switch (name)
            {
                //giá trị của name được truyền vào từng case và gán cho n, và được biến đổi kiểm tra sau when
                case var n when n.ToLower().Contains("putin"): return "Privet Vova!";
                case string n when n.ToLower().Contains("trump"): return "Hello, Mr. president";
                case var n when n.Trim() == "": return "Sorry, who are you?";
                default: return $"Hi, {name}";
            }
        }

        //Property pattern
        private static string Position(Point point)
        {
            //đặc điểm nhận dạng của mỗi pattern chính là danh sách giá trị của các public property của object.
            return point switch
            {
                { X: 0, Y: 0 } => "At the origin",
                { X: _, Y: 0 } => "On the X axis",
                { X: 0, Y: _ } => "On the Y axis",
                { X: var x, Y: var y } => $"({x} {y})",
                _ => "Somewhere"
            };
        }

        private static string PositionWithTuplePattern(int x, int y)
        {
            //sử dụng tuple pattern
            return (x, y) switch
            {
                (0, 0) => "At the origin",
                (_, 0) => "On the X axis",
                (0, _) => "On the Y axis",
                (var a, var b) => $"({a} {b})",

            };


        }
    }
}





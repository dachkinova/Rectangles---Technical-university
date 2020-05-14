using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace RectanglesDrawing
{
    class InvalidValueException : Exception
    {
        public InvalidValueException(string message) : base(message)
        {

        }
    }
    [Serializable]
    class Rectangle
    {
        private int _width;
        [NonSerialized]
        private Color _color;

        public bool Selected
        { get; set; }
        public Point Location
        { get; set; }
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidValueException("Invalid width");
                }
                _width = value;
            }
        }
        public int Height
        { get; set; }
        public Color Color
        {
            get 
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        
        
        public bool Border { get; set; }

        public Rectangle()
        {
            Border = true;
        }
        public int Area
        {
            get
            {
                return Width * Height;
            }
        }

        public static Rectangle operator-(Rectangle r1, Rectangle r2)
        {
            return r1.Intersection(r2);
        }
        public Rectangle Intersection(Rectangle rectangle)
        {
            var x1 = Math.Max(this.Location.X, rectangle.Location.X);
            var y1 = Math.Max(this.Location.Y, rectangle.Location.Y);
            var x2 = Math.Min(this.Location.X + this.Width, rectangle.Location.X + rectangle.Width);
            var y2 = Math.Min(this.Location.Y + this.Height, rectangle.Location.Y + rectangle.Height);
            return
           x2 - x1 < 0 || y2 - y1 < 0 ? null : new Rectangle
           {
               Location = new Point(x1, y1),
               Width = x2 - x1,
               Height = y2 - y1,
               Color = this.Order > rectangle.Order
               ? this.Color : rectangle.Color
           };
        }
        public int Order
        {
            get;
            set;
        }
        public void Paint(Graphics graphics)
        {
            var fillColor = (Color.FromArgb(
                Math.Min(byte.MaxValue, Color.R + 100),
                Math.Min(byte.MaxValue, Color.B + 100),
                Math.Min(byte.MaxValue, Color.G + 100)));
            using (var brush = new SolidBrush(fillColor))
            {
                graphics.FillRectangle(brush, Location.X, Location.Y, Width, Height);
            }
            using (var pen = new Pen(Border ? Color : fillColor, 3))
            { 
                graphics.DrawRectangle(pen, Location.X, Location.Y, Width, Height);
            }
        }

        public void PaintTransperent(Graphics graphics)
        {
            using (var brush = new SolidBrush(Color.FromArgb(
                Math.Min(byte.MaxValue, Color.R + 40),
                Math.Min(byte.MaxValue, Color.B + 88),
                Math.Min(byte.MaxValue, Color.G + 25))))
            {
                //graphics.FillRectangle(brush, Location.X, Location.Y, Width, Height);
            }
            using (var pen = new Pen(Color, 3))
            {
                graphics.DrawRectangle(pen, Location.X, Location.Y, Width, Height);
            }
        }
        public bool Contains(Point pt)
        {
            if (pt.X >= Location.X && pt.X <= Location.X + Width && pt.Y >= Location.Y && pt.Y <= Location.Y + Height)
            {
                return true;
            }
            return false;
        }
         
    }
}

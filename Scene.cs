using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RectanglesDrawing
{
    public partial class Scene : Form
    {
        private bool _tracingMouse = false;
        private bool _mouseSelecting = false;
        private Point mouseCurrent;
        private List<Rectangle> _rectangles = new List<Rectangle>();
        private Rectangle _selectedRectangle = null;
        private List<Rectangle> SelectedItems = new List<Rectangle>();


        public Scene()
        {
            InitializeComponent();

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }

        private void CalculateArea()
        {
            var area = _rectangles.Select(s => s.Area).Sum();


            for (int i = 0; i < _rectangles.Count() - 1; i++)
            {
                for (int k = i + 1; k < _rectangles.Count(); k++)
                {
                    var intersectionRectangle = _rectangles[i] - _rectangles[k];

                    if (intersectionRectangle == null)
                    {
                        continue;
                    }
                    else
                    {
                        area -= intersectionRectangle.Area;
                    }
                }
            }
            toolStripStatusLabelArea.Text = "Area:" + area;
        }

       
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (Rectangle rectangle in _rectangles)
            {
                rectangle.Paint(e.Graphics);
            }

            for (int i = 0; i < _rectangles.Count() - 1; i++)
            {
                for (int k = i + 1; k < _rectangles.Count(); k++)
                {
                    var intersectionRectangle = _rectangles[i] - _rectangles[k];

                    if (intersectionRectangle == null)
                    {
                        continue;
                    }
                    intersectionRectangle.Border = false;
                    intersectionRectangle.Paint(e.Graphics);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseSelecting = true;
                mouseCurrent = e.Location;

                if (_selectedRectangle != null)
                {
                    _selectedRectangle.Color = Color.Green;
                }
                if (SelectedItems.Count > 0)
                {
                    foreach (var rect in _rectangles)
                    {
                        rect.Color = Color.Green;
                    }
                    SelectedItems.Clear();

                }
                _selectedRectangle = _rectangles
                    .OrderByDescending(o => o.Order)
                    .Where(rectangle => rectangle.Contains(e.Location))
                    .FirstOrDefault();

                if (_selectedRectangle != null)
                {
                    _selectedRectangle.Color = Color.Red;
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                _tracingMouse = true;
                mouseCurrent = e.Location;
            }
            Invalidate();
        }

        private void Scene_MouseUp(object sender, MouseEventArgs e)
        {
            if (_tracingMouse && e.Button == MouseButtons.Right)
            {
                var width = Math.Abs(e.Location.X - mouseCurrent.X);
                var height = Math.Abs(e.Location.Y - mouseCurrent.Y);

                var x = Math.Min(e.Location.X, mouseCurrent.X);
                var y = Math.Min(e.Location.Y, mouseCurrent.Y);
                

                Rectangle rectangle = new Rectangle();

                rectangle.Location = new Point(x, y);
                rectangle.Width = width;
                rectangle.Height = height;
                rectangle.Color = Color.Red;
                rectangle.Order = _rectangles
                    .Select(s => s.Order)
                    .OrderBy(o => o)
                    .LastOrDefault() + 1;

                _rectangles.Add(rectangle);

                if (_selectedRectangle != null)
                    _selectedRectangle.Color = Color.Blue;

                _selectedRectangle = rectangle;
            }
            if (_mouseSelecting && e.Button == MouseButtons.Left)
            {
                foreach (var rect in _rectangles)
                {
                    if (rect.Color == Color.Red)
                    {
                        SelectedItems.Add(rect);
                    }
                }
            }
            CalculateArea();

            Invalidate();
            _tracingMouse = false;
            _mouseSelecting = false;


        }
        private void Scene_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_tracingMouse && !_mouseSelecting)
                return;
            else if (_tracingMouse)
            {
                var width = Math.Abs(e.Location.X - mouseCurrent.X);
                var height = Math.Abs(e.Location.Y - mouseCurrent.Y);

                if (width > 0 && height > 0)
                {

                    var x = Math.Min(e.Location.X, mouseCurrent.X);
                    var y = Math.Min(e.Location.Y, mouseCurrent.Y);



                    Rectangle rectangle = new Rectangle();
                    rectangle.Color = Color.Blue;
                    rectangle.Location = new Point(x, y);
                    rectangle.Width = width;
                    rectangle.Height = height;


                    Invalidate();
                    Application.DoEvents();

                    using (var graphics = CreateGraphics())
                    {
                        rectangle.Paint(graphics);
                    }
                }
            } else if (_mouseSelecting)
            {
                var width = Math.Abs(e.Location.X - mouseCurrent.X);
                var height = Math.Abs(e.Location.Y - mouseCurrent.Y);

                if (width > 0 && height > 0)
                {

                    var x = Math.Min(e.Location.X, mouseCurrent.X);
                    var y = Math.Min(e.Location.Y, mouseCurrent.Y);

                    Rectangle rectangle = new Rectangle();
                    rectangle.Color = Color.Black;
                    
                    rectangle.Location = new Point(x, y);
                    rectangle.Width = width;
                    rectangle.Height = height;

                    Invalidate();
                    Application.DoEvents();

                    using (var graphics = CreateGraphics())
                    {
                        rectangle.PaintTransperent(graphics);
                    }

                    if (_rectangles.Count > 0)
                    {
                        foreach (var newRectangle in _rectangles)
                        {
                            Point upRight = new Point(newRectangle.Location.X + newRectangle.Width, newRectangle.Location.Y);
                            Point downRight = new Point(newRectangle.Location.X + newRectangle.Width, newRectangle.Location.Y + newRectangle.Height);
                            Point downLeft = new Point(newRectangle.Location.X, newRectangle.Location.Y + newRectangle.Height);

                            if (rectangle.Contains(newRectangle.Location) || rectangle.Contains(upRight) ||
                                rectangle.Contains(downRight) || rectangle.Contains(downLeft))
                            {

                                newRectangle.Color = Color.Red;
                                SelectedItems.Add(newRectangle);
                               _selectedRectangle = newRectangle;
                            }
                        }
                    }
                }
                

            }
        }

        private void Delete(object sender, KeyPressEventArgs e)
        {

        }

        private void Scene_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete && _selectedRectangle != null && SelectedItems.Count == 0)
            {
                _rectangles.Remove(_selectedRectangle);
                _selectedRectangle = null;
                Invalidate();
            }
            else if (e.KeyCode == Keys.Delete && SelectedItems.Count > 0)
            {
                foreach (var rect in SelectedItems)
                {
                    _rectangles.Remove(rect);

                    CalculateArea();
                    Invalidate();
                
                }
                SelectedItems.Clear();
            }

        }

        private void Scene_Load(object sender, EventArgs e)
        {
            if (!File.Exists("rectangles"))
                return;

            var binformatter = new BinaryFormatter();

            using (var fileStream = new FileStream("rectangles", 
                FileMode.Open, FileAccess.Read))
            {
               _rectangles = (List<Rectangle>)binformatter.Deserialize(fileStream);
            }

            foreach(var rectangle in _rectangles)
            {
                rectangle.Color = Color.Blue;
            }
        }

        private void Scene_FormClosed(object sender, FormClosedEventArgs e)
        {
            var binformatter = new BinaryFormatter();

            using(var fileStream = new FileStream("rectangles", FileMode.Create, FileAccess.Write))
            {
                binformatter.Serialize(fileStream, _rectangles);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;

namespace MatrixPanel
{

   

    class Matrix
    {
        private Canvas _canvas;
        private int _width;
        private int _height;
        private bool _editmode;
        private VideoBuffer _vb;
        private SolidColorBrush[] _colorArr = new SolidColorBrush[2] { Brushes.LightGray, Brushes.Red };
        private int _charWidth;
        //private List<Dot> _dots = new List<Dot>();


        public bool EditMode { get => _editmode; set => _editmode = value; }
        public SolidColorBrush LedOffColor { get => _colorArr[0]; set => _colorArr[0] = value; }
        public SolidColorBrush LedOnColor { get => _colorArr[1]; set => _colorArr[1] = value; }
        public int CharWidth { set => _charWidth = value; }

        public Matrix(Canvas canvas, VideoBuffer vb, int char_width)
        {

            const int _CANVAS_PAD = 40;

            _canvas = canvas;
            _vb = vb;
            _width = vb.Width;
            _height = vb.Height;
            _charWidth = char_width;
            _vb.RefreshData += new EventHandler<EventArgs>(RefreshData);

            _canvas.Width = (17 * _width) + _CANVAS_PAD;

            Rectangle rect = new Rectangle();
            rect.Width  = _width - (_CANVAS_PAD * 2);
            rect.Height = (_height * 17) + 2;
            rect.Fill = Brushes.Black;
            //rect.StrokeThickness = 1;
            //rect.Stroke = Brushes.Black;
            Canvas.SetLeft(rect, _CANVAS_PAD);
            Canvas.SetTop(rect, _CANVAS_PAD);
            _canvas.Children.Add(rect);
            

            for (int x = 0; x < _width; x++)
            {
                if ( ((x + 1) %_charWidth) == 0)
                {
                    Line verLine = new Line();

                    verLine.StrokeThickness = 1;

                    verLine.Stroke = Brushes.Black;
                   // verLine.X1 = ((x + 1) / _charWidth) * 17 * _charWidth;
                   // verLine.Y1 = 0;
                   // verLine.X2 = ((x + 1) / _charWidth) * 17 * _charWidth;
                   // verLine.Y2 = 8 * 17;
                   // _canvas.Children.Add(verLine);

                }

                int colValue = _vb.GetCol(x);
                string hexValue = Convert.ToString(colValue, 16);

                TextBox tb = new TextBox();
                tb.Text = hexValue;
                tb.Width = 25;
                tb.LayoutTransform = new RotateTransform(270);
                
                Canvas.SetLeft(tb, (_CANVAS_PAD + x * 17));
                Canvas.SetTop(tb, 10);
                _canvas.Children.Add(tb);
                


                for (int y = 0; y < _height; y++)
                {
                    Ellipse el = new Ellipse();
                    _canvas.Children.Add(el);
                    el.Tag = (x * _height) + y;
                    Canvas.SetLeft(el, (_CANVAS_PAD + x * 17));
                    Canvas.SetTop(el, _CANVAS_PAD + y * 17);
                    el.Name = "el" + x.ToString() + "_" + y.ToString();
                    el.Height = 16;
                    el.Width = 16;
                    el.StrokeThickness = 0.0;
                    el.Fill = _colorArr[0];
                    el.Stroke = Brushes.Black;
                    el.MouseDown += new MouseButtonEventHandler(MouseDown);
                    el.MouseMove += new MouseEventHandler(MouseMove);

                }
            }
            RefreshData(null, null);
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            int state = 0;
            state |= e.LeftButton == MouseButtonState.Pressed ?  2 : 0;
            state |= e.RightButton == MouseButtonState.Pressed ? 1 : 0;

            if ( state > 0 )
            {
                if (sender is Ellipse)
                {
                    Ellipse el = (sender as Ellipse);
                    int index = (int)el.Tag;
                    _vb.SetBitOfDot(index, (state == 1));       // (state == 1) если нажата LeftButton
                    el.Fill = _colorArr[state-1];
                }
            }
        }

        private void RefreshData(object sender, EventArgs e)
        {
            // выбрали все элементы типа Ellipse
            IEnumerable<Ellipse> dots = _canvas.Children.OfType<Ellipse>();
            // тип IEnumerable преобразован в массив
            Ellipse[] arr = dots.ToArray();
            // количество элементов массива
            int dotsCount = arr.Count<Ellipse>();
            for (int i = 0; i < dotsCount - 1; i++)
            {
                int state = Convert.ToInt32(_vb.GetBitOfDot(i));
                SolidColorBrush scb = _colorArr[state];
                arr[i].Fill = scb;
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse)
            {
                Ellipse el = (sender as Ellipse);
                //int index = _canvas.Children.IndexOf(el);
                int index = (int)el.Tag;
                int state = Convert.ToInt32(_vb.InvBitOfDot(index));
                el.Fill = _colorArr[state];
            }



        }

    
    }
}

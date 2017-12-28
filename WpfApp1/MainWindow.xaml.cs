using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace MatrixPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int childI = 0;
        private int I = 0;
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Fonts fs = new Fonts();
            FontType ft = new FontType();
            ft.ReadFont("1");
            fs.Add(ft);
            VideoBuffer vb = new VideoBuffer(ft.ColCount, 8);

            for (int i=0; i< ft.ColCount; i++)
            {
                int col = ft.GetFontCol(i);
                vb.AddCol(col);
            }
            
            Matrix _m = new Matrix(matrixCanvas, vb, ft.CharWidth);
            
            
          

 
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show((sender as Ellipse).Name);
        }

        private void timerTick(object sender, EventArgs e)
        {
            

            //timer.Stop();
            // (matrixCanvas.Children[childI] as Ellipse).Fill = Brushes.Blue;
            
            //if (childI < matrixCanvas.Children.Count-1) {
            //    Button_Click_1(sender, null);
            //    childI++;
            //}
            
            
        }

        async private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //foreach(Ellipse el in matrixCanvas.Children)
            //{
            //    el.Fill = Brushes.Blue;
                await Task.Delay(10);
                


            //}
            
            
            
        }

      
    }
}


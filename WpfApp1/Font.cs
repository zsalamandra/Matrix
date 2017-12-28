using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MatrixPanel
{
    public class FontType
    {
        #region property
        public List<int> _fontBuffer = new List<int>();
        private string _name;
        private int _charWidth;
        private int _height;
        #endregion


        public string Name { get => _name; set => _name = value; }

        
        // количество столбцов
        public int ColCount
        {
            get
            {
                return _fontBuffer.Count;
            }
        }

        public int CharWidth { get => _charWidth; set => _charWidth = value; }


        // парсинг строки в котором представлен шрифт в формате Header файла Си
        // парсинг происходит на основе регулярных выражений
        private void AnalyseFile(String inputString)
        {
            MatchCollection _mc;
            // паттерн для регулярного выражения, разработано на сайте https://regex101.com/
            string HexPattern = "(?<=0x)[a-fA-F0-9]{2,4}";
            // _mc будет содержать количество всех совпадений
            _mc = Regex.Matches(inputString, HexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // цикл по всем совпадениям
            foreach (Match m in _mc)
            {
                // перевод хексовой строки в инт
                int intValue = int.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);
                _fontBuffer.Add(intValue);
            }

        }


        /**
         * Чтение фонта с файла переданного в качестве параметра
         */
        public void ReadFont(String FileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"d:\Программирование\Проекты\C#\LedMatrix\LedMatrix\LedMatrix\font_8x8.h"))
                {
                    // чтение всего файла в строку line
                    String line = sr.ReadToEnd();
                    // передача файла на анализ
                    AnalyseFile(line);
                }

                // Ширина знака пока установлено фиксированно
                // в дальнейшем необходимо сделать чтение с файла
                _charWidth = 8;
            }
            //реакция на ошибки связанные с чтением файла
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        public int GetFontCol(string source, int index)
        {
            byte[] indexesOfFont = Encoding.ASCII.GetBytes(source);
            int indexOfFont = indexesOfFont[index / CharWidth];
            //_fontBuffer[indexOfFont];
            return 0;
        }

        public int GetFontCol(int index)
        {
            return _fontBuffer[index];
        }

    }


    public class Fonts : IEnumerable
    {

        private List<FontType> _fonts = new List<FontType>();


        public void Add(FontType fontType)
        {
            _fonts.Add(fontType);
        }

        public FontType Get(int Index)
        {

            if (Index < _fonts.Count)
            {
                return _fonts[Index];
            }
            return null;
        }

        public FontType Get(String fontName)
        {
            foreach (FontType _ft in _fonts)
            {
                if (_ft.Name == fontName)
                {
                    return _ft;
                }
            }
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return _fonts.GetEnumerator();
        }

        public void PrintFont(Canvas canvas, int index)
        {
            FontType ft = _fonts[index];
            for (int i=0; i<ft.ColCount; i++)
            {
                int col = ft.GetFontCol(i);

            }
            
        }

    }
    
}

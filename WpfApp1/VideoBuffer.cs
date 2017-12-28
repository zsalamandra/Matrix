using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixPanel
{
    class VideoBuffer
    {
        private int[] _vbArr;
        private int _width;
        private int _height;
        private int _index = 0;

        public int Width { get => _width; }
        public int Height { get => _height; }

        public event EventHandler<EventArgs> RefreshData;

        public VideoBuffer(int width, int height)
        {
            _width = width;
            _height = height;
            _vbArr = new int[width];

        }

        protected virtual void OnRefreshData()
        {
            if (RefreshData != null)
            {
                //no custom data, so just use empty EventArgs
                RefreshData(this, EventArgs.Empty);
            }
        }

        /*
         * Функция возвращает состояние бита в массиве видеобуфера
         */
        public bool GetBitOfDot(int index)
        {
            int col = index / _height; // индекс столбца в массиве
            int bitNum = index % _height;
            return (_vbArr[col] & (1 << bitNum)) > 0;
        }

        public bool InvBitOfDot(int index)
        {
            int col = index / _height; // индекс столбца в массиве
            int bitNum = index % _height;
            _vbArr[col] ^= (1 << bitNum);
            return (_vbArr[col] & (1 << bitNum)) > 0;
        }

        public void SetBitOfDot(int index, bool state)
        {
            int col = index / _height; // индекс столбца в массиве
            int bitNum = index % _height;

            if (state == true)
            {
                _vbArr[col] |= (1 << bitNum);
            }
            else
            {
                _vbArr[col] &= ~(1 << bitNum);
            }
            
        }

        public int GetCol(int index)
        {
            return _vbArr[index];
        }

        public void AddCol(int coldata)
        {
            _vbArr[_index++] = coldata;
            OnRefreshData();
        }

 
    
    }
}

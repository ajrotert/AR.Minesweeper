using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AR.Minesweeper
{
    class Grid
    {
        public Grid(int width, int height, int bombs)
        {
            Width = width;
            Height = height;
            this.bombs = bombs;
            this._grid = new int[_width, _height];
        }

        private int _width = 0;
        public int Width
        {
            get{ return _width - 2; }
            set { _width = value + 2; }
        }
        private int _height = 0;
        public int Height
        {
            get { return _height - 2; }
            set { _height = value + 2; }
        }
        public int bombs;

        private int[,] _grid;

        public bool SetGrid(int w, int h, int value)
        {
            if (_grid[w, h] == -1)
                return false;
            if (value == -1)
                _grid[w, h] = value;
            else
                _grid[w, h]++;
            return true;
        }
        public int GetGrid(int w, int h)
        {
            return _grid[w, h];
        }

        public void ShowAll()
        {
            string grid = "";
            for(int a = 1; a < _width-1; a++)
            {
                for(int b = 1; b< _height-1; b++)
                {
                    grid += this._grid[b, a].ToString() +"  ";
                }
                grid += Environment.NewLine;
            }
            MessageBox.Show(grid);
        }
    }
}

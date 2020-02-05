using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Minesweeper
{
    class Bomb
    {
        public Bomb(Grid g)
        {
            set = g.bombs;
            this.g = g;
        }

        private int set;
        private Grid g;

        public void SetBombs()
        {
            Random r = new Random();
            int placeW, placeH;
            while (set > 0)
            {
                placeW = r.Next(g.Width)+1;
                placeH = r.Next(g.Height)+1;
                if (g.SetGrid(placeW, placeH, -1))
                {
                    set--;
                    g.SetGrid(placeW - 1, placeH - 1, 1);
                    g.SetGrid(placeW - 1, placeH, 1);
                    g.SetGrid(placeW - 1, placeH + 1, 1);
                    g.SetGrid(placeW, placeH - 1, 1);
                    g.SetGrid(placeW, placeH + 1, 1);
                    g.SetGrid(placeW + 1, placeH - 1, 1);
                    g.SetGrid(placeW + 1, placeH + 1, 1);
                    g.SetGrid(placeW + 1, placeH, 1);




                }
            }
        }
    }
}
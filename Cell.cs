using System;

namespace minesweeper
{
    public abstract class Cell
    {
        public bool Open { get; set; }

        public bool Flag { get; set; }

        public abstract void PrintCell();
    }
}
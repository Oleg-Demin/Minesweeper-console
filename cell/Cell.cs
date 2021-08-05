using System;

namespace minesweeper
{
    public class Cell
    {
        public bool Open { get; set; }

        public bool Flag { get; set; }

        public virtual void PrintCell()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" *");
            Console.ResetColor();
        }
    }
}
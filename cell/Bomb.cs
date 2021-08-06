using System;

namespace minesweeper
{
    public class Bomb : Cell
    {
        static public int NumberActiveBombs { get; set; }

        public Bomb()
        {
            Bomb.NumberActiveBombs++;
        }

        public override void PrintCell()
        {
            if (Open)
            {
                if (Flag)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" *");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" *");
                }

            }
            else
            {
                if (Flag)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" #");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(" *");
                }
            }

            // Console.ResetColor();
        }
    }
}
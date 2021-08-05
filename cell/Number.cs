using System;

namespace minesweeper
{
    public class Number : Cell
    {
        static public int NumberUnopenedNumbers { get; set; }

        public Number()
        {
            Number.NumberUnopenedNumbers++;
        }

        public int BombsAround { get; set; }

        public override void PrintCell()
        {
            if (Open)
            {
                if (Flag)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" #");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    if (BombsAround == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Console.Write($" {BombsAround}");
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
                    base.PrintCell();
                }
            }

            // Console.ResetColor();
        }
    }
}
using System;

namespace minesweeper
{
    public class Board
    {
        private int height;
        private int width;

        private int startI;
        private int startJ;

        private int numberBombs;

        private Cell[,] board;

        public bool Loss { get; private set; }

        public bool Win { get; private set; }


        public Board(int height, int width, int numderBombs, int startI, int startJ)
        {
            this.height = height;
            this.width = width;

            this.numberBombs = numderBombs;

            this.startI = startI;
            this.startJ = startJ;

            board = new Cell[height, width];

            // Заполняем поле бомбами
            Random rnd = new Random();
            while (Bomb.NumberActiveBombs < numberBombs)
            {
                int rndI = rnd.Next(0, height);
                int rndJ = rnd.Next(0, width);

                if (board[rndI, rndJ] is Bomb) continue;

                int topI = startI - 1;
                int btmI = startI + 1;
                int lftJ = startJ - 1;
                int rgtJ = startJ + 1;

                if (rndI == startI && rndJ == startJ) continue;

                if (rndI == topI && rndJ == lftJ) continue;
                if (rndI == topI && rndJ == rgtJ) continue;

                if (rndI == btmI && rndJ == lftJ) continue;
                if (rndI == btmI && rndJ == rgtJ) continue;

                if (rndI == startI && rndJ == lftJ) continue;
                if (rndI == startI && rndJ == rgtJ) continue;

                if (rndI == topI && rndJ == startJ) continue;
                if (rndI == btmI && rndJ == startJ) continue;

                board[rndI, rndJ] = new Bomb();
            }

            // Заполняем поле номерами
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (board[i, j] is Bomb) continue;
                    Number numder = new Number();

                    int topI = i - 1;
                    int btmI = i + 1;
                    int lftJ = j - 1;
                    int rgtJ = j + 1;

                    if (topI >= 0 && lftJ >= 0 && board[topI, lftJ] is Bomb) numder.BombsAround++;
                    if (topI >= 0 && rgtJ < width && board[topI, rgtJ] is Bomb) numder.BombsAround++;

                    if (btmI < height && lftJ >= 0 && board[btmI, lftJ] is Bomb) numder.BombsAround++;
                    if (btmI < height && rgtJ < width && board[btmI, rgtJ] is Bomb) numder.BombsAround++;

                    if (lftJ >= 0 && board[i, lftJ] is Bomb) numder.BombsAround++;
                    if (rgtJ < width && board[i, rgtJ] is Bomb) numder.BombsAround++;

                    if (topI >= 0 && board[topI, j] is Bomb) numder.BombsAround++;
                    if (btmI < height && board[btmI, j] is Bomb) numder.BombsAround++;

                    board[i, j] = numder;
                }
            }

            // Открываем стартовую ячейку
            OpenCell(startI, startJ);
        }

        public void OpenCell(int i, int j)
        {
            // Открытие ячеек с числами
            if (board[i, j] is Number && !board[i, j].Open)
            {
                board[i, j].Open = true;
                Number.NumberUnopenedNumbers--;

                if (Number.NumberUnopenedNumbers == 0 && Bomb.NumberActiveBombs == 0)
                {
                    Win = true;
                    return;
                }

                int bombsAround = ((Number)board[i, j]).BombsAround;
                if (bombsAround == 0)
                {
                    int topI = i - 1;
                    int btmI = i + 1;
                    int lftJ = j - 1;
                    int rgtJ = j + 1;

                    if (topI >= 0 && lftJ >= 0) OpenCell(topI, lftJ);
                    if (topI >= 0 && rgtJ < width) OpenCell(topI, rgtJ);

                    if (btmI < height && lftJ >= 0) OpenCell(btmI, lftJ);
                    if (btmI < height && rgtJ < width) OpenCell(btmI, rgtJ);

                    if (lftJ >= 0) OpenCell(i, lftJ);
                    if (rgtJ < width) OpenCell(i, rgtJ);

                    if (topI >= 0) OpenCell(topI, j);
                    if (btmI < height) OpenCell(btmI, j);
                }
            }

            // Открытие ячеек с бомбами
            if (board[i, j] is Bomb && !board[i, j].Flag)
            {
                Loss = true;
            }
        }

        public void PutFlag(int i, int j)
        {
            if (board[i, j].Open) return;

            if (board[i, j].Flag)
            {
                board[i, j].Flag = false;

                if (board[i, j] is Bomb)
                {
                    Bomb.NumberActiveBombs++;
                }
            }
            else
            {
                board[i, j].Flag = true;

                if (board[i, j] is Bomb)
                {
                    Bomb.NumberActiveBombs--;
                }
            }

            if (Number.NumberUnopenedNumbers == 0 && Bomb.NumberActiveBombs == 0)
            {
                Win = true;
                return;
            }
        }

        public void OpenAllCells()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j].Open = true;
                }
            }
        }

        public void PrintBoard()
        {
            int heightMaxI = (height - 1).ToString().Length;
            int widthMaxJ = (width - 1).ToString().Length;

            int numberRank = widthMaxJ;
            while (numberRank > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"".PadLeft(heightMaxI + 1));
                for (int i = 0; i < width; i++)
                {
                    int numeric = i % (int)Math.Pow(10, numberRank) / (int)Math.Pow(10, numberRank - 1);

                    if (numeric == 0 && i < (int)Math.Pow(10, numberRank) && numberRank != 1)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write($" {numeric}");
                    }
                }
                Console.WriteLine();

                numberRank--;
            }

            for (int i = 0; i < height; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{i}".PadLeft(heightMaxI  + 1));
                for (int j = 0; j < width; j++)
                {
                    board[i, j].PrintCell();
                }
                Console.WriteLine();
            }

            // Console.WriteLine($"Количество не открытых номеров {Number.NumberUnopenedNumbers}");
            // Console.WriteLine($"Количество активных бомб {Bomb.NumberActiveBombs}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
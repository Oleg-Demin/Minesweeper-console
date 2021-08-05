using System;

namespace minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Игра: МИНЕР");
            Console.WriteLine("План взаимодействия с игрой:");
            Console.WriteLine("1) вводим 5 параметров для построения игрового поля");
            Console.WriteLine("    - высота поля (от 4 до 100)");
            Console.WriteLine("    - ширина поля (от 4 до 100)");
            Console.WriteLine("    - количество бомб на поле (от 1 до высота * ширина - 9)");
            Console.WriteLine("    - координаты i и j первой открытой ячейки (от 0 до ширина - 1 или высота - 1)");
            Console.WriteLine("2) в течении игры вводим");
            Console.WriteLine("    - команду open или flag для открытия или флагования ячейки");
            Console.WriteLine("    - координаты i и j поля которое открываем или флагуем");
            Console.WriteLine("3) для досрочного выхода из игры в любом поле ввода пишем (end)");
            Console.WriteLine();

            Console.WriteLine("Введите размеры игрового поля.");
            int height;
            int width;
            InputHeightWidth(out height, out width);

            Console.WriteLine("Укажите число бомб располагаемых на игровом поле.");
            int numberBombs;
            InputNumberBombs(height, width, out numberBombs);

            Console.WriteLine("Откройте первую ячейку.");
            int startI;
            int startJ;
            InputIJ(height, width, out startI, out startJ);

            Board board = new Board(height, width, numberBombs, startI, startJ);
            board.PrintBoard();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Команда (open, flag): ");
                string command = Console.ReadLine();
                EndGame(command);

                if (command == "open")
                {
                    int i;
                    int j;
                    InputIJ(height, width, out i, out j);

                    board.OpenCell(i, j);

                    if (board.Loss) break;
                    if (board.Win) break;

                    board.PrintBoard();
                }

                if (command == "flag")
                {
                    int i;
                    int j;
                    InputIJ(height, width, out i, out j);

                    board.PutFlag(i, j);

                    if (board.Loss) break;
                    if (board.Win) break;

                    board.PrintBoard();
                }
            }

            board.OpenAllCells();
            board.PrintBoard();

            if (board.Loss)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ВЫ ПРОИГРАЛИ!");
                Console.ResetColor();
            }
            if (board.Win)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ВЫ ВЫЙГРАЛИ!");
                Console.ResetColor();
            }
        }

        static void InputHeightWidth(out int height, out int width)
        {
            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                Console.Write("Высота: ");

                string str = Console.ReadLine();
                EndGame(str);

                if (int.TryParse(str, out height) && height >= 4 && height <= 100)
                    break;
            }

            while (true)
            {
                Console.Write("Ширина: ");

                string str = Console.ReadLine();
                EndGame(str);

                if (int.TryParse(str, out width) && width >= 4 && width <= 100)
                    break;
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        static void InputNumberBombs(int height, int width, out int numberBombs)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int maxBombs = height * width - 9;
            while (true)
            {
                Console.Write($"Количество бомб (max {maxBombs}): ");

                string str = Console.ReadLine();
                EndGame(str);

                if (int.TryParse(str, out numberBombs) && numberBombs >= 1 && numberBombs <= maxBombs)
                    break;
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        static void InputIJ(int height, int width, out int i, out int j)
        {
            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                Console.Write("i: ");

                string str = Console.ReadLine();
                EndGame(str);

                if (int.TryParse(str, out i) && i >= 0 && i < height)
                    break;
            }

            while (true)
            {
                Console.Write("j: ");
                
                string str = Console.ReadLine();
                EndGame(str);
                
                if (int.TryParse(str, out j) && j >= 0 && j < width)
                    break;
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        static void EndGame(string str)
        {
            if (str == "end")
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("СПАСИБО ЗА ИГРУ!");
                Console.ResetColor();

                System.Environment.Exit(0);
            }
        }
    }
}

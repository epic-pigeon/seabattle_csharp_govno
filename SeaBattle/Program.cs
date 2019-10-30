using System;
using System.Data;

namespace SeaBattle  {
    internal static class Program  {
        public static void Main(string[] args)
        {
            _Main2(args);
        }

        private static void _Main1(string[] args)
        {
            Board board1 = new Board(), board2 = new Board();
            board1.SetCell(1, 1, Board.CellStatus.ClosedShip);
            board1.SetCell(1, 2, Board.CellStatus.ClosedEmpty);
            board1.SetCell(1, 3, Board.CellStatus.ClosedShip);
            board1.SetCell(1, 4, Board.CellStatus.ClosedShip);
            //board1.Shoot(1, 0);
            board1.Shoot(1, 1);
            //board1.Shoot(1, 2);
            board1.Shoot(1, 3);
            //board1.Shoot(1, 4);
            //Console.WriteLine(Board.CellStatus.Empty.Equals(Board.CellStatus.Empty));
            ShipArrangement.Random.Apply(board2);
            board2.Shoot(0, 0);
            board2.Shoot(7, 0);
            board2.Shoot(3, 3);
            board2.Shoot(3, 4);
            board2.Shoot(3, 5);
            SeaBattle.DrawBoards(board1, board2);
        }

        private static void _Main2(string[] args)
        {
            Console.WriteLine("Enter first player's name:");
            string name1 = Console.ReadLine();
            Console.WriteLine("Enter second player's name:");
            string name2 = Console.ReadLine();
            bool turn = true;
            Board board1 = new Board(), board2 = new Board();
            ShipArrangement.Random.Apply(board1);
            ShipArrangement.Random.Apply(board2);
            while (!board1.CheckAllShipsDestroyed() && !board2.CheckAllShipsDestroyed())
            {
                Console.Clear();
                Console.WriteLine(name1 + "   " + name2);
                SeaBattle.DrawBoards(board1, board2);
                Console.Write((turn ? name1 : name2) + "'s turn! Enter your move >");
                Board currentBoard = turn ? board2 : board1;
                string move;
                int row, column;
                while (true)
                {
                    move = Console.ReadLine();
                    if (move.Length != 2)
                    {
                        Console.Write("A move should consist of a upper-case latin letter and a digit. >");
                        continue;
                    }

                    if (65 > move[0] || move[0] >= 65 + Board.Size)
                    {
                        Console.Write("First letter should be column letter. >");
                        continue;
                    }

                    if (49 > move[1] || move[1] >= 49 + Board.Size)
                    {
                        Console.Write("Second digit should be the number of the row. >");
                        continue;
                    }

                    row = move[1] - 49;
                    column = move[0] - 65;

                    if (Board.CellIsOpen(currentBoard.GetCell(row, column)))
                    {
                        Console.Write("You already shot there. >");
                        continue;
                    }

                    break;
                }
                currentBoard.Shoot(row, column);
                if (currentBoard.GetCell(row, column) == Board.CellStatus.Empty) turn = !turn;
            }

            bool firstWon = board2.CheckAllShipsDestroyed();
            
            (firstWon ? board1 : board2).Reveal();
            
            Console.Clear();
            Console.WriteLine(name1 + "   " + name2);
            SeaBattle.DrawBoards(board1, board2);
            Console.WriteLine((firstWon ? name1 : name2) + " won!");
        }
    }
}
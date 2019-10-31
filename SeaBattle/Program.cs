using System;
using System.Data;
using System.Diagnostics;

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
            for (int i = 0; i < 2; i++)
            {
                Board currentBoard = turn ? board1 : board2;
                Console.Clear();
                Console.WriteLine((turn ? name1 : name2) + "'s turn to arrange their ships!");
                Console.Write("Please enter R for random arrangement and C for custom >");
                bool random;
                while (true)
                {
                    string c = Console.ReadLine();
                    if (c.Length != 1)
                    {
                        Console.Write("You should only enter one character >");
                        continue;
                    }

                    if (c[0] != 'C' && c[0] != 'R')
                    {
                        Console.Write("You should only C or R >");
                        continue;
                    }

                    random = c[0] == 'R';
                    break;
                }

                if (random)
                {
                    while (true)
                    {
                        Console.Clear();
                        currentBoard.Reset();
                        ShipArrangement.Random.Apply(currentBoard);
                        currentBoard.Reveal();
                        SeaBattle.DrawBoards(currentBoard);
                        Console.Write("Enter S to stop and anything else to continue >");
                        string response = Console.ReadLine();
                        if (response.Length == 1 && response[0] == 'S') break;
                    }
                }
                else
                {
                    ShipArrangement arrangement = ShipArrangement.Empty;
                    for (int k = 9; k >= 0; k--)
                    {
                        int size = 0;
                        switch (k)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                size = 1;
                                break;
                            case 4:
                            case 5:
                            case 6:
                                size = 2;
                                break;
                            case 7:
                            case 8:
                                size = 3;
                                break;
                            case 9:
                                size = 4;
                                break;
                        }
                        Console.Clear();
                        currentBoard.Reveal();
                        SeaBattle.DrawBoards(currentBoard);
                        Console.Write("Enter a position for ship sized " + size + " >");
                        while (true)
                        {
                            string result = Console.ReadLine();
                            if (size == 1)
                            {
                                if (result.Length < 2)
                                {
                                    Console.Write("You should enter a letter for column and a number for row >");
                                    continue;
                                }

                                if (65 > result[0] || result[0] >= 65 + Board.Size)
                                {
                                    Console.Write("You should enter a letter for column >");
                                    continue;
                                }

                                int row;

                                if (!int.TryParse(result.Substring(1), out row))
                                {
                                    Console.Write("You should enter a number for row >");
                                    continue;
                                }

                                row--;
                                int column = result[0] - 65;
                                if (!arrangement.Add(row, column, ShipArrangement.Direction.Down, size))
                                {
                                    Console.Write("Cannot add a ship there >");
                                    continue;
                                }
                                
                                arrangement.Apply(currentBoard);
                                break;
                            }
                            else
                            {
                                if (result.Length < 3)
                                {
                                    Console.Write(
                                        "You should enter a letter for direction, a letter for column and a number for row >");
                                    continue;
                                }

                                if (result[0] != 'U' && result[0] != 'D' && result[0] != 'R' && result[0] != 'L')
                                {
                                    Console.Write("You should enter a letter for direction(R, L, U, D) >");
                                    continue;
                                }
                                
                                if (65 > result[1] || result[1] >= 65 + Board.Size)
                                {
                                    Console.Write("You should enter a letter for column >");
                                    continue;
                                }

                                int row;

                                if (!int.TryParse(result.Substring(2), out row))
                                {
                                    Console.Write("You should enter a number for row >");
                                    continue;
                                }

                                row--;
                                int column = result[1] - 65;
                                ShipArrangement.Direction direction = ShipArrangement.Direction.Down;
                                switch (result[0])
                                {
                                    case 'U':
                                        direction = ShipArrangement.Direction.Up;
                                        break;
                                    case 'D':
                                        direction = ShipArrangement.Direction.Down;
                                        break;
                                    case 'L':
                                        direction = ShipArrangement.Direction.Left;
                                        break;
                                    case 'R':
                                        direction = ShipArrangement.Direction.Right;
                                        break;
                                }
                                if (!arrangement.Add(row, column, direction, size))
                                {
                                    Console.Write("Cannot add a ship there >");
                                    continue;
                                }
                                arrangement.Apply(currentBoard);
                                break;
                            }
                        }
                    }
                }
                currentBoard.Hide();
                turn = !turn;
            }
            //ShipArrangement.Random.Apply(board1);
            //ShipArrangement.Random.Apply(board2);
            while (!board1.CheckAllShipsDestroyed() && !board2.CheckAllShipsDestroyed())
            {
                Console.Clear();
                Console.WriteLine("  " 
                                  + (name1 + "                 ").Substring(0, Board.Size) 
                                  + "   " 
                                  + (name2 + "                 ").Substring(0, Board.Size));
                SeaBattle.DrawBoards(board1, board2);
                Console.Write((turn ? name1 : name2) + "'s turn! Enter your move >");
                Board currentBoard = turn ? board2 : board1;
                string move;
                int row, column;
                while (true)
                {
                    move = Console.ReadLine();
                    if (move.Length == 0)
                    {
                        Console.Write("A move should consist of a upper-case latin letter and a digit. >");
                        continue;
                    }

                    if (65 > move[0] || move[0] >= 65 + Board.Size)
                    {
                        Console.Write("First letter should be column letter. >");
                        continue;
                    }

                    if (!int.TryParse(move.Substring(1), out row) || 1 > row || row > Board.Size)
                    {
                        Console.Write("Second number should be the number of the row. >");
                        continue;
                    }

                    row--;
                    //row = move[1] - 49;
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
            Console.WriteLine("  " 
                              + (name1 + "                 ").Substring(0, Board.Size) 
                              + "   " 
                              + (name2 + "                 ").Substring(0, Board.Size));
            SeaBattle.DrawBoards(board1, board2);
            Console.WriteLine((firstWon ? name1 : name2) + " won!");
        }
    }
}
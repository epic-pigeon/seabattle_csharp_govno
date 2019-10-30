using System;
using System.Data;

namespace SeaBattle  {
    internal static class Program  {
        public static void Main(string[] args)
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
    }
}
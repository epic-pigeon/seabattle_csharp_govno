using System.Data;

namespace SeaBattle  {
    internal static class Program  {
        public static void Main(string[] args)
        {
            Board board1 = new Board(), board2 = new Board();
            board1.SetCell(1, 1 , Board.CellStatus.ClosedShip);
            board1.SetCell(1 , 2 , Board.CellStatus.ClosedEmpty);
            board1.SetCell(1, 3 , Board.CellStatus.ClosedShip);
            board1.SetCell(1, 4 , Board.CellStatus.ClosedShip);
            board1.Shoot(1 , 3);
            board1.Shoot(1, 4);
            SeaBattle.DrawBoards(board1, board2);
        }
    }
}
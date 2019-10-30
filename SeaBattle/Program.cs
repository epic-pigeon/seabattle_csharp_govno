using System.Data;

namespace SeaBattle  {
    internal static class Program  {
        public static void Main(string[] args)
        {
            Board board = new Board();
            board.Shoot(1, 1);
            board.SetCell(2, 2, Board.CellStatus.Ship);
            SeaBattle.DrawBoards(board, board);
        }
    }
}
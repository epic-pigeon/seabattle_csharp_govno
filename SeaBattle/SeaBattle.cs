using System;
using System.Collections.Specialized;
using System.Data;

namespace SeaBattle {
    public class SeaBattle {
        public static void DrawBoards(params Board[] boards) {
            for (int i = -1; i < Board.Size; i++) {
                foreach (var board in boards) {
                    for (int j = -1; j < Board.Size; j++) {
                        if (i == -1)
                        {
                            if (j == -1) Console.Write("  "); else Console.Write((char) ('A' + j));
                        }
                        else
                        {
                            if (j == -1)
                            {
                                if (("" + (i + 1)).Length < 2)
                                {
                                    Console.Write(" ");
                                }

                                Console.Write(i + 1);
                            }
                            else
                            {
                                board.PrintCell(i, j);
                                //Console.Write(Board.ColoredCharFromCellStatus(board.GetCell(i, j)).Char);
                            }
                        } 
                        //Console.Write(" ");
                        Console.Out.Flush();
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
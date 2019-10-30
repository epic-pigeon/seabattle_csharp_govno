using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace SeaBattle
{
    public class Board
    {
        public enum CellStatus 
        {
            ClosedEmpty, ClosedShip, Empty, Ship
        }

        public static bool CellIsClosed(CellStatus status)
        {
            return status == CellStatus.ClosedEmpty || status == CellStatus.ClosedShip;
        }

        public static bool CellIsOpen(CellStatus status)
        {
            return !CellIsClosed(status);
        }

        public static ColoredChar ColoredCharFromCellStatus(CellStatus status)
        {
            ColoredChar result = new ColoredChar();
            switch (status)
            {
                case CellStatus.ClosedEmpty: case CellStatus.ClosedShip:
                    result.BackgroundColor = ConsoleColor.White;
                    result.Char = '.';
                    break;
                case CellStatus.Ship:
                    result.BackgroundColor = ConsoleColor.Red;
                    result.ForegroundColor = ConsoleColor.Black;
                    result.Char = 'X';
                    break;
                case CellStatus.Empty:
                    result.BackgroundColor = ConsoleColor.Blue;
                    result.ForegroundColor = ConsoleColor.White;
                    result.Char = '-';
                    break;
                default: throw new Exception();
            }
            return result;
        }

        public const int Size = 8;

        private DataTable board;

        public Board()
        {
            board = new DataTable();
            for (int i = 0; i < Size; i++)
            {
                board.Columns.Add("Column " + i, typeof(CellStatus));
            }

            for (int i = 0; i < Size; i++)
            {
                object[] arr = new object[Size];
                for (int j = 0; j < Size; j++)
                {
                    arr[j] = CellStatus.ClosedEmpty;
                }

                board.Rows.Add(arr);
            }
        }

        public void SetCell(int row, int column, CellStatus status)
        {
            board.Rows[row][column] = status;
        }

        public CellStatus GetCell(int row, int column)
        {
            return (CellStatus) board.Rows[row][column];
        }

        public void Shoot(int row, int column)
        {
            if (!CellIsClosed(GetCell(row, column))) return;
            if (GetCell(row, column) == CellStatus.ClosedEmpty)
            {
                SetCell(row, column, CellStatus.Empty);
            }
            else if (GetCell(row, column) == CellStatus.ClosedShip)
            {
                SetCell(row, column, CellStatus.Ship);
                
            }
        }

        public bool CheckBounds(int row, int column)
        {
            return 0 <= row && row < Size && 0 <= column && column < Size;
        }

        public void PrintCell(int row, int column)
        {
            ColoredCharFromCellStatus(GetCell(row, column)).Print();
        }

        public DataTable ToDataTable()
        {
            return board;
        }
    }
}
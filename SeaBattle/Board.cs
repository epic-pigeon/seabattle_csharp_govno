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

        public const int Size = 10;

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

        public bool CheckAllShipsDestroyed()
        {
            for (int i = 0; i < Size * Size; i++)
            {
                if (GetCell(i / Size, i % Size) == CellStatus.ClosedShip) return false;
            }

            return true;
        }

        public void Reveal()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (GetCell(row, column) == CellStatus.ClosedEmpty)
                    {
                        SetCell(row, column, CellStatus.Empty);
                    }
                    if (GetCell(row, column) == CellStatus.ClosedShip)
                    {
                        SetCell(row, column, CellStatus.Ship);
                    }
                }
            }
        }
        
        public void Hide()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (GetCell(row, column) == CellStatus.Empty)
                    {
                        SetCell(row, column, CellStatus.ClosedEmpty);
                    }
                    if (GetCell(row, column) == CellStatus.Ship)
                    {
                        SetCell(row, column, CellStatus.ClosedShip);
                    }
                }
            }
        }
        
        public void Reset()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    SetCell(row, column, CellStatus.ClosedEmpty);
                }
            }
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
                if (CheckShipDestroyed(row, column, 0))
                {
                    //Console.WriteLine("kar");
                    SetShipDestroyed(row, column, 0);
                }
            }
        }

        public void SetShipDestroyed(int row, int column, int direction)
        {
            int tempRow, tempColumn;
            for (int i = 0; i < 9; ++i)
            {
                tempRow = row - 1 + i % 3;
                tempColumn = column - 1 + i / 3;
                if (CheckBounds(tempRow, tempColumn))
                {
                    if (GetCell(tempRow, tempColumn).Equals(CellStatus.ClosedEmpty))
                    {
                        SetCell(tempRow, tempColumn, CellStatus.Empty);
                    }
                }
            }

            if (direction == 0)
            {
                
                if (CheckBounds(row - 1, column))
                {
                    if (GetCell(row - 1, column).Equals(CellStatus.Ship))
                    {
                        SetShipDestroyed(row - 1, column, -1);
                    }
                }
                if (CheckBounds(row + 1, column))
                {
                    if (GetCell(row + 1, column).Equals(CellStatus.Ship))
                    {
                        SetShipDestroyed(row + 1, column, 1);
                    }
                }
                if (CheckBounds(row, column - 1))
                {
                    if (GetCell(row , column - 1).Equals(CellStatus.Ship))
                    {
                        SetShipDestroyed(row, column, -2);
                    }
                }
                if (CheckBounds(row, column + 1))
                {
                    if (GetCell(row, column + 1).Equals(CellStatus.Ship))
                    {
                        SetShipDestroyed(row, column + 1, 2);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CheckBounds(row + 1, column))
                        {
                            if (GetCell(row + 1, column).Equals(CellStatus.Ship))
                            {
                                SetShipDestroyed(row + 1, column, 1);
                            }
                        } 
                        break;
                    case -1:
                        if (CheckBounds(row - 1, column))
                        {
                            if (GetCell(row - 1, column).Equals(CellStatus.Ship))
                            {
                                SetShipDestroyed(row - 1, column, -1);
                            }
                        } 
                        break;
                    case 2:
                        if (CheckBounds(row, column + 1))
                        {
                            if (GetCell(row, column + 1).Equals(CellStatus.Ship))
                            {
                                SetShipDestroyed(row, column + 1, 2);
                            }
                        }
                        break;
                    case -2:
                        if (CheckBounds(row, column - 1))
                        {
                            if (GetCell(row , column - 1).Equals(CellStatus.Ship))
                            {
                                SetShipDestroyed(row, column - 1, -2);
                            }
                        }
                        break;
                }
            }
        }

        public bool CheckShipDestroyed(int row, int column, int direction)
        {
            int tempRow, tempColumn;
            for (int i = 0; i < 9; ++i)
            {
                tempRow = row - 1 + i % 3;
                tempColumn = column - 1 + i / 3;
                if (i != 4 && CheckBounds(tempRow, tempColumn))
                {
                    if (GetCell(tempRow, tempColumn) == CellStatus.ClosedShip)
                    {
                        return false;
                    }
                }
            }

            bool fl1 = true, fl2 = true, fl3 = true, fl4 = true;
            if (direction == 0)
            {
                
                if (CheckBounds(row - 1, column))
                {
                    if (GetCell(row - 1, column).Equals(CellStatus.Ship))
                    {
                        fl1 = CheckShipDestroyed(row - 1, column, -1);
                    }
                }
                if (CheckBounds(row + 1, column))
                {
                    if (GetCell(row + 1, column).Equals(CellStatus.Ship))
                    {
                        fl2 = CheckShipDestroyed(row + 1, column, 1);
                    }
                }
                if (CheckBounds(row, column - 1))
                {
                    if (GetCell(row , column - 1).Equals(CellStatus.Ship))
                    {
                        fl3 = CheckShipDestroyed(row, column, -2);
                    }
                }
                if (CheckBounds(row, column + 1))
                {
                    if (GetCell(row, column + 1).Equals(CellStatus.Ship))
                    {
                        fl4 = CheckShipDestroyed(row, column + 1, 2);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CheckBounds(row + 1, column))
                        {
                            if (GetCell(row + 1, column).Equals(CellStatus.Ship))
                            {
                                fl2 = CheckShipDestroyed(row + 1, column, 1);
                            }
                        } 
                        break;
                    case -1:
                        if (CheckBounds(row - 1, column))
                        {
                            if (GetCell(row - 1, column).Equals(CellStatus.Ship))
                            {
                                fl1 = CheckShipDestroyed(row - 1, column, -1);
                            }
                        } 
                        break;
                    case 2:
                        if (CheckBounds(row, column + 1))
                        {
                            if (GetCell(row, column + 1).Equals(CellStatus.Ship))
                            {
                                fl4 = CheckShipDestroyed(row, column + 1, 2);
                            }
                        }
                        break;
                    case -2:
                        if (CheckBounds(row, column - 1))
                        {
                            if (GetCell(row , column - 1).Equals(CellStatus.Ship))
                            {
                                fl3 = CheckShipDestroyed(row, column - 1, -2);
                            }
                        }
                        break;
                }
            }

            return fl1 && fl2 && fl3 && fl4;
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
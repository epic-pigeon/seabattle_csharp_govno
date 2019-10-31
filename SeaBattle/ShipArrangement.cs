﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SeaBattle
{
    public class ShipArrangement
    {
        private class Ship
        {
            private int row, column;
            private Direction direction;
            private int size;

            public Ship(int row, int column, Direction direction, int size)
            {
                this.row = row;
                this.column = column;
                this.direction = direction;
                this.size = size;
            }

            public int Row => row;

            public int Column => column;

            public Direction Direction => direction;

            public int Size => size;
        }

        public enum Direction
        {
            Up, Down, Left, Right
        }

        private List<Ship> ships;

        private ShipArrangement(List<Ship> ships)
        {
            this.ships = ships;
        }

        public static ShipArrangement Random {
            get
            {
                int random = new Random().Next(5);
                if (true)
                {
                    return new ShipArrangement(new List<Ship>
                    {
                        new Ship(0, 0, Direction.Right, 1),
                        new Ship(7, 0, Direction.Right, 1),
                        new Ship(0, 7, Direction.Right, 1),
                        new Ship(3, 3, Direction.Right, 3),
                        new Ship(7, 7, Direction.Right, 1),
                    });
                }

                return null;
            }
        }
        
        public static ShipArrangement Empty => new ShipArrangement(new List<Ship>());

        public bool Add(int row, int column, Direction direction, int size)
        {
            if (0 > row || row >= Board.Size || 0 > column || column >= Board.Size)
            {
                return false;
            }

            switch (direction)
            {
                case Direction.Down:
                    if (0 > (row + size - 1) || (row + size - 1) >= Board.Size)
                    {
                        return false;
                    }
                    break;
                case Direction.Up:
                    if (0 > (row - size + 1) || (row - size + 1) >= Board.Size)
                    {
                        return false;
                    }
                    break;
                case Direction.Left:
                    if (0 > (column - size + 1) || (column - size + 1) >= Board.Size)
                    {
                        return false;
                    }
                    break;
                case Direction.Right:
                    if (0 > (column + size - 1) || (column + size - 1) >= Board.Size)
                    {
                        return false;
                    }
                    break;
            }
            
            // TODO
            foreach (var ship in ships)
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        int shipRow, shipColumn, currentRow, currentColumn;
                        shipRow = shipColumn = currentRow = currentColumn = 0;
                        switch (ship.Direction)
                        {
                            case Direction.Down:
                                shipRow = ship.Row + i;
                                shipColumn = ship.Column;
                                break;
                            case Direction.Up:
                                shipRow = ship.Row - i;
                                shipColumn = ship.Column;
                                break;
                            case Direction.Left:
                                shipRow = ship.Row;
                                shipColumn = ship.Column - i;
                                break;
                            case Direction.Right:
                                shipRow = ship.Row;
                                shipColumn = ship.Column + i;
                                break;
                        }
                        switch (direction)
                        {
                            case Direction.Down:
                                currentRow = row + i;
                                currentColumn = column;
                                break;
                            case Direction.Up:
                                currentRow = row - i;
                                currentColumn = column;
                                break;
                            case Direction.Left:
                                currentRow = row;
                                currentColumn = column - i;
                                break;
                            case Direction.Right:
                                currentRow = row;
                                currentColumn = column + i;
                                break;
                        }

                        if (Math.Abs(shipRow - currentRow) < 2 && Math.Abs(shipColumn - currentColumn) < 2)
                        {
                            return false;
                        }
                    }
                }
            }

            ships.Add(new Ship(row, column, direction, size));
            return true;
        }

        public void Apply(Board board)
        {
            foreach (var ship in ships)
            {
                int row = ship.Row, column = ship.Column;
                switch (ship.Direction)
                {
                    case Direction.Down:
                        for (var i = 0; i < ship.Size; i++)
                        {
                            board.SetCell(row + i, column, Board.CellStatus.ClosedShip);
                        }
                        break;
                    case Direction.Up:
                        for (var i = 0; i < ship.Size; i++)
                        {
                            board.SetCell(row - i, column, Board.CellStatus.ClosedShip);
                        }
                        break;
                    case Direction.Left:
                        for (var i = 0; i < ship.Size; i++)
                        {
                            board.SetCell(row, column - i, Board.CellStatus.ClosedShip);
                        }
                        break;
                    case Direction.Right:
                        for (var i = 0; i < ship.Size; i++)
                        {
                            board.SetCell(row, column + i, Board.CellStatus.ClosedShip);
                        }
                        break;
                }
            }
        }
    }
}
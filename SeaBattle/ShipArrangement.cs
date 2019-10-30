using System;
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

        private enum Direction
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
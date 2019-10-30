using System;

namespace SeaBattle
{
    public class ColoredChar
    {
        public ConsoleColor ForegroundColor = ConsoleColor.White,
            BackgroundColor = ConsoleColor.Black;
        public char Char;

        public void Print()
        {
            ConsoleColor foregroundSave = Console.ForegroundColor, backgroundSave = Console.BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.Write(Char);
            Console.ForegroundColor = foregroundSave;
            Console.BackgroundColor = backgroundSave;
        }
    }
}
using System;
using System.Threading;
using RpgGame.Characters;
using RpgGame.Logic;

namespace RpgGame.Logic
{
    public static class DisplayManager
    {
        private const int TotalWidth = 80;
        private const int ColumnWidth = 35;
        private const int LogStartRow = 12;
        private const int MaxLogLines = 10;

        private static int _currentLogLine = 0;

        public static void DrawHeader(string title, EnvironmentType env)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', TotalWidth));
            string centeredTitle = CenterText($" {title} - {env} ", TotalWidth);
            Console.WriteLine(centeredTitle);
            Console.WriteLine(new string('=', TotalWidth));
            Console.ResetColor();
        }

        public static void UpdateBattleScreen(Character hero, Character monster)
        {
            // Reset cursor to just below header
            Console.SetCursorPosition(0, 3);
            
            // Hero Block
            DrawCharacterBlock(hero, 2, ConsoleColor.Green);
            
            // Divider
            for(int i = 4; i < 9; i++)
            {
                Console.SetCursorPosition(40, i);
                Console.Write("|");
            }

            // Monster Block
            DrawCharacterBlock(monster, 45, ConsoleColor.Yellow);

            Console.SetCursorPosition(0, 10);
            Console.WriteLine(new string('=', TotalWidth));
        }

        private static void DrawCharacterBlock(Character c, int x, ConsoleColor color)
        {
            int y = 5;

            ClearLineSegment(y, x, 35);
            Console.SetCursorPosition(x, y++);
            Console.ForegroundColor = color;
            Console.Write($"{c.Name.ToString().ToUpper()}");
            Console.ResetColor();

            ClearLineSegment(y, x, 35);
            Console.SetCursorPosition(x, y++);
            DrawHealthBar(c.Health, c.MaxHealth);

            ClearLineSegment(y, x, 35);
            Console.SetCursorPosition(x, y++);
            Console.Write($"Strength: {c.Strength}");
            
            ClearLineSegment(y, x, 35);
            Console.SetCursorPosition(x, y++);
            string weapon = c.EquippedWeapon?.Name ?? "Bare Hands";
            Console.Write($"Weapon: {weapon}");
        }

        private static void DrawHealthBar(int current, int max)
        {
            const int barLength = 20;
            double percentage = (double)current / max;
            int filled = (int)Math.Max(0, Math.Min(barLength, Math.Round(percentage * barLength)));

            Console.Write("HP [");
            
            if (percentage < 0.25) Console.ForegroundColor = ConsoleColor.Red;
            else if (percentage < 0.5) Console.ForegroundColor = ConsoleColor.DarkYellow;
            else Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(new string('■', filled));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('-', barLength - filled));
            Console.ResetColor();
            Console.Write($"] {current}/{max}");
        }

        /// <summary>
        /// Prints a message in the battle log area below the separator line.
        /// Used by character/monster/loot classes instead of raw Console.WriteLine.
        /// </summary>
        public static void PrintBattleLog(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            // If we've filled all log lines, scroll up
            if (_currentLogLine >= MaxLogLines)
            {
                ScrollLogUp();
                _currentLogLine = MaxLogLines - 1;
            }

            int row = LogStartRow + _currentLogLine;
            ClearFullLine(row);
            Console.SetCursorPosition(1, row);
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();

            _currentLogLine++;
        }

        /// <summary>
        /// Resets the battle log area (call when starting a new duel).
        /// </summary>
        public static void ResetBattleLog()
        {
            _currentLogLine = 0;
            for (int i = 0; i < MaxLogLines; i++)
            {
                ClearFullLine(LogStartRow + i);
            }
        }

        /// <summary>
        /// Scrolls all log lines up by one, discarding the top line.
        /// </summary>
        private static void ScrollLogUp()
        {
            // We can't actually "read" console lines, so we just clear the area
            // and reset to the last line. This gives a clean slate effect.
            for (int i = 0; i < MaxLogLines; i++)
            {
                ClearFullLine(LogStartRow + i);
            }
            _currentLogLine = 0;
        }

        /// <summary>
        /// Clears a full row by overwriting with spaces.
        /// </summary>
        private static void ClearFullLine(int row)
        {
            Console.SetCursorPosition(0, row);
            Console.Write(new string(' ', TotalWidth));
        }

        /// <summary>
        /// Clears a segment of a row starting at column x for the given length.
        /// Prevents leftover trailing characters when shorter text overwrites longer text.
        /// </summary>
        private static void ClearLineSegment(int row, int startCol, int length)
        {
            Console.SetCursorPosition(startCol, row);
            Console.Write(new string(' ', length));
        }

        public static void PrintMessage(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($" {message}");
            Console.ResetColor();
        }

        public static void CenterPrint(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(CenterText(text, TotalWidth));
            Console.ResetColor();
        }

        private static string CenterText(string text, int width)
        {
            if (text.Length >= width) return text;
            int leftPadding = (width - text.Length) / 2;
            return new string(' ', leftPadding) + text;
        }
    }
}

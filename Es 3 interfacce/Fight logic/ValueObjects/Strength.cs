using System;

namespace RpgGame.ValueObjects
{
       public class Strength
    {
        public int Value { get; }

        public const int MinValue = 0;

        public Strength(int value)
        {
            Value = Math.Max(MinValue, value);
        }

        public override string ToString()
        {
            return $"{Value} STR";
        }

        // Allows using Strength directly as an int
        public static implicit operator int(Strength str)
        {
            return str.Value;
        }

        // Allows creating Strength from an int (explicit to force validation awareness)
        public static explicit operator Strength(int value)
        {
            return new Strength(value);
        }

        // Arithmetic operators
        public static Strength operator +(Strength a, int change)
        {
            return new Strength(a.Value + change);
        }

        public static Strength operator -(Strength a, int change)
        {
            return new Strength(a.Value - change);
        }

        // Comparison operators
        public static bool operator <(Strength a, Strength b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(Strength a, Strength b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <=(Strength a, Strength b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(Strength a, Strength b)
        {
            return a.Value >= b.Value;
        }
    }
}

using System;

namespace RpgGame.ValueObjects
{
    
    public class HealthPoints
    {
        public int Value { get; }

        public const int MinValue = 0;

        public HealthPoints(int value)
        {
            Value = Math.Max(MinValue, value);
        }

        public override string ToString()
        {
            return $"{Value} HP";
        }

        // Allows using HealthPoints directly as an int
        public static implicit operator int(HealthPoints hp)
        {
            return hp.Value;
        }

        // Allows creating HealthPoints from an int (explicit to force validation awareness)
        public static explicit operator HealthPoints(int value)
        {
            return new HealthPoints(value);
        }

        // Arithmetic operators
        public static HealthPoints operator +(HealthPoints a, int change)
        {
            return new HealthPoints(a.Value + change);
        }

        public static HealthPoints operator -(HealthPoints a, int change)
        {
            return new HealthPoints(a.Value - change);
        }

        // Comparison operators
        public static bool operator <(HealthPoints a, HealthPoints b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >(HealthPoints a, HealthPoints b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <=(HealthPoints a, HealthPoints b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >=(HealthPoints a, HealthPoints b)
        {
            return a.Value >= b.Value;
        }
    }
}

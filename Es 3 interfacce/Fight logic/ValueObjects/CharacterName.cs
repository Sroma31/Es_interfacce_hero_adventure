using System;

namespace RpgGame.ValueObjects
{
    
    public class CharacterName
    {
        public string Value { get; }

        public CharacterName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Character name cannot be empty or whitespace.");
            }
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        // Allows implicit conversion: CharacterName -> string
        public static implicit operator string(CharacterName name)
        {
            return name.Value;
        }

        // Allows implicit conversion: string -> CharacterName
        public static implicit operator CharacterName(string value)
        {
            return new CharacterName(value);
        }
    }
}

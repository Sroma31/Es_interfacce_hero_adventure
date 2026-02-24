using System;
using System.Collections.Generic;

namespace RpgGame.Logic
{
    
    public class EnvironmentManager
    {
        private readonly Random _rnd = new Random();
        public EnvironmentType CurrentEnvironment { get; private set; }

        public EnvironmentManager()
        {
            ChangeEnvironment();
        }

        public void ChangeEnvironment()
        {
            var values = (EnvironmentType[])Enum.GetValues(typeof(EnvironmentType));
            CurrentEnvironment = values[_rnd.Next(values.Length)];
        }
    }
}

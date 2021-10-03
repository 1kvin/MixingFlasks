using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Entity.Flask;

namespace Scripts.GameLogic.Entity
{
    public class Map : ICloneable
    {
        public List<FlaskUnit> flaskUnits { get; private set; }
        public int numberFlask { get; private set; }
        
        public Map(List<FlaskUnit> flaskUnits, int numberFlask)
        {
            this.flaskUnits = flaskUnits;
            this.numberFlask = numberFlask;
        }

        private List<FlaskUnit> CloneFlaskUnits(List<FlaskUnit> flaskUnits)
        {
            return flaskUnits.Select(flaskUnit => (FlaskUnit)flaskUnit.Clone()).ToList();
        }

        public object Clone()
        {
            return new Map(CloneFlaskUnits(flaskUnits), numberFlask);
        }
    }
}
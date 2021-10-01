using System;
using UnityEngine;

namespace Scripts.Entity
{
    public class Liquid : ICloneable
    {
        public Color Color { get; private set; }
        public int ColorHash { get; private set; }
        
        public Liquid(Color color)
        {
            this.Color = color;
            this.ColorHash = (color.a, color.r, color.g, color.b).GetHashCode();
        }

        public object Clone()
        {
            return new Liquid(Color);
        }
    }
}

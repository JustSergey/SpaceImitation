using System.Drawing;
using System.Collections.Generic;

namespace SpaceImitation
{
    public class SpaceObject
    {
        public int Mass;
        public int Radius;
        public Vector Position;
        public Vector Velocity;
        public Color Color;
        public List<Vector> Tail;

        public SpaceObject(int mass, int radius, Color color)
        {
            Mass = mass;
            Radius = radius;
            Position = new Vector();
            Velocity = new Vector();
            Color = color;
            Tail = new List<Vector>();
        }

        public static SpaceObject Earth => new SpaceObject(156, 4, Color.LimeGreen);
        public static SpaceObject Moon => new SpaceObject(2, 1, Color.LightGray);
        public static SpaceObject Jupiter => new SpaceObject(49497, 44, Color.SandyBrown);
        public static SpaceObject Sun => new SpaceObject(51938640, 435, Color.LightYellow);
    }
}

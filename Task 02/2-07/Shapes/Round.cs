namespace _2_07
{
    using System;
    using System.Drawing;
    
    public class Round : Shape, IHasArea, IHasPerimeter
    {
        private int radius = 0;
        
        public Round(Point center, int radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public Round(Point center, int radius, Color color)
            : this(center, radius)
        {
            this.Color = color;
        }

        public Point Center { get; set; }

        public int Radius
        {
            get
            {
                return this.radius;
            }

            set
            {
                if (value > 0)
                {
                    this.radius = value;
                }
                else
                {
                    throw new ArgumentException("The radius mustn't be negative!");
                }
            }
        }
        
        public double Perimeter
        {
            get { return 2 * Math.PI * this.Radius; }
        }
        
        public double Area
    {
            get { return Math.PI * this.Radius * this.Radius; }
        }

        public override string ToString()
        {
            string toScreen =
                "Figure: \t\"Round\"\n" +
                "Center X: \t\"" + this.Center.X.ToString() + "\"\n" +
                "Center Y: \t\"" + this.Center.Y.ToString() + "\"\n" +
                "Radius: \t\"" + this.Radius.ToString() + "\"\n" +
                "Color: \t\t\"" + this.Color.ToString() + "\"\n" +
                "Length: \t\"" + this.Perimeter.ToString() + "\"\n" +
                "Area: \t\t\"" + this.Area.ToString() + "\"\n";

            return toScreen;
        }
    }
}

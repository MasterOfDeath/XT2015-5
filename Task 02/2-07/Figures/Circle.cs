namespace _2_07
{
    using System;
    using System.Drawing;

    public class Circle : Figure
    {
        private int r = 0;

        public Circle(Point center, int r)
        {
            this.Center = center;
            this.R = r;
        }

        public Circle(Point center, int r, Color color)
            : this(center, r)
        {
            this.Color = color;
        }

        public Point Center { get; set; }

        public int R
        {
            get
            {
                return this.r;
            }

            set
            {
                if (value > 0)
                {
                    this.r = value;
                }
                else
                {
                    throw new ArgumentException("The radius mustn't be negative!");
                }
            }
        }

        public double Length
        {
            get { return 2 * Math.PI * this.R; }
        }

        public override void Draw()
        {
            string toScreen =
                "Figure: \t\"Circle\"\n" +
                "Center X: \t\"" + this.Center.X.ToString() + "\"\n" +
                "Center Y: \t\"" + this.Center.Y.ToString() + "\"\n" +
                "Radius: \t\"" + this.R.ToString() + "\"\n" +
                "Color: \t\t\"" + this.Color.ToString() + "\"\n" +
                "Length: \t\"" + this.Length.ToString() + "\"\n";

            Console.WriteLine(toScreen);
        }
    }
}

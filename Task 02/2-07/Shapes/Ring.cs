namespace _2_07
{
    using System;
    using System.Drawing;

    public class Ring : Shape, IHasArea, IHasPerimeter
    {
        private int outerRadius;

        private int innerRadius;

        public Ring(Point center, int outerRadius, int innerRadius)
        {
            this.Center = center;
            this.SetRadiuses(outerRadius, innerRadius);
        }

        public Ring(Point center, int outerRadius, int innerRadius, Color color)
            : this(center, outerRadius, innerRadius)
        {
            this.Color = color;
        }

        public Point Center { get; set; }

        public int OuterRadius
        {
            get
            {
                return this.outerRadius;
            }

            set
            {
                this.SetRadiuses(value, this.InnerRadius);
            }
        }

        public int InnerRadius
        {
            get
            {
                return this.innerRadius;
            }

            set
            {
                this.SetRadiuses(this.OuterRadius, value);
            }
        }

        public double Perimeter
        {
            get
            {
                return (2 * Math.PI * this.InnerRadius) + (2 * Math.PI * this.OuterRadius);
            }
        }

        public double Area
        {
            get
            {
                return (Math.PI * this.OuterRadius * this.OuterRadius) - (Math.PI * this.InnerRadius * this.InnerRadius);
            }
        }

        public override string ToString()
        {
            string toScreen =
                "Figure: \t\"Ring\"\n" +
                "X: \t\t\"" + this.Center.X.ToString() + "\"\n" +
                "Y: \t\t\"" + this.Center.Y.ToString() + "\"\n" +
                "Outer radius: \t\"" + this.OuterRadius.ToString() + "\"\n" +
                "Inner radius: \t\"" + this.InnerRadius.ToString() + "\"\n" +
                "Color: \t\t\"" + this.Color.ToString() + "\"\n" +
                "Perimeter: \t\"" + this.Perimeter.ToString() + "\"\n" +
                "Area: \t\t\"" + this.Area.ToString() + "\"\n";

            return toScreen;
        }

        private void SetRadiuses(int outerRadius, int innerRadius)
        {
            if (outerRadius < 0 || innerRadius < 0)
            {
                throw new ArgumentException("The radius mustn't be negative!");
            }

            if (outerRadius > innerRadius)
            {
                this.outerRadius = outerRadius;
                this.innerRadius = innerRadius;
            }
            else
            {
                throw new ArgumentException("Inner radius have to be less than outer radius!");
            }
        }
    }
}
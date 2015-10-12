namespace _2_06
{
    using System;

    public class Ring
    {
        private int outerRadius;

        private int innerRadius;

        public Ring(Point center, int outerRadius, int innerRadius)
        {
            this.Center = center;

            this.SetRadiuses(outerRadius, innerRadius);
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
                return 2 * Math.PI * (this.InnerRadius + this.OuterRadius);
            }
        }

        public double Area
        {
            get
            {
                return Math.PI * ((this.OuterRadius * this.OuterRadius) - (this.InnerRadius * this.InnerRadius));
            }
        }

        public override string ToString()
        {
            return "X: \t\t\"" + this.Center.X.ToString() + "\"\n" +
                "Y: \t\t\"" + this.Center.Y.ToString() + "\"\n" +
                "Outer radius: \t\"" + this.OuterRadius.ToString() + "\"\n" +
                "Inner radius: \t\"" + this.InnerRadius.ToString() + "\"\n" +
                "Perimeter: \t\"" + this.Perimeter.ToString() + "\"\n" +
                "Area: \t\t\"" + this.Area.ToString() + "\"\n";
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

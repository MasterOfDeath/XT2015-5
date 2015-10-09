namespace _2_07
{
    using System;
    using System.Drawing;

    public class Ring : Figure
    {
        private int outR;

        private int inR;

        public Ring(Point center, int outR, int inR)
        {
            this.Center = center;
            this.SetRs(outR, inR);
        }

        public Ring(Point center, int outR, int inR, Color color)
            : this(center, outR, inR)
        {
            this.Color = color;
        }

        public Point Center { get; set; }

        public int OutR
        {
            get
            {
                return this.outR;
            }

            set
            {
                this.SetRs(value, this.InR);
            }
        }

        public int InR
        {
            get
            {
                return this.inR;
            }

            set
            {
                this.SetRs(this.OutR, value);
            }
        }

        public double Perimeter
        {
            get
            {
                return (2 * Math.PI * this.InR) + (2 * Math.PI * this.OutR);
            }
        }

        public double Area
        {
            get
            {
                return (Math.PI * this.OutR * this.OutR) - (Math.PI * this.InR * this.InR);
            }
        }
        
        public override void Draw()
        {
            string toScreen =
                "Figure: \t\"Ring\"\n" +
                "X: \t\t\"" + this.Center.X.ToString() + "\"\n" +
                "Y: \t\t\"" + this.Center.Y.ToString() + "\"\n" +
                "Outer radius: \t\"" + this.OutR.ToString() + "\"\n" +
                "Inner radius: \t\"" + this.InR.ToString() + "\"\n" +
                "Color: \t\t\"" + this.Color.ToString() + "\"\n" +
                "Perimeter: \t\"" + this.Perimeter.ToString() + "\"\n" +
                "Area: \t\t\"" + this.Area.ToString() + "\"\n";

            Console.WriteLine(toScreen);
        }

        private void SetRs(int outR, int inR)
        {
            if (outR < 0 || inR < 0)
            {
                throw new ArgumentException("The radius mustn't be negative!");
            }

            if (outR > inR)
            {
                this.outR = outR;
                this.inR = inR;
            }
            else
            {
                throw new ArgumentException("Inner radius have to be less than outer radius!");
            }
        }
    }
}

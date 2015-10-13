namespace _2_07
{
    using System;
    using System.Drawing;

    public class Rectangle : Shape, IHasArea, IHasPerimeter
    {
        private int sideA;
        private int sideB;

        public Rectangle(int sideA, int sideB)
        {
            this.sideA = sideA;
            this.sideB = sideB;
        }

        public Rectangle(int sideA, int sideB, Color color)
            : this(sideA, sideB)
        {
            this.Color = color;
        }

        public int SideA
        {
            get
            {
                return this.sideA;
            }

            set
            {
                if (value > 0)
                {
                    this.sideA = value;
                }
                else
                {
                    throw new ArgumentException("Side of rectangle mustn't be negative or 0!");
                }
            }
        }

        public int SideB
        {
            get
            {
                return this.sideB;
            }

            set
            {
                if (value > 0)
                {
                    this.sideB = value;
                }
                else
                {
                    throw new ArgumentException("Side of rectangle mustn't be negative or 0!");
                }
            }
        }

        public double Perimeter
        {
            get
            {
                return 2 * (this.sideA + this.sideB);
            }
        }

        public double Area
        {
            get
            {
                return this.sideA * this.sideB;
            }
        }

        public override string ToString()
        {
            string toScreen =
                "Figure: \t\"Rectangle\"\n" +
                "Lenght A: \t\"" + this.SideA.ToString() + "\"\n" +
                "Lenght B: \t\"" + this.SideB.ToString() + "\"\n" +
                "Color: \t\t\"" + this.Color.ToString() + "\"\n" +
                "Perimeter: \t\"" + this.Perimeter.ToString() + "\"\n" +
                "Area: \t\t\"" + this.Area.ToString() + "\"\n";

            return toScreen;
        }
    }
}
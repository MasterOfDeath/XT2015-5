namespace _2_07
{
    using System;
    using System.Drawing;

    public class Line : Shape, IHasPerimeter
    {
        private Point point1;
        private Point point2;

        public Line(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public Line(Point point1, Point point2, Color color)
            : this(point1, point2)
        {
            this.Color = color;
        }

        public Point Point1
        {
            get { return this.point1; }
            set { this.point1 = value; }
        }

        public Point Point2
        {
            get { return this.point2; }
            set { this.point2 = value; }
        }

        public double Perimeter
        {
            get
            {
                return Math.Sqrt(Math.Pow(this.point2.X - this.point1.X, 2) + Math.Pow(this.point2.Y - this.point1.Y, 2));
            }
        }

        public override string ToString()
        {
            string toScreen =
                "Figure: \t\"Line\"\n" +
                "Point1 X: \t\"" + this.Point1.X.ToString() + "\"\n" +
                "Point1 Y: \t\"" + this.Point1.Y.ToString() + "\"\n" +
                "Point2 X: \t\"" + this.Point2.X.ToString() + "\"\n" +
                "Point2 Y: \t\"" + this.Point2.Y.ToString() + "\"\n" +
                "Color: \t\t\"" + this.Color.ToString() + "\"\n" +
                "Length: \t\"" + this.Perimeter.ToString() + "\"\n";

            return toScreen;
        }
    }
}
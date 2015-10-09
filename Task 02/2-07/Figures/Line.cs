namespace _2_07
{
    using System;
    using System.Drawing;

    public class Line : Figure
    {
        private Point p1;
        private Point p2;

        public Line(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public Line(Point p1, Point p2, Color color)
            : this(p1, p2)
        {
            this.Color = color;
        }

        public Point P1
        {
            get { return this.p1; }
            set { this.p1 = value; }
        }

        public Point P2
        {
            get { return this.p2; }
            set { this.p2 = value; }
        }

        public override void Draw()
        {
            string toScreen =
                "Figure: \t\"Line\"\n" +
                "Point1 X: \t\"" + this.P1.X.ToString() + "\"\n" +
                "Point1 Y: \t\"" + this.P1.Y.ToString() + "\"\n" +
                "Point2 X: \t\"" + this.P2.X.ToString() + "\"\n" +
                "Point2 Y: \t\"" + this.P2.Y.ToString() + "\"\n" +
                "Color: \t\t\"" + this.Color.ToString() + "\"\n";

            Console.WriteLine(toScreen);
        }
    }
}

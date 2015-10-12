namespace _2_07
{
    using System.Drawing;

    public abstract class Shape
    {
        public Shape()
            : this(Color.Black)
        {
        }

        public Shape(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }

        // public abstract void Draw();
    }
}

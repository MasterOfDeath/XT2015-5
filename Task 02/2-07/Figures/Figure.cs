namespace _2_07
{
    using System.Drawing;

    public abstract class Figure
    {
        public Figure()
            : this(Color.Black)
        {
        }

        public Figure(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }

        public abstract void Draw();
    }
}

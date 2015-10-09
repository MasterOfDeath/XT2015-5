//-----------------------------------------------------------------------
// <copyright file="Triangle.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_02
{
    using System;

    /// <summary>
    /// Class of triangle
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// Length of a side
        /// </summary>
        private int a;

        /// <summary>
        /// Length of b side
        /// </summary>
        private int b;

        /// <summary>
        /// Length of c side
        /// </summary>
        private int c;

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle" /> class.
        /// </summary>
        /// <param name="a">Side A</param>
        /// <param name="b">Side B</param>
        /// <param name="c">Side C</param>
        public Triangle(int a, int b, int c)
        {
            this.SetAllSides(a, b, c);
        }

        /// <summary>
        /// Gets or sets value of a
        /// </summary>
        public int A
        {
            get
            {
                return this.a;
            }

            set
            {
                this.SetAllSides(value, this.B, this.C);
            }
        }

        /// <summary>
        /// Gets or sets value of b
        /// </summary>
        public int B
        {
            get
            {
                return this.b;
            }

            set
            {
                this.SetAllSides(this.A, value, this.C);
            }
        }

        /// <summary>
        /// Gets or sets value of c
        /// </summary>
        public int C
        {
            get
            {
                return this.c;
            }

            set
            {
                this.SetAllSides(this.A, this.B, value);
            }
        }
        
        /// <summary>
        /// Gets perimeter of triangle
        /// </summary>
        public double Perimeter
        {
            get
            {
                return this.A + this.B + this.C;
            }
        }

        /// <summary>
        /// Gets area of triangle
        /// </summary>
        public double Area
        {
            get
            {
                double halfPerimeter = this.Perimeter / 2;
                return Math.Sqrt(halfPerimeter * (halfPerimeter - this.A) * (halfPerimeter - this.B) * (halfPerimeter - this.C));
            }
        }

        private void SetAllSides(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentException("Side mustn't be negative!");
            }

            if ((a + b) > c && (a + c) > b && (b + c) > a)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
            else
            {
                throw new ArgumentException("Incorrect side values!");
            }
        }
    }
}

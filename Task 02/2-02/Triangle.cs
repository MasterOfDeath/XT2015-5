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
        private int a = 0;

        /// <summary>
        /// Length of b side
        /// </summary>
        private int b = 0;

        /// <summary>
        /// Length of c side
        /// </summary>
        private int c = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle" /> class.
        /// </summary>
        public Triangle()
        {
            this.a = 0;
            this.b = 0;
            this.c = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle" /> class.
        /// </summary>
        /// <param name="a">Side A</param>
        /// <param name="b">Side B</param>
        /// <param name="c">Side C</param>
        public Triangle(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        /// <summary>
        /// Gets or sets value of a
        /// </summary>
        public int A
        {
            get { return this.a; }
            set { this.a = (value > 0) ? value : 0; }
        }

        /// <summary>
        /// Gets or sets value of b
        /// </summary>
        public int B
        {
            get { return this.b; }
            set { this.b = (value > 0) ? value : 0; }
        }

        /// <summary>
        /// Gets or sets value of c
        /// </summary>
        public int C
        {
            get { return this.c; }
            set { this.c = (value > 0) ? value : 0; }
        }
        
        /// <summary>
        /// Gets perimeter of triangle
        /// </summary>
        public double Perimeter
        {
            get { return this.GetPerimeter(); }
        }

        /// <summary>
        /// Gets area of triangle
        /// </summary>
        public double Area
        {
            get { return this.GetArea(); }
        }

        /// <summary>
        /// Count perimeter
        /// </summary>
        /// <returns>Value of perimeter</returns>
        private double GetPerimeter()
            => this.a + this.b + this.c;

        /// <summary>
        /// Count area
        /// </summary>
        /// <returns>Value of area</returns>
        private double GetArea()
        {
            double poluPer = this.GetPerimeter() / 2;

            return Math.Sqrt(poluPer * (poluPer - this.a) * (poluPer - this.b) * (poluPer - this.c));
        }
    }
}

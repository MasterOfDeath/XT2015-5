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
            if (this.IsTriangle(a, b, c))
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
            else
            {
                throw new ArgumentException("Incorrect values of sides!");
            }
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
                if (this.IsTriangle(value, this.B, this.C))
                {
                    this.a = value;
                }
                else
                {
                    throw new ArgumentException("Incorrect values of A side!");
                }
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
                if (this.IsTriangle(this.A, value, this.C))
                {
                    this.b = value;
                }
                else
                {
                    throw new ArgumentException("Incorrect values of B side!");
                }
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
                if (this.IsTriangle(this.A, this.B, value))
                {
                    this.c = value;
                }
                else
                {
                    throw new ArgumentException("Incorrect values of C side!");
                }
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

        /// <summary>
        /// Can we create triangle by these sides?
        /// </summary>
        /// <param name="a">Side A</param>
        /// <param name="b">Side B</param>
        /// <param name="c">Side C</param>
        /// <returns>Yes or no</returns>
        private bool IsTriangle(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                return false;
            }

            return (a + b) > c && (a + c) > b && (b + c) > a;
        }
    }
}

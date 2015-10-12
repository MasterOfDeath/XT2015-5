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
        private int sideA;

        /// <summary>
        /// Length of b side
        /// </summary>
        private int sideB;

        /// <summary>
        /// Length of c side
        /// </summary>
        private int sideC;

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle" /> class.
        /// </summary>
        /// <param name="sideA">Side A</param>
        /// <param name="sideB">Side B</param>
        /// <param name="sideC">Side C</param>
        public Triangle(int sideA, int sideB, int sideC)
        {
            this.SetAllSides(sideA, sideB, sideC);
        }

        /// <summary>
        /// Gets or sets value of a
        /// </summary>
        public int SideA
        {
            get
            {
                return this.sideA;
            }

            set
            {
                this.SetAllSides(value, this.SideB, this.SideC);
            }
        }

        /// <summary>
        /// Gets or sets value of b
        /// </summary>
        public int SideB
        {
            get
            {
                return this.sideB;
            }

            set
            {
                this.SetAllSides(this.SideA, value, this.SideC);
            }
        }

        /// <summary>
        /// Gets or sets value of c
        /// </summary>
        public int SideC
        {
            get
            {
                return this.sideC;
            }

            set
            {
                this.SetAllSides(this.SideA, this.SideB, value);
            }
        }
        
        /// <summary>
        /// Gets perimeter of triangle
        /// </summary>
        public double Perimeter
        {
            get
            {
                return this.SideA + this.SideB + this.SideC;
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

                return 
                    Math.Sqrt(
                        halfPerimeter * 
                        (halfPerimeter - this.SideA) * 
                        (halfPerimeter - this.SideB) * 
                        (halfPerimeter - this.SideC));
            }
        }

        private void SetAllSides(int sideA, int sideB, int sideC)
        {
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                throw new ArgumentException("Side mustn't be negative!");
            }

            if ((sideA + sideB) > sideC && (sideA + sideC) > sideB && (sideB + sideC) > sideA)
            {
                this.sideA = sideA;
                this.sideB = sideB;
                this.sideC = sideC;
            }
            else
            {
                throw new ArgumentException("Incorrect side values!");
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="Round.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_01
{
    using System;

    /// <summary>
    /// Class of round
    /// </summary>
    public class Round
    {
        private double radius = 0;
        
        public Round(double radius)
        {
            this.Radius = radius;
        }
        
        public int X { get; set; }

        public int Y { get; set; }

        public double Radius
        {
            get
            {
                return this.radius;
            }

            set
            {
                if (value > 0)
                {
                    this.radius = value;
                }
                else
                {
                    throw new ArgumentException("The radius mustn't be negative!");
                }
            }
        }
        
        public double Perimeter
        {
            get { return 2 * Math.PI * this.Radius; }
        }
        
        public double Area
        {
            get { return Math.PI * this.Radius * this.Radius; }
        }
    }
}

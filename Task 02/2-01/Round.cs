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
        private double r = 0;
        
        public Round(double r)
        {
            this.R = r;
        }
        
        public int X { get; set; }

        public int Y { get; set; }

        public double R
        {
            get
            {
                return this.r;
            }

            set
            {
                if (value > 0)
                {
                    this.r = value;
                }
                else
                {
                    throw new ArgumentException("The radius mustn't be negative!");
                }
            }
        }
        
        public double Area
        {
            get { return 2 * Math.PI * this.R; }
        }
        
        public double Length
        {
            get { return Math.PI * this.R * this.R; }
        }
    }
}

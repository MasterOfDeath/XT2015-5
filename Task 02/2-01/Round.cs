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
        /// <summary>
        /// Value of radius
        /// </summary>
        private double r = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Round" /> class.
        /// </summary>
        public Round()
        {
            this.Y = 0;
            this.X = 0;
            this.r = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round" /> class.
        /// </summary>
        public Round(int r)
        {
            this.Y = 0;
            this.X = 0;
            this.r = r;
        }

        /// <summary>
        /// Gets or sets X coordinate of center of round
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets Y coordinate of center of round
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets value of radius of round
        /// </summary>
        public double R
        {
            get { return this.r; }
            set { this.r = (value > 0) ? value : 0; }
        }

        /// <summary>
        /// Gets radius of area
        /// </summary>
        public double RoundArea
        {
            get { return 2 * Math.PI * this.R; }
        }

        /// <summary>
        /// Gets length of circle
        /// </summary>
        public double CircleLength
        {
            get { return Math.PI * this.R * this.R; }
        }
    }
}

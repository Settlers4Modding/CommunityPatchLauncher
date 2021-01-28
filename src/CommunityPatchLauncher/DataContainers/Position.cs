using System;
using System.Windows;

namespace CommunityPatchLauncher.DataContainers
{
    /// <summary>
    /// This class represents a point on the monitor
    /// </summary>
    internal class Position
    {
        /// <summary>
        /// The x coordinate of the position
        /// </summary>
        public float X { get; }

        /// <summary>
        /// The y coodinate of the position
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// Create a new instance for this class
        /// </summary>
        /// <param name="point"></param>
        public Position(Point point)
        {
            X = (float)point.X;
            Y = (float)point.Y;
        }

        /// <summary>
        /// Get the distance to another point
        /// </summary>
        /// <param name="otherPosition">The other point to check</param>
        /// <returns>The distance between two points</returns>
        public float DistanceTo(Position otherPosition)
        {
            float termA = (X - otherPosition.X);
            float termB = (Y - otherPosition.Y);
            termA = termA * termA;
            termB = termB * termB;
            return (float)Math.Sqrt(termA + termB);
        }
    }
}


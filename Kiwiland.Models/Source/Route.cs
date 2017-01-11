using System;

namespace Kiwiland.Models
{
    /// <summary>
    /// Representation of a route between two towns.
    /// </summary>
    /// <seealso cref="System.IComparable{Route}" />
    public class Route : IComparable<Route>
    {
        #region Fields

        /// <summary>
        /// Gets the starting town.
        /// </summary>
        /// <value>
        /// </value>
        public Town From { get; }

        /// <summary>
        /// Gets the ending town.
        /// </summary>
        /// <value>
        /// </value>
        public Town To { get; }

        /// <summary>
        /// Gets the distance of the route between starting and ending town.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public int Distance { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Route"/> class.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="distance">The distance.</param>
        public Route(Town origin,Town destination,int distance)
        {
            From = origin;
            To = destination;
            Distance = distance;
        }

        #endregion

        public override string ToString()
        {
            return From.Name + " " + To.Name + " " + Distance;
        }

        #region IComparable implementation

        /// <summary>
        /// Compares one route to another. In our case, compare's the distance of one 
        /// route to distance of another route.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(Route other)
        {
           return Distance.CompareTo(other.Distance);
        }

        #endregion
    }
}

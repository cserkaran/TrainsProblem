using System;
using System.Collections;
using System.Collections.Generic;

namespace Kiwiland.Models
{
    /// <summary>
    /// The routes directly accessible from a particular town.
    /// In terms of graph representation, this is the adjacency list of a town vertex i.e.
    /// list of routes of towns directly reachable from a given town.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Kiwiland.Models.Route}" />
    public class TownRoutes  : IEnumerable<Route>
    {
        #region Fields

        /// <summary>
        /// the origin town.
        /// </summary>
        private Town _from;

        /// <summary>
        /// The routes originating from a given town.
        /// </summary>
        private LinkedList<Route> _routes = new LinkedList<Route>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TownRoutes"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        public TownRoutes(Town from)
        {
            _from = from;
        }

        #endregion

        #region Add a Route

        /// <summary>
        /// Adds the specified route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <exception cref="System.InvalidOperationException">Route has to start from given town in the constructor.</exception>
        public void Add(Route route)
        {
            if(route.From != _from)
            {
                throw new InvalidOperationException("Route has to start from given town in the constructor.");
            }
            _routes.AddLast(route);
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Route> GetEnumerator()
        {
            return _routes.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _routes.GetEnumerator();
        }

        #endregion

        #region Route to Destination

        /// <summary>
        /// Gets the route to particular destination if exists.
        /// </summary>
        /// <param name="to">The destination town.</param>
        /// <returns>Route if exists else a null value.</returns>
        public Route RouteToDestination(Town to)
        {
            foreach(var route in _routes)
            {
                if (route.To.Equals(to))
                {
                    return route;
                }
            }
            return null;
        }

        #endregion
    }
}

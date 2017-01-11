using System;
using System.Collections.Generic;
using System.Linq;

namespace Kiwiland.Models
{
    /// <summary>
    /// Represents a directed graph of a Railways network with town's as vertices and 
    /// routes as edges.
    /// </summary>
    public class RailNetwork
    {
        #region Properties

        /// <summary>
        /// The routes dictionary. Maintains the routes from a town.
        /// </summary>
        Dictionary<Town, TownRoutes> routes = new Dictionary<Town, TownRoutes>();

        /// <summary>
        /// The towns in the railway network.
        /// </summary>
        HashSet<Town> towns = new HashSet<Town>();

        #endregion

        #region Fields

        public IEnumerable<Town> Towns
        {
            get
            {
                return towns;
            }
        }

        #endregion

        #region Routes in the rail network.

        /// <summary>
        /// Adds the route.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="distance">The distance.</param>
        /// <exception cref="System.InvalidOperationException">starting and ending town cannot be the same!</exception>
        public void AddRoute(Town from,Town to,int distance)
        {
            if(from == to)
            {
                throw new InvalidOperationException("starting and ending town cannot be the same!");
            }

            Route route = new Route(from, to, distance);

            if (routes.ContainsKey(from))
            {
                routes[from].Add(route);
            }
            else
            {
                TownRoutes list = new TownRoutes(from);
                list.Add(route);
                routes.Add(from, list);
            }

            if (!routes.ContainsKey(to))
            {
                TownRoutes list = new TownRoutes(to);
                routes.Add(to, list);
            }

            towns.Add(from);
            towns.Add(to);
        }

        /// <summary>
        /// Get the Routes directly reachable from a specified town if they exist.
        /// Else return a null value.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>
        /// <see cref="TownRoutes"/>directly reachable from a specified town if they exist.
        /// Else return a null value.
        /// </returns>
        public TownRoutes Routes(Town from)
        {
            if (routes.ContainsKey(from))
            {
                return routes[from];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Trips between two towns

        /// <summary>
        /// Gets the Possible trips from one town to another town.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public IReadOnlyCollection<IReadOnlyCollection<Route>> Trips(Town from,Town to)
        {
            var trips = new List<List<Route>>();
            var routes = Routes(from);

            HashSet<Route> traversed = new HashSet<Route>();

            foreach (var route in routes)
            {
                Trips(route, trips, to, traversed);
                traversed.Clear();
            }

            return trips;
        }

        /// <summary>
        /// Calculate the Possible trips from one town to another town.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="trips">The trips.</param>
        /// <param name="to">To.</param>
        /// <param name="traversed">The traversed.</param>
        public void Trips(Route route, List<List<Route>> trips, Town to, HashSet<Route> traversed)
        {
            if (!traversed.Contains(route))
            {
                traversed.Add(route);

                if (route.To == to)
                {
                    trips.Add(traversed.ToList());
                }

                var routes = Routes(route.To);
                foreach (var subRoute in routes)
                {
                    Trips(subRoute, trips, to, traversed);
                }
            }
        }

        #endregion
    }
}

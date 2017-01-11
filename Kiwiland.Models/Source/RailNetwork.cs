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

        /// <summary>
        /// Gets the towns.
        /// </summary>
        /// <value>
        /// The towns.
        /// </value>
        public List<Town> Towns
        {
            get
            {
                return towns.ToList();
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

        #region Trips Count For Exact number of Hops 

        /// <summary>
        /// Calculates the number of trips starting at from and ending at to with exactly hop number of stops.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="hops">The hops.</param>
        /// <returns></returns>
        public Trips TripCountForExactHops(Town from,Town to,int hops)
        {
            List<List<Route>> trips = new List<List<Route>>();

            int count = 0;
            var routes = Routes(from);
            foreach(var route in routes)
            {
                var processedRoutes = new List<Route>();
                TripCountForHops(route, to, processedRoutes,hops,trips,ref count);
            }

            return new Trips(trips);
        }

        /// <summary>
        /// Calculates the number of trips starting at from and ending at to with exactly hop number of stops.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="to">To.</param>
        /// <param name="processedRoutes">The processed routes.</param>
        /// <param name="hops">The hops.</param>
        /// <param name="trips">The trips.</param>
        /// <param name="count">The count.</param>
        private void TripCountForHops(Route route, Town to, List<Route> processedRoutes, int hops, List<List<Route>> trips, ref int count)
        {
            processedRoutes.Add(route);

            if (route.To == to && hops == processedRoutes.Count)
            {
                trips.Add(processedRoutes.ToList());
                count++;
            }
            else
            {
                if (!(processedRoutes.Count >= hops && route.To != to))
                {
                    var from = route.To;
                    var routes = Routes(from);
                    foreach (var r in routes)
                    {
                        TripCountForHops(r, to, processedRoutes, hops, trips,ref count);
                        processedRoutes.Remove(r);
                    }
                }
            }
        }

        #endregion

        #region Trips Count For Max number of Hops 

        /// <summary>
        /// Calculates the number of trips starting at from and ending at less or than equal to maxhop number of stops.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="maxHops">The maximum allowed hops.</param>
        /// <returns></returns>
        public Trips TripCountForMaxNumberOfHops(Town from, Town to, int maxHops)
        {
            List<List<Route>> trips = new List<List<Route>>();
         
            var routes = Routes(from);
            foreach (var route in routes)
            {
                var processedRoutes = new List<Route>();
                TripCountForMaxNumberOfHops(route, to, processedRoutes, maxHops,trips);
            }

            return new Trips(trips);
        }

        /// <summary>
        /// Calculates the number of trips starting at from and ending at to with exactly hop number of stops.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="to">To.</param>
        /// <param name="processedRoutes">The processed routes.</param>
        /// <param name="maxHops">The maxHops.</param>
        /// <param name="trips">The trips.</param>
        /// <param name="count">The count.</param>
        private void TripCountForMaxNumberOfHops(Route route, Town to, List<Route> processedRoutes, int maxHops, List<List<Route>> trips)
        {
            processedRoutes.Add(route);

            if (route.To == to && maxHops >= processedRoutes.Count)
            {
                trips.Add(processedRoutes.ToList());
            }
            else
            {
                if (!(processedRoutes.Count > maxHops && route.To != to))
                {
                    var from = route.To;
                    var routes = Routes(from);
                    foreach (var r in routes)
                    {
                        TripCountForMaxNumberOfHops(r, to, processedRoutes, maxHops, trips);
                    }
                }
                else
                {
                    processedRoutes.Remove(route);
                }
            }
        }

        #endregion

        #region Trips Count For Max number of Hops 

        /// <summary>
        /// Calculates the count of the number of different routes from one town to another
        /// with a total distance of less than the given maximum distance.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        public Trips TripsCountForMaxDistance(Town from, Town to, int maxDistance)
        {
            List<List<Route>> trips = new List<List<Route>>();
            var runningSum = 0;
            var routes = Routes(from);
            foreach (var route in routes)
            {
                runningSum = 0;
                var processedRoutes = new List<Route>();
                TripsCountForMaxDistance(route, to, processedRoutes, maxDistance,trips,ref runningSum);
            }

            return new Trips(trips);
        }

        /// <summary>
        /// Calculates the count of the number of different routes from one town to another
        /// with a total distance of less than the given maximum distance.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="to">To.</param>
        /// <param name="processedRoutes">The processed routes.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <param name="runningSum">The running sum.</param>
        /// <param name="count">The count.</param>
        private void TripsCountForMaxDistance(Route route, Town to, List<Route> processedRoutes,
                                            int maxDistance, List<List<Route>> trips, ref int runningSum)
        {
            processedRoutes.Add(route);
            runningSum += route.Distance;
            if (route.To == to && runningSum < maxDistance)
            {
                trips.Add(processedRoutes.ToList());
            }

            if (!(runningSum > maxDistance))
            {
                var from = route.To;
                var routes = Routes(from);
                foreach (var r in routes)
                {
                    TripsCountForMaxDistance(r, to, processedRoutes, maxDistance, trips,ref runningSum);
                    runningSum -= r.Distance;
                    RemoveAtLastIndex(r, processedRoutes);
                }
            }
        }

        /// <summary>
        /// Removes last occurance of an item from the list.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="processedRoutes">The processed routes.</param>
        private static void RemoveAtLastIndex(Route route, List<Route> processedRoutes)
        {
            var lastIndex = processedRoutes.IndexOf(route);
            for (int i = processedRoutes.Count - 1; i >= 0; i--)
            {
                if (route == processedRoutes[i])
                {
                    lastIndex = i;
                    break;
                }
            }

            if (lastIndex >= 0 && lastIndex < processedRoutes.Count)
            {
                processedRoutes.RemoveAt(lastIndex);
            }
        }

        #endregion

    }
}

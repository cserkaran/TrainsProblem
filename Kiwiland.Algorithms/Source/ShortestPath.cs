using Kiwiland.Models;
using System.Collections.Generic;

namespace Kiwiland.Algorithms
{
    /// <summary>
    /// Dijkstra's shortest path algorithm implementation to calculate the shortest 
    /// path between two towns.
    /// </summary>
    public class ShortestPath
    {
        #region Fields

        /// <summary>
        /// The origin town.
        /// </summary>
        private Town _from;

        /// <summary>
        /// The rail network
        /// </summary>
        private RailNetwork _railNetwork;

        /// <summary>
        /// The min priority queue.
        /// </summary>
        MinPQ<Route> pq = new MinPQ<Route>();

        /// <summary>
        /// Maintains The distance from origin town to every other town in the rail network.
        /// </summary>
        private Dictionary<Town, int> _distTo = new Dictionary<Town, int>();

        /// <summary>
        /// The route from origin town to every other town.It stores the last route edge to that town on the path.
        /// </summary>
        private Dictionary<Town, Route> _routeTo = new Dictionary<Town, Route>();

        /// <summary>
        /// All the routes.
        /// </summary>
        private HashSet<Route> _allRoutes = new HashSet<Route>();

        #endregion

        #region Constructors

        public ShortestPath(RailNetwork railNetwork, Town from)
        {
            _railNetwork = railNetwork;
            _from = from;
            _distTo.Add(_from, 0);
            _routeTo.Add(_from, null);
            foreach (var town in railNetwork.Towns)
            {
                if (town != from)
                {
                    _distTo.Add(town, int.MaxValue);
                    _routeTo.Add(town, null);
                }
            }

            Route r = new Route(from, from, 0);
            _allRoutes.Add(r);
            pq.Insert(r);
            while (!pq.IsEmpty())
            {
                Route v = pq.DelMin();
                var townRoutes = railNetwork.Routes(v.To);
                foreach(var route in townRoutes)
                {
                    Relax(route);
                    _allRoutes.Add(route);
                }

            }
        }

        #endregion

        #region Relax the edges 

        /// <summary>
        /// Relaxes the specified route and re-compute the shorter part to a given town
        /// if applicable.
        /// </summary>
        /// <param name="route">The route.</param>
        private void Relax(Route route)
        {
            var from = route.From;
            var to = route.To;

            // if we are back to starting from node.. i.e. there is a cycle in the graph, loop back
            // in the cycle to calculate shortest 
            if (_distTo[to] > _distTo[from] + route.Distance ||(to == _from && _distTo[to] == 0))
            {
                _distTo[to] = _distTo[from] + route.Distance;
                var o = _routeTo[to];
                _routeTo[to] = route;
                if (pq.Contains(o))
                {
                    pq.ChangeKey(o, _routeTo[to]);
                }
                else
                {
                    pq.Insert(_routeTo[to]);
                }
            }
        }

        #endregion

        #region Shortest Path API

        /// <summary>
        /// Distance to given town from the origin.
        /// </summary>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public int DistanceTo(Town to)
        {
            return _distTo[to]; 
        }


        /// <summary>
        /// Determines whether a route, indirect or direct exists to the specified town from origin.
        /// </summary>
        /// <param name="to">To.</param>
        /// <returns>
        ///   <c>true</c> if it has route to specified town; otherwise, <c>false</c>.
        /// </returns>
        public bool HasRouteTo(Town to)
        {
            return _routeTo[to] != null;
        }

        #endregion
    }
}
